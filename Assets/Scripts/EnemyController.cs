using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private float enemySpeed;
    
    private Vector3 targetPosition;
    private Rigidbody2D enemyBody;
    private GameObject player;
    private bool targetBase = true;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyBody = enemy.GetComponent<Rigidbody2D>(); //Grabs rigidbody from enemy to move it
        player = GameObject.FindGameObjectWithTag("Player");
        targetPosition = player.transform.position;
        // enemy has 50/50 chance of targeting either the dome or the nearest tower
        if (Random.Range(0, 2) != 1) return;
        targetBase = false;
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
        if (towers.Length > 0)
        {
            targetPosition = NearestTower(towers);
        }
    }

    void FixedUpdate()
    {
        Vector2 force = enemy.transform.InverseTransformPoint(targetPosition); //Gets location of target relative to enemyObject
        
        enemyBody.AddForce(force.normalized*enemySpeed); //Moves enemy towards target
    }

    public void TargetNextTower()
    {
        if (!targetBase)
        {
            GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
            if (towers.Length > 0)
            {
                targetPosition = NearestTower(towers);
            }
            else if (player != null)
            {
                targetPosition = player.transform.position;
            }
        }
    }
    
    private Vector2 NearestTower(GameObject[] towers)
    {
        var position = towers[0].transform.position;
        Vector2 nearestTowerPosition = new Vector2(position.x, position.y);
        for (int i = 1; i < towers.Length; i++)
        {
            Vector2 enemyLocation = enemy.transform.position;
            float xDifference = Mathf.Abs(enemyLocation.x - towers[i].transform.position.x);
            float nearestXDifference = Mathf.Abs(enemyLocation.x - nearestTowerPosition.x);
            float yDifference = Mathf.Abs(enemyLocation.y - towers[i].transform.position.y);
            float nearestYDifference = Mathf.Abs(enemyLocation.y - nearestTowerPosition.y);
            if (xDifference * xDifference + yDifference * yDifference < nearestXDifference * nearestXDifference +
                nearestYDifference * nearestYDifference)
            {
                nearestTowerPosition.x = towers[i].transform.position.x;
                nearestTowerPosition.y = towers[i].transform.position.y;
            }
        }
        return nearestTowerPosition;
    }
    
}
