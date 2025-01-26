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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("Poping back to main");
        StartCoroutine(ExampleCoroutine());

    }

    public void RestartLevel()
    {
       
        // Get the name of the current active scene
        

   
        StartCoroutine(ExampleCoroutine());

        // Load the scene with the same name to restart it

    }

    IEnumerator ExampleCoroutine()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        Debug.Log("Restarting: " + sceneName);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Title Screen");
        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
}
