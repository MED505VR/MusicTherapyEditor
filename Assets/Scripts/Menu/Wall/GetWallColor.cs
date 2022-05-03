using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

//Makes sure we have a material component so we can add the new component
[RequireComponent(typeof(Material))]
public class GetWallColor : MonoBehaviour
{
    private Material color;     //create material var
    private GameObject[] walls; //array of all the walls

    private void SetWallColor(Material color)
    {
        color = GetComponent<WallScript>().chosenWallColor;     //get the wall color that is chosen from the dropdown menu
        if (walls == null)      //If there is no walls in the array 
            walls = GameObject.FindGameObjectsWithTag("Wall");      //fills the array with object that has the Wall tag

        for (int i = 0; i < walls.Length; i++)      //Sets the chosen material onto the walls 
        {
            //Set wall material to color variable
        }
    }
}
