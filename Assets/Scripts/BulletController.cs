using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    private Rigidbody2D bulletBody;
    private Vector2 startPosition;
    private Vector2 targetPosition;
    private float speed;
    private int lifetime;
    private GameObject bankObject;
    
    public void Initialize(Vector2 bulletTarget, float bulletSpeed, int bulletLifetime)
    {
        targetPosition = bulletTarget;
        speed = bulletSpeed;
        lifetime = bulletLifetime;
        
    }
    // Start is called before the first frame update
    public void Start()
    {
        bankObject = GameObject.FindGameObjectWithTag("Money");
        bulletBody = bullet.GetComponent<Rigidbody2D>();
        var localPosition = bullet.GetComponent<Transform>().localPosition;
        startPosition = new Vector2(localPosition.x, localPosition.y);
        Destroy(this.gameObject, lifetime);
    }

    void FixedUpdate()
    {
        Vector2 force = new Vector2(targetPosition.x - startPosition.x, targetPosition.y - startPosition.y);
        bulletBody.AddForce(force.normalized*speed);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
        Destroy(this.gameObject);
        bankObject.GetComponent<IncomeController>().EnemyDown();
    }

}
