using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallScript : MonoBehaviour
{
    public Material[] wallColorlist;
    public Material chosenWallColor;
    private Dropdown dropdown;
    private GameObject walls;
    private void Start()
    {
        dropdown = transform.GetComponent<Dropdown>();

        dropdown.options.Clear();

        List<string> items = new List<string>();
        
        
        for (int i = 0; i < wallColorlist.Length; i++)
        {
            items.Add(wallColorlist[i].ToString());
        }

        foreach (var item in items)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = item });
        }
        DropdownItemSelected(dropdown);
        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
    }

    void DropdownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;
        chosenWallColor = wallColorlist[index];

    }

    
}
