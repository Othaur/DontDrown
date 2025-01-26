using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitMaze : MonoBehaviour
{
    [SerializeField] GameMessageChannelSO gameChannel;
    [SerializeField] AudioSource winNoise;
    [SerializeField] GameObject winPanel;

    void Start()
    {
        winPanel.SetActive(false);
    }
    void OnTriggerExit(Collider other)
    {
        // Destroy everything that leaves the trigger
        Debug.Log("You win");
        gameChannel.RaiseEvent(GameMessageType.Success);

        winNoise.Play();
        winPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("Poping back to main");
        StartCoroutine(ExampleCoroutine());


    }



    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Title Screen");        
    }
}
