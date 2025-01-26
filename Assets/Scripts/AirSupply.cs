using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AirSupply : MonoBehaviour
{
    //Place this script on an air supply.

    [SerializeField] private float airSupplyIncrease;
    [SerializeField] private float airSupplyCountdownMaxTime;
    [SerializeField] private GameObject bubbleParticleEffect;
    [SerializeField] private bool isCountdownActive;
    [SerializeField] private GameObject airBubblePrefab;

    private void Start()
    {
        StartCoroutine(AirSupplyCountdown(airSupplyCountdownMaxTime));
    }

    private void Update()
    {
        VisualCue();
    }

    private void VisualCue()
    {
        if (isCountdownActive)
        {
            bubbleParticleEffect.SetActive(false);
        }
        if (!isCountdownActive)
        {
            bubbleParticleEffect.SetActive(true);
        }
    }


    // Timer to refresh air supply.

    IEnumerator AirSupplyCountdown(float time)
    {
        yield return new WaitForSeconds(2);

        for (float i = time; i > 0; i -= Time.deltaTime)
        {
            if (i > 0)
            {
                isCountdownActive = true;
            }
            if (i <= 0)
            {
                Instantiate(airBubblePrefab, transform.position, transform.rotation);
                isCountdownActive = false;
                //Starts a countdown to have more supply air.
                StartCoroutine(AirSupplyCountdown(airSupplyCountdownMaxTime));
            }
            Debug.Log(i);
            yield return null;
        }


    }




}
