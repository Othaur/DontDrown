using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantAirBubble : MonoBehaviour
{
    Air air;

    [SerializeField] private float airSupplyIncrease;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float movementForce;

    // Start is called before the first frame update
    void Start()
    {
        air = GameObject.FindObjectOfType<Air>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector3.up * movementForce);
    }



    private void OnCollisionEnter(Collision collision)
    {
        /*if (GameObject.FindObjectOfType<FPCController>())
        {
            //Increases player's air supply.
            air.ChangeAirSupply(airSupplyIncrease);
            StartCoroutine(BurstBubble());
        }*/
    }

    IEnumerator BurstBubble()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
