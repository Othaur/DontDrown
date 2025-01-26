using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        deathPanel.SetActive(true);
    }

    public void RestartLevel()
    {
        Debug.Log("Restarting Level");
        // Get the name of the current active scene
        string sceneName = SceneManager.GetActiveScene().name;
        
        // Load the scene with the same name to restart it
        SceneManager.LoadScene(sceneName);
    }


}
