using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{

        public Color highlightColor;
    private Color normalColor;
    private Color currentColor;

    private MeshRenderer myRend;
    private bool isSelected;

    private BuildSystem buildSystem;



    private void Start()
    {
        myRend = GetComponent<MeshRenderer>();
        normalColor = myRend.material.color;//getting the color of the ground cube (green color)
        currentColor = normalColor;
    }

    public void SetBuildSystem(BuildSystem _build)//setting a ref to the build system that was passed in by the GridSpawner...this 
        //saves us from having to do a GameObject.Find("Whatever").Getcomponent<BuildSystem>()
    {
        buildSystem = _build;
    }



}