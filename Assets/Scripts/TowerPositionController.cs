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
    private String towerType;

    // Start is called before the first frame update
    void Start()
    {
        towerExists = false;
       
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void OnMouseDown()
    {
        GameObject child = gameObject.transform.GetChild(0).gameObject; // canvas
        child = child.transform.GetChild(1).gameObject; // buy button
        child = child.transform.GetChild(0).gameObject; // buy button text
        TextMeshProUGUI textLabel = child.GetComponent<TextMeshProUGUI>(); // buy button text label
        if (!towerExists) // TODO check if player has enough currency to buy tower
        {
            // look at value of dropdown menu to set tower type
            TMP_Dropdown dropdownMenu = FindObjectOfType<TMP_Dropdown>();
            towerType = dropdownMenu.options[dropdownMenu.value].text;
            tower.gameObject.GetComponent<TowerController>().SetTowerType(towerType);
            currentTower = Instantiate(tower, transform.localPosition, Quaternion.identity);
            towerExists = true;
            if (textLabel.text.Contains("Buy"))
            {
                textLabel.SetText("Sell Tower");
            }
        }
        else
        {
            // TODO add money when selling tower
            if (textLabel.text.Contains("Sell"))
            {
                textLabel.SetText("Buy Tower");
            }
            Destroy(currentTower);
            towerExists = false;
        }
    }

    
}
