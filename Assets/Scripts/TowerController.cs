using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TowerController : MonoBehaviour
{
    [SerializeField] private GameObject tower;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float fireRatePerSecond;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletLifetime;
    [SerializeField] private TowerType type = TowerType.Basic;
    
    private Vector2 towerLocation;
    private GameObject nearestEnemy;
    // Start is called before the first frame update
    void Start()
    {
        towerLocation = tower.transform.position;
        StartCoroutine("SpawnBullets");
    }

    void FixedUpdate()
    {
    }


    private IEnumerator SpawnBullets()
    {
        while (true) // TODO maybe check for game state here
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length > 0)
            {
                Vector2 bulletTarget = NearestEnemy(enemies);
                FireBullet(bulletTarget);
            }
            yield return new WaitForSeconds(1 / fireRatePerSecond);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Enemy"){
            // TODO lower tower health here
        }
    }

    private void OnDestroy()
    {
        // have enemies retarget when tower is destroyed
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyController>().TargetNextTower();
        }
    }

    private void FireBullet(Vector2 bulletTarget)
    {
        GameObject newBullet = Instantiate(bullet, towerLocation, Quaternion.identity);
        newBullet.GetComponent<BulletController>().Initialize(type, bulletTarget, bulletSpeed, bulletLifetime);
    }

    private Vector2 NearestEnemy(GameObject[] enemies)
    {
        var position = enemies[0].transform.position;
        Vector2 nearestEnemyPosition = new Vector2(position.x, position.y);
        for (int i = 1; i < enemies.Length; i++)
        {
            float xDifference = Mathf.Abs(towerLocation.x - enemies[i].transform.position.x);
            float nearestXDifference = Mathf.Abs(towerLocation.x - nearestEnemyPosition.x);
            float yDifference = Mathf.Abs(towerLocation.y - enemies[i].transform.position.y);
            float nearestYDifference = Mathf.Abs(towerLocation.y - nearestEnemyPosition.y);
            if (xDifference * xDifference + yDifference * yDifference < nearestXDifference * nearestXDifference +
                nearestYDifference * nearestYDifference)
            {
                nearestEnemyPosition.x = enemies[i].transform.position.x;
                nearestEnemyPosition.y = enemies[i].transform.position.y;
            }
        }
        return nearestEnemyPosition;
    }

    public void SetTowerType(TowerType newType)
    {
        type = newType;
    }

    public enum TowerType
    {
        Basic,
        Bomb,
        Slow,
    }
}
