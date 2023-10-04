using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomeController : MonoBehaviour
{
    [SerializeField] private int bankTotal = 100;
    [SerializeField] private int enemyGold = 5;
    [SerializeField] private int roundGold = 25;
    private GameObject gameController;
    // Start is called before the first frame update
    public void EnemyDown()
    {
        bankTotal += enemyGold;
        gameController.GetComponent<GameController>().BankChange();
    }

    public void RoundEndGold()
    {
        bankTotal += roundGold;
        gameController.GetComponent<GameController>().BankChange();
    }

    public int BankTotalReturn()
    {
        return bankTotal;
    }

    public void BoughtTower()
    {
        bankTotal -= 50;
    }

    public void SoldTower()
    {
        bankTotal += 25;
    }
    
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Use this to check that when calling above functions, banktotal is properly updating
        //Debug.Log( bankTotal);
    }
}
