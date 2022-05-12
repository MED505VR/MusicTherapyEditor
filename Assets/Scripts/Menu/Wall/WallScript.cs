using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallScript : MonoBehaviour
{
    public Material[] wallColorList;
    public Material chosenWallColor;
    private Dropdown dropdown;
    private GameObject walls;
    private void Start()
    {
        dropdown = transform.GetComponent<Dropdown>();      //gets dropdown component

        dropdown.options.Clear();       //Clears the list

        List<string> items = new List<string>();
        
        
        for (int i = 0; i < wallColorList.Length; i++)      //adds the color list to Dropdown list
        {
            items.Add(wallColorList[i].name);
        }

        foreach (var item in items)     //for every color in the dropdownlist add the name of the color
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = item });
        }
        DropdownItemSelected(dropdown); 
        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
    }
    

    void DropdownItemSelected(Dropdown dropdown)        //sets the value of 
    {
        int index = dropdown.value;
        chosenWallColor = wallColorList[index];
    }
}
