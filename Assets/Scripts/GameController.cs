using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Vector2 enemyLeftPosition = new Vector2(-11, -3);
    [SerializeField] private Vector2 enemyRightPosition = new Vector2(11, -3);
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject towerPosition;
    [SerializeField] private int waitTime = 5; //wait time for when another enemy will spawn
    [SerializeField] private int towerPositions = 5;
    // Start is called before the first frame update
    void Start()
    {
        SetUpTowerPositions();
        StartCoroutine("SpawnEnemies");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetUpTowerPositions()
    {
        for (int i = 1; i <= towerPositions; i++)
        {
            float offset = 5;
            var position = new Vector3(offset*(float)Math.Cos((float)i * Math.PI / (towerPositions + 1)),
                offset*(float)Math.Sin((float)i * Math.PI / (towerPositions + 1))-offset, -1);
            Instantiate(towerPosition, position, Quaternion.identity);
        }
    }
    
    private IEnumerator SpawnEnemies() //Coroutine to call enemy spawning function
    {
        while (true) //Will need to change true to set number in the future
        {
            SpawnEnemy();
            yield return new WaitForSeconds(waitTime);
        }
    }
    private void SpawnEnemy()
    {//Spawns two enemies on different sides of platform
        Instantiate(enemyPrefab, enemyLeftPosition, Quaternion.identity);
        Instantiate(enemyPrefab, enemyRightPosition, Quaternion.identity);
    }
}
