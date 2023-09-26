using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Vector2 enemyLeftPosition = new Vector2(-11, -3);
    [SerializeField] private Vector2 enemyRightPosition = new Vector2(11, -3);
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int waitTime = 5; //wait time for when another enemy will spawn
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnEnemies");
    }

    // Update is called once per frame
    void Update()
    {
        
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
