using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitMaze : MonoBehaviour
{
    [SerializeField] GameMessageChannelSO gameChannel;

    void OnTriggerExit(Collider other)
    {
        // Destroy everything that leaves the trigger
        Debug.Log("You win");
        gameChannel.RaiseEvent(GameMessageType.Success);
    }
}
