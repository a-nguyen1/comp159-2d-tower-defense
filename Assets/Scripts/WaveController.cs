using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public int WaveTime = 10;
    [SerializeField] public int IntermissionTime = 15;
    void Awake(){
        StateManager.OnGameStateChanged += StateChangeListener;
    }

    void OnDestroy(){
        StateManager.OnGameStateChanged -= StateChangeListener;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StateChangeListener(GameState obj){
        switch(obj){
            case GameState.Setup:
                Debug.Log("Setup");
                break;
            case GameState.Intermission:

                break;
            case GameState.Round_Start:
                StartCoroutine("WaveTimer");
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

    //Coroutine to wait for wavetime seconds, then change state to intermission
    private IEnumerator WaveTimer(){
        yield return new WaitForSeconds(WaveTime);
        StateManager.Instance.UpdateState(GameState.Round_End);
    }
}
