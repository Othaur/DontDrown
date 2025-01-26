using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameMessageChannelSO gameChannel;


    // Start is called before the first frame update
    void Start()
    {
        gameChannel.OnGameMessageRequested += GameEventOccured;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GameEventOccured(GameMessageType type)
    {
        switch (type)
        {
            case GameMessageType.Start:
                {
                    Debug.Log("Game Started");
                    break;
                }
            case GameMessageType.Success:
                {
                    Debug.Log("Player Successful");
                    break;
                }
            case GameMessageType.Failure:
                {
                    Debug.Log("Player Failed");
                    break;
                }
            default:
                {
                    break;
                }

        }
    }
}
