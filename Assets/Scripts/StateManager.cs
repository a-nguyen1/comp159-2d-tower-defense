using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance;
    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    void Awake(){
        Instance = this;
    }
    
    //These states can call other scripts, but don't put much else here.
    //It would be best to have other scripts listen for these events.

    public void UpdateState(GameState state){
        Debug.Log("State changted from " + State.ToString() + " to " + state.ToString());
        State = state;
        switch(state){
            case GameState.Setup:
                Debug.Log("Setup");
                break;
            case GameState.Intermission:

                break;
            case GameState.Round_Start:

                break;
            case GameState.Round_End:

                break;
            case GameState.Game_Over:

                break;
            default:
                Debug.LogError("Invalid state change");
                break;
        }

        OnGameStateChanged?.Invoke(state);
    }
    void Start()
    {
        UpdateState(GameState.Setup);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public GameState GetGameState(){
        return State;
    }

}

public enum GameState{
    Setup,
    Intermission,
    Round_Start,
    Round_End,
    Game_Over,
}
