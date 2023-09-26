using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private float enemySpeed;
    private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 rangeLocation = enemy.transform.InverseTransformPoint(player.transform.position); //Gets location of dome relative to enemyObject
        
        Rigidbody2D enemyBody = enemy.GetComponent<Rigidbody2D>(); //Grabs rigidbody from enemy to move it
        
        enemyBody.AddForce(rangeLocation.normalized*enemySpeed); //Moves enemy towards dome
    }
    
}
