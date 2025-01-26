using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum GameMessageType
{
    Start,
    Success,
    Failure,
}

[CreateAssetMenu(menuName = "Events/Game Message Channel")]
public class GameMessageChannelSO : ScriptableObject
{
    public UnityAction<GameMessageType> OnGameMessageRequested;

    public void RaiseEvent(GameMessageType type)
    {
        if (OnGameMessageRequested != null)
        {
            Debug.Log("Event being raised: " + type);
            OnGameMessageRequested.Invoke(type);
        }
        else
        {
            Debug.LogWarning("A game cue was requested but nobody wanted it");
        }
    }
}
