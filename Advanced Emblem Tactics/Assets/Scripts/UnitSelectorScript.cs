﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelectorScript : MonoBehaviour
{

    public GameObject unitInfo;
    public GameObject AttackMenu;
    public GameObject InventoryMenu;
    public GameObject TurnBox;
    public bool uiActive;
    // Start is called before the first frame update
    void Start()
    {
        uiActive = false;
        TurnBox.SetActive(false);
        unitInfo.SetActive(false);
        AttackMenu.SetActive(false);
        InventoryMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }
        if(Input.GetMouseButtonDown(0)){
            Ray ray = gameObject.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit)){
                
                Debug.Log(hit.collider.tag);

                if(hit.collider.tag == "PlayerUnit"){

                    GameObject[] others = GameObject.FindGameObjectsWithTag("ActiveUnit");
                    foreach (GameObject go in others)
                    {
                    go.GetComponent<PlayerMovementTest>().wasNotClicked();
                    }
                    
                    hit.collider.gameObject.GetComponent<PlayerMovementTest>().unitWasClicked();
                    GameObject.FindGameObjectWithTag("Map").GetComponent<GridScript>().TurnOffGrid();
                    unitInfo.SetActive(true);
                    uiActive = true;
                
                    
                }else if(hit.collider.tag == "ActiveUnit"){
                    if(unitInfo.activeSelf == false){
                        GameObject.FindGameObjectWithTag("Map").GetComponent<GridScript>().TurnOffGrid();
                        unitInfo.SetActive(true);
                        uiActive = true;
                    }
                }else if(hit.collider.tag == "EnemyUnit"){
                    GameObject.FindGameObjectWithTag("Map").GetComponent<GridScript>().selectedEnemyUnit = hit.collider.gameObject;
                    GameObject.FindGameObjectWithTag("Map").GetComponent<GridScript>().CheckInRange();
                    if(hit.collider.gameObject.GetComponent<EnemyMovementScript>().inRange == true){
                        if (unitInfo.activeSelf == true)
                        {
                            unitInfo.SetActive(false);
                        }
                        AttackMenu.GetComponent<AttackBoxScript>().enemyUnit = hit.collider.gameObject;
                        AttackMenu.SetActive(true);
                        uiActive = true;
                    } 
                }else{
                    //if you do hit something, but I don't care about it, display the endturn button
                    if(uiActive == true){

                    }else{
                        TurnBox.SetActive(true); 
                    }
                }            
            }else{
                //if you don't hit anything, display the endturn button
                if(uiActive == true){

                }else{
                   TurnBox.SetActive(true); 
                }
                
            }
        }
    }

    public void OptionBoxInventory(){
        unitInfo.SetActive(false);
        InventoryMenu.SetActive(true);
    }

    
}