using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DomeManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float domeHP = 100;
    public float domeMaxHP = 100;
    public int UpgradeLevel = 1;
    private StateManager StateManagerObject;
    private SpriteRenderer domeSprite;
    private GameObject game;

    void Awake(){
        StateManager.OnGameStateChanged += StateChangeListener;
    }

    void OnDestroy(){
        StateManager.OnGameStateChanged -= StateChangeListener;
    }

     void OnTriggerEnter2D(Collider2D other){
        Debug.Log("Dome hit");
        if(other.gameObject.tag == "Enemy"){
            Debug.Log("Enemy hit");
            TakeDamage(10);
            Destroy(other.gameObject);
        }
    }

    void Start()
    {
        domeSprite = GetComponent<SpriteRenderer>();
        game = GameObject.FindGameObjectWithTag("GameController");
        StateManagerObject = GameObject.Find("GameController").GetComponent<StateManager>();
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
        //Debug.Log(StateManagerObject.GetGameState());
        
    }

    //USE THESE TO DO THINGS WITH THE DOME(tm)

    public void TakeDamage(float damage){
        domeHP -= damage;
        CheckStatus();
    }

    public void RepairDome(){
        domeHP = domeMaxHP;
    }

    private void CheckStatus(){
        
        if(domeHP <= 0 && StateManager.Instance.State != GameState.Game_Over){
            StateManager.Instance.UpdateState(GameState.Game_Over);
            domeSprite.color = new Color(255,0,0);
            game.GetComponent<GameController>().GameOver();
        }
    }

    public void UpgradeDome(int additional_level){
            UpgradeLevel+=additional_level;
            RepairDome();

    }

}
