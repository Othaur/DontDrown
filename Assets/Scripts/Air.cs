using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Air : MonoBehaviour
{
    [SerializeField] private Slider airSlider;

    [Header("Air supply and Timer")]
    [SerializeField] private float airSupply;
    [SerializeField] private float airUsage;
    private float airSupplyMax = 100;
    private float airSupplyMin = 0;
    [SerializeField] private float airCountdownMaxTime;


    [Header("Air Bubble UI")]
    [SerializeField] private GameObject airBubble0;
    [SerializeField] private GameObject airBubbleHalf0;
    [SerializeField] private GameObject airBubble1;
    [SerializeField] private GameObject airBubbleHalf1;
    [SerializeField] private GameObject airBubble2;
    [SerializeField] private GameObject airBubbleHalf2;
    [SerializeField] private GameObject airBubble3;
    [SerializeField] private GameObject airBubbleHalf3;
    [SerializeField] private GameObject airBubble4;
    [SerializeField] private GameObject airBubbleHalf4;
    [SerializeField] private GameObject airBubble5;
    [SerializeField] private GameObject airBubbleHalf5;
    [SerializeField] private GameObject airBubble6;
    [SerializeField] private GameObject airBubbleHalf6;
    [SerializeField] private GameObject airBubble7;
    [SerializeField] private GameObject airBubbleHalf7;
    [SerializeField] private GameObject airBubble8;
    [SerializeField] private GameObject airBubbleHalf8;
    [SerializeField] private GameObject airBubble9;
    [SerializeField] private GameObject airBubbleHalf9;

    [SerializeField] private bool isDead;


    private void Start()
    {
        airSupply = airSupplyMax;

        //Air slider starts at full.
        //airSlider.value = 1;
        SliderUpdate();

        //Timer for player to lose air begins.
        StartCoroutine(AirCountdown(airCountdownMaxTime));
    }

    private void Update()
    {
        //Checks for death conditions.
        DeathCheck();
    }

    private void SliderUpdate()
    {
        //Updates air slider.
        //airSlider.value = airSupply / 100;
        if (!isDead)
        {
            ResetBubbleSlider();

            if (airSupply > 90 && airSupply <= 95)
            {
                airBubble9.SetActive(false);
                airBubbleHalf9.SetActive(true);
            }
            if (airSupply > 85 && airSupply <= 90)
            {
                airBubble9.SetActive(false);
            }
            if (airSupply > 80 && airSupply <= 85)
            {
                airBubble9.SetActive(false);
                airBubble8.SetActive(false);
                airBubbleHalf8.SetActive(true);
            }
            if (airSupply > 75 && airSupply <= 80)
            {
                airBubble9.SetActive(false);
                airBubble8.SetActive(false);
            }
            if (airSupply > 70 && airSupply <= 75)
            {
                airBubble9.SetActive(false);
                airBubble8.SetActive(false);
                airBubble7.SetActive(false);
                airBubbleHalf7.SetActive(true);
            }
            if (airSupply > 65 && airSupply <= 70)
            {
                airBubble9.SetActive(false);
                airBubble8.SetActive(false);
                airBubble7.SetActive(false);
            }
            if (airSupply > 60 && airSupply <= 65)
            {
                airBubble9.SetActive(false);
                airBubble8.SetActive(false);
                airBubble7.SetActive(false);
                airBubble6.SetActive(false);
                airBubbleHalf6.SetActive(true);
            }
            if (airSupply > 55 && airSupply <= 60)
            {
                airBubble9.SetActive(false);
                airBubble8.SetActive(false);
                airBubble7.SetActive(false);
                airBubble6.SetActive(false);
            }
            if (airSupply > 50 && airSupply <= 55)
            {
                airBubble9.SetActive(false);
                airBubble8.SetActive(false);
                airBubble7.SetActive(false);
                airBubble6.SetActive(false);
                airBubble5.SetActive(false);
                airBubbleHalf5.SetActive(true);
            }
            if (airSupply > 45 && airSupply <= 50)
            {
                airBubble9.SetActive(false);
                airBubble8.SetActive(false);
                airBubble7.SetActive(false);
                airBubble6.SetActive(false);
                airBubble5.SetActive(false);
            }
            if (airSupply > 40 && airSupply <= 45)
            {
                airBubble9.SetActive(false);
                airBubble8.SetActive(false);
                airBubble7.SetActive(false);
                airBubble6.SetActive(false);
                airBubble5.SetActive(false);
                airBubble4.SetActive(false);
                airBubbleHalf4.SetActive(true);
            }
            if (airSupply > 35 && airSupply <= 40)
            {
                airBubble9.SetActive(false);
                airBubble8.SetActive(false);
                airBubble7.SetActive(false);
                airBubble6.SetActive(false);
                airBubble5.SetActive(false);
                airBubble4.SetActive(false);
            }
            if (airSupply > 30 && airSupply <= 35)
            {
                airBubble9.SetActive(false);
                airBubble8.SetActive(false);
                airBubble7.SetActive(false);
                airBubble6.SetActive(false);
                airBubble5.SetActive(false);
                airBubble4.SetActive(false);
                airBubble3.SetActive(false);
                airBubbleHalf3.SetActive(true);
            }
            if (airSupply > 25 && airSupply <= 30)
            {
                airBubble9.SetActive(false);
                airBubble8.SetActive(false);
                airBubble7.SetActive(false);
                airBubble6.SetActive(false);
                airBubble5.SetActive(false);
                airBubble4.SetActive(false);
                airBubble3.SetActive(false);
            }
            if (airSupply > 20 && airSupply <= 25)
            {
                airBubble9.SetActive(false);
                airBubble8.SetActive(false);
                airBubble7.SetActive(false);
                airBubble6.SetActive(false);
                airBubble5.SetActive(false);
                airBubble4.SetActive(false);
                airBubble3.SetActive(false);
                airBubble2.SetActive(false);
                airBubbleHalf2.SetActive(true);
            }
            if (airSupply > 15 && airSupply <= 20)
            {
                airBubble9.SetActive(false);
                airBubble8.SetActive(false);
                airBubble7.SetActive(false);
                airBubble6.SetActive(false);
                airBubble5.SetActive(false);
                airBubble4.SetActive(false);
                airBubble3.SetActive(false);
                airBubble2.SetActive(false);
            }
            if (airSupply > 10 && airSupply <= 15)
            {
                airBubble9.SetActive(false);
                airBubble8.SetActive(false);
                airBubble7.SetActive(false);
                airBubble6.SetActive(false);
                airBubble5.SetActive(false);
                airBubble4.SetActive(false);
                airBubble3.SetActive(false);
                airBubble2.SetActive(false);
                airBubble1.SetActive(false);
                airBubbleHalf1.SetActive(true);
            }
            if (airSupply > 5 && airSupply <= 10)
            {
                airBubble9.SetActive(false);
                airBubble8.SetActive(false);
                airBubble7.SetActive(false);
                airBubble6.SetActive(false);
                airBubble5.SetActive(false);
                airBubble4.SetActive(false);
                airBubble3.SetActive(false);
                airBubble2.SetActive(false);
                airBubble1.SetActive(false);
            }
            if (airSupply > 0 && airSupply <= 5)
            {
                airBubble9.SetActive(false);
                airBubble8.SetActive(false);
                airBubble7.SetActive(false);
                airBubble6.SetActive(false);
                airBubble5.SetActive(false);
                airBubble4.SetActive(false);
                airBubble3.SetActive(false);
                airBubble2.SetActive(false);
                airBubble1.SetActive(false);
                airBubble0.SetActive(false);
                airBubbleHalf0.SetActive(true);
            }
            if (airSupply == 0)
            {
                airBubble9.SetActive(false);
                airBubble8.SetActive(false);
                airBubble7.SetActive(false);
                airBubble6.SetActive(false);
                airBubble5.SetActive(false);
                airBubble4.SetActive(false);
                airBubble3.SetActive(false);
                airBubble2.SetActive(false);
                airBubble1.SetActive(false);
                airBubble0.SetActive(false);
            }
        }

    }

    private void ResetBubbleSlider()
    {
        airBubble0.SetActive(true);
        airBubble1.SetActive(true);
        airBubble2.SetActive(true);
        airBubble3.SetActive(true);
        airBubble4.SetActive(true);
        airBubble5.SetActive(true);
        airBubble6.SetActive(true);
        airBubble7.SetActive(true);
        airBubble8.SetActive(true);
        airBubble9.SetActive(true);

        airBubbleHalf0.SetActive(false);
        airBubbleHalf1.SetActive(false);
        airBubbleHalf2.SetActive(false);
        airBubbleHalf3.SetActive(false);
        airBubbleHalf4.SetActive(false);
        airBubbleHalf5.SetActive(false);
        airBubbleHalf6.SetActive(false);
        airBubbleHalf7.SetActive(false);
        airBubbleHalf8.SetActive(false);
        airBubbleHalf9.SetActive(false);

    }

    IEnumerator AirCountdown(float time)
    {
        // Timer to decrease air
        for (float i = time; i > 0; i -= Time.deltaTime)
        {
            yield return null;
        }

        StartCoroutine(AirCountdown(airCountdownMaxTime));

        ChangeAirSupply(-1 * airUsage);
        //Updates air slider.
        SliderUpdate();
    }


    public void ChangeAirSupply(float amt)
    {
        // Increase air from air supply.
        airSupply += amt;
        //Updates air slider.
        SliderUpdate();

        // Max air limit.
        if (airSupply > airSupplyMax)
        {
            airSupply = airSupplyMax;
        }

        // Min air limit.
        if (airSupply <= airSupplyMin)
        {
            isDead = true;
        }
    }



    // Death
    private void DeathCheck()
    {
        if (isDead)
        {
            Debug.Log("Player is dead");
            //Add player lose scenario here
        }
    }

}
