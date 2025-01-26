using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathManager : MonoBehaviour
{
    [SerializeField] private AudioSource soundsOfDying;
    [SerializeField] private GameObject deathPanel;

    private void Start()
    {
        deathPanel.SetActive(false);
    }

    public void Death()
    {
        soundsOfDying.Play();
        deathPanel.SetActive(true);
    }



}
