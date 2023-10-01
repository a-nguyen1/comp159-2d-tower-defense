using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        GameObject child = gameObject.transform.GetChild(0).gameObject; // canvas
        child = child.transform.GetChild(1).gameObject; // buy button
        child = child.transform.GetChild(0).gameObject; // buy button text
        TextMeshProUGUI textLabel = child.GetComponent<TextMeshProUGUI>(); // buy button text label
        if (!towerExists) // TODO check if player has enough currency to buy tower
        {
            // TODO add UI to buy tower
            currentTower = Instantiate(tower, transform.localPosition, Quaternion.identity);
            towerExists = true;
            if (textLabel.text.Contains("Buy Turret"))
            {
                textLabel.SetText("Sell Turret");
            }
        }
        else
        {
            // TODO add money when selling tower
            if (textLabel.text.Contains("Sell Turret"))
            {
                textLabel.SetText("Buy Turret");
            }
            Destroy(currentTower);
            towerExists = false;
        }
    }

    
}
