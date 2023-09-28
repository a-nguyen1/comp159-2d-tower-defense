using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomeController : MonoBehaviour
{
    [SerializeField] private int bankTotal = 100;
    [SerializeField] private int enemyGold = 5;

    [SerializeField] private int roundGold = 25;
    // Start is called before the first frame update
    public void EnemyDown()
    {
        bankTotal += enemyGold;
    }

    public void RoundEndGold()
    {
        bankTotal += roundGold;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Use this to check that when calling above functions, banktotal is properly updating
        Debug.Log( bankTotal);
    }
}
