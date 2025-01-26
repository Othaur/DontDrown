using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Howto: MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadSceneAsync(3);
    }
}
