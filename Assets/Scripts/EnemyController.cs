using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    //[SerializeField] private GameObject dome;
    [SerializeField] private float enemySpeed = 0.25f;
    [SerializeField] private Vector2 domeLocation;
    private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        domeLocation = new Vector2(1,1);
        player = GameObject.FindGameObjectWithTag("Player");
        //StartMoving();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(enemySpeed);
        Vector2 playerLocation = player.GetComponent<Transform>().position; 
        //Vector2 thisLocation = enemy.GetComponent<Transform>().position; 
        Vector2 rangeLocation = enemy.transform.InverseTransformPoint(player.transform.position);
        Rigidbody2D enemyBody = enemy.GetComponent<Rigidbody2D>();
        //enemyBody.AddForce(this.transform.forward);
        enemyBody.AddForce(rangeLocation.normalized*enemySpeed); 
    }

    void StartMoving()
    {
        Debug.Log(enemySpeed);
        Rigidbody2D enemyBody = enemy.GetComponent<Rigidbody2D>();
        enemyBody.AddForce(this.transform.forward);
        //enemyBody.AddForce(domeLocation*enemySpeed);
        //Debug.Log(domeLocation);
    }
    
}
