using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TurretController : MonoBehaviour
{
    [SerializeField] private GameObject turret;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float fireRatePerSecond;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private int bulletLifetime;

    private Vector2 turretLocation;
    private GameObject nearestEnemy;
    // Start is called before the first frame update
    void Start()
    {
        turretLocation = turret.transform.position;
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
    
    private void FireBullet(Vector2 bulletTarget)
    {
        GameObject newBullet = Instantiate(bullet, turretLocation, Quaternion.identity);
        newBullet.GetComponent<BulletController>().Initialize(bulletTarget, bulletSpeed, bulletLifetime);
    }

    private Vector2 NearestEnemy(GameObject[] enemies)
    {
        var position = enemies[0].transform.position;
        Vector2 nearestEnemyPosition = new Vector2(position.x, position.y);
        for (int i = 1; i < enemies.Length; i++)
        {
            float xDifference = Mathf.Abs(turretLocation.x - enemies[i].transform.position.x);
            float nearestXDifference = Mathf.Abs(turretLocation.x - nearestEnemyPosition.x);
            float yDifference = Mathf.Abs(turretLocation.y - enemies[i].transform.position.y);
            float nearestYDifference = Mathf.Abs(turretLocation.y - nearestEnemyPosition.y);
            if (xDifference * xDifference + yDifference * yDifference < nearestXDifference * nearestXDifference +
                nearestYDifference * nearestYDifference)
            {
                nearestEnemyPosition.x = enemies[i].transform.position.x;
                nearestEnemyPosition.y = enemies[i].transform.position.y;
            }
        }
        Debug.Log($"enemy pos: {nearestEnemyPosition}");
        return nearestEnemyPosition;
    }
}
