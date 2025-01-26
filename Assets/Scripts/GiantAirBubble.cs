using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantAirBubble : MonoBehaviour
{
    Air air;

    [SerializeField] private float airSupplyIncrease;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float movementForce;
    private float bubbleBurstCountdown = 10;

    // Start is called before the first frame update
    void Start()
    {
        air = GameObject.FindObjectOfType<Air>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector3.up * movementForce);
    }

    private void Update()
    {
        BubbleBurstCountdown();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (GameObject.FindObjectOfType<CharacterController>())
        {
            //Increases player's air supply.
            air.ChangeAirSupply(airSupplyIncrease);
            StartCoroutine(BurstBubble());
        }
    }

    private void BubbleBurstCountdown()
    {
        if (bubbleBurstCountdown > 0)
        {
            bubbleBurstCountdown -= Time.deltaTime;
            //Debug.Log(bubbleBurstCountdown);
        }
        else
        {
            StartCoroutine(BurstBubble());
        }
    }

    IEnumerator BurstBubble()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
