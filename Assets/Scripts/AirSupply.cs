using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AirSupply : MonoBehaviour
{
    //Place this script on an air supply.

    [SerializeField] private float airSupplyIncrease;
    [SerializeField] private float airSupplyCountdownMaxTime;
    private float countdownTime;
    [SerializeField] private GameObject bubbleParticleEffect;
    [SerializeField] private bool isCountdownActive;
    [SerializeField] private GameObject airBubblePrefab;

    private void Start()
    {
        StartCoroutine(StartCountDown());
    }

    private void Update()
    {
        VisualCue();

        AirSupplyCountdown();
    }

    private void VisualCue()
    {
        if (!isCountdownActive)
        {
            bubbleParticleEffect.SetActive(false);
        }
        if (isCountdownActive)
        {
            bubbleParticleEffect.SetActive(true);
        }
    }


    // Timer to refresh air supply.
    private void AirSupplyCountdown()
    {
        if (isCountdownActive)
        {
            if (countdownTime > 0)
            {
                countdownTime -= Time.deltaTime;
                //Debug.Log(countdownTime);
            }
            else
            {
                StartCoroutine(StartCountDown());
            }
        }
        else
        {
            //Debug.Log("Countdown stopped");
        }
    }

    IEnumerator StartCountDown()
    {
        isCountdownActive = false;
        Instantiate(airBubblePrefab, transform.position, transform.rotation);
        yield return new WaitForSeconds(2);

        //Starts a countdown to have more supply air.
        countdownTime = airSupplyCountdownMaxTime;
        isCountdownActive = true;
        AirSupplyCountdown();
    }


}
