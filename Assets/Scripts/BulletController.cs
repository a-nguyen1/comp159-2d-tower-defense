using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ForceMode;

public class BulletController : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    private Rigidbody2D bulletBody;
    private Vector2 startPosition;
    private Vector2 targetPosition;
    private float speed;
    private float lifetime;
    private GameObject bankObject;
    private TowerController.TowerType type;
    
    public void Initialize(TowerController.TowerType bulletType, Vector2 bulletTarget, float bulletSpeed, float bulletLifetime)
    {
        targetPosition = bulletTarget;
        speed = bulletSpeed;
        lifetime = bulletLifetime;
        type = bulletType;
    }
    // Start is called before the first frame update
    public void Start()
    {
        bankObject = GameObject.FindGameObjectWithTag("Money");
        bulletBody = bullet.GetComponent<Rigidbody2D>();
        var localPosition = bullet.GetComponent<Transform>().localPosition;
        startPosition = new Vector2(localPosition.x, localPosition.y);
        Destroy(gameObject, lifetime);
    }

    void FixedUpdate()
    {
        Vector2 force = new Vector2(targetPosition.x - startPosition.x, targetPosition.y - startPosition.y);
        bulletBody.AddForce(force.normalized*speed);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        switch (type)
        {
            case TowerController.TowerType.Basic:
                Destroy(other.gameObject);
                Destroy(gameObject);
                break;
            case TowerController.TowerType.Slow:
                // slow down enemies
                var force = other.gameObject.GetComponent<Rigidbody2D>().velocity;
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(-force, (ForceMode2D)Impulse);
                Destroy(gameObject);
                break;
            case TowerController.TowerType.Bomb:
                // enlarge bullet like a bomb
                Destroy(other.gameObject);
                Transform bombTransform = bullet.GetComponent<Transform>();
                Vector3 scaledBullet = new Vector3(15, 15, 1);
                bombTransform.localScale = scaledBullet;
                // make bomb stay still and destroy it after 1 second
                bulletBody.AddForce(-bulletBody.velocity, (ForceMode2D)Impulse);
                speed = 0;
                Destroy(gameObject, 1);
                break;
        }
        bankObject.GetComponent<IncomeController>().EnemyDown();
    }

}
