using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerPositionController : MonoBehaviour
{
    [SerializeField] private GameObject tower;
    private bool towerExists;
    private GameObject currentTower;
    

    // Start is called before the first frame update
    void Start()
    {
        towerExists = false;
       
    }

    // Update is called once per frame
    void Update()
    {
    }
    //TODO disable circle collider when round starts and enable during setup/intermission
    public void OnMouseDown()
    {
        if (!towerExists) // TODO check if player has enough currency to buy/upgrade tower
        {
            // TODO add UI to buy tower
            currentTower = Instantiate(tower, transform.localPosition, Quaternion.identity);
            towerExists = true;
        }
        else
        {
            Destroy(currentTower);
            towerExists = false;
            // TODO add UI for upgrading or selling tower
        }
    }

    
}
