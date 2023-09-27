using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DomeManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float domeHP = 100;
    public float domeMaxHP = 100;

    void Awake(){
        StateManager.OnGameStateChanged += StateChangeListener;
    }

    void OnDestroy(){
        StateManager.OnGameStateChanged -= StateChangeListener;
    }

    void Start()
    {
        
    }
    // throw a bunch of switch statements in here, this is whe
    private void StateChangeListener(GameState obj){
        switch(obj){
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
