using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    [SerializeField] private Vector2 enemyXPositions = new Vector2(-11, 11);
    [SerializeField] private Vector2 enemyYPositionRange = new Vector2(-4f, 4f);
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject towerPosition;
    [SerializeField] private int waitTime = 5; //wait time for when another enemy will spawn
    [SerializeField] private int towerPositions = 5;
    [SerializeField] private TextMeshProUGUI bankLabel;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private GameObject mainMenuButton;
    private StateManager StateManagerObject;
    private GameObject bankObject;
    private GameObject[] BuyButton;
    private GameObject[] MenuLabel;

    // Start is called before the first frame update
    void Start()
    {
        StateManagerObject = GameObject.Find("GameController").GetComponent<StateManager>();
        StateManagerObject.UpdateState(GameState.Round_Start);
        bankObject = GameObject.FindGameObjectWithTag("Money");
        bankLabel.SetText("Bank:  " + bankObject.GetComponent<IncomeController>().BankTotalReturn());
        SetUpTowerPositions();
        //StartCoroutine("SpawnEnemies");
        BuyButtonSetUp();
        LabelSetUp();
        
    }

    void Awake(){
        StateManager.OnGameStateChanged += RoundStartListener;
    }

    void OnDestroy(){
        StateManager.OnGameStateChanged -= RoundStartListener;
    }

    private void RoundStartListener(GameState obj){
        if(obj == GameState.Round_Start){
            StartCoroutine("SpawnEnemies");
        }
         if(obj == GameState.Round_End){
            StopCoroutine("SpawnEnemies");
        }

        
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetUpTowerPositions()
    {
        for (int i = 1; i <= towerPositions; i++)
        {
            float circularOffset = 6;
            float yOffset = 5;
            var position = new Vector3(circularOffset*(float)Math.Cos((float)i * Math.PI / (towerPositions + 1)),
                circularOffset*(float)Math.Sin((float)i * Math.PI / (towerPositions + 1))-yOffset, -1);
            Instantiate(towerPosition, position, Quaternion.identity);
        }
    }
    
    private IEnumerator SpawnEnemies() //Coroutine to call enemy spawning function
    {
        while (StateManagerObject.GetGameState() == GameState.Round_Start) //Will need to change true to set number in the future
        {
            SpawnEnemy();
            yield return new WaitForSeconds(waitTime);
        }
    }
    private void SpawnEnemy()
    {//Spawns two enemies on different sides of platform
        Vector3 spawnPosition = new Vector3(enemyXPositions.x, Random.Range(enemyYPositionRange.x, enemyYPositionRange.y), 0);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        spawnPosition.x = enemyXPositions.y;
        spawnPosition.y = Random.Range(enemyYPositionRange.x, enemyYPositionRange.y);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
    private void BuyButtonSetUp()
    {
        float screenWidth= Screen.width;
        float screenHeight = Screen.height;
        BuyButton = GameObject.FindGameObjectsWithTag("BuyButton");
        int j = 0;
        for (int i = 4; i >= 0; i--)
        {
            
            float xValue = screenWidth * 0.94f; // 6% from the right
            float yValue = screenHeight * (0.90f - (j * 0.2f)); // Increment by 20% height
            var position = new Vector3(xValue,yValue, -10);
            BuyButton[i].transform.position = position;
            j++;


        }
    }

    private void LabelSetUp()
    {
        
        
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        MenuLabel = GameObject.FindGameObjectsWithTag("MenuLabel");
        for (int i = 0; i < 5; i++)
        {
            
            float xValue = screenWidth * 0.95f; // 6% from the right
            float yValue = screenHeight * (0.98f - (i * 0.2f));// Increment by 20% height
            var position = new Vector3(xValue,yValue, 0);
            MenuLabel[i].transform.position = position;
        }
    }
    public void BankChange()
    {
        bankLabel.SetText("Bank:  " + bankObject.GetComponent<IncomeController>().BankTotalReturn());
    }

    public void GameOver()
    {
        //gameOverText.SetActive(true);
        //mainMenuButton.SetActive(true);
    }
}
