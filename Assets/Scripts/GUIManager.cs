using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] GUIElements;

    void Start()
    {
        GUIElements = GetAllChildren(GameObject.Find("Canvas").transform);

    }

    public void ToggleGUI(bool tof){
        foreach(GameObject element in GUIElements){
            element.SetActive(tof);
        }
    }

    public void ToggleGameEndScreen(bool tof){
        foreach (GameObject element in GUIElements)
        {
            if (element.tag == "EndScreen"){
                element.SetActive(tof);
            }
        }
    }
 

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private GameObject[] GetAllChildren(Transform parent)
    {
        int childCount = parent.childCount;
        GameObject[] childrenArray = new GameObject[childCount];

        for (int i = 0; i < childCount; i++)
        {
            Transform childTransform = parent.GetChild(i);
            childrenArray[i] = childTransform.gameObject;
        }

        return childrenArray;
    }


}
