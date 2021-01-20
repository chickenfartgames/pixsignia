using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Text nameText;
    public Text speedText;
    public Text hpText;
    public Text currWeaponText;
    public GameObject activeUnit;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        activeUnit = GameObject.FindGameObjectWithTag("ActiveUnit");
        if(activeUnit != null){
            nameText.text = "Name : " + activeUnit.GetComponent<CharacterStats>().Name;
            speedText.text = "Speed : " + activeUnit.GetComponent<CharacterStats>().movementSpeed;
            hpText.text = "Hp: " + activeUnit.GetComponent<CharacterStats>().currHealth + "/" + activeUnit.GetComponent<CharacterStats>().maxHealth;
            currWeaponText.text = "Current Weapon: " + activeUnit.GetComponent<CharacterStats>().equipedWeapon.name;
        } 
    }

    public void UnitToMove(){
        if(activeUnit.GetComponent<PlayerMovementTest>().unitMoved != true){
            GameObject.FindGameObjectWithTag("Map").GetComponent<GridScript>().SelectedUnit = activeUnit;
            GameObject.FindGameObjectWithTag("Map").GetComponent<GridScript>().GenerateMovementRange(activeUnit.GetComponent<CharacterStats>().movementSpeed,
                activeUnit.GetComponent<PlayerMovementTest>().currentTileX, activeUnit.GetComponent<PlayerMovementTest>().currentTileZ);
            //gameObject.SetActive(false);
        }
        
    }

    public void AttackButton(){
        if(activeUnit.GetComponent<PlayerMovementTest>().unitAttacked != true){
            GameObject.FindGameObjectWithTag("Map").GetComponent<GridScript>().SelectedUnit = activeUnit;
            GameObject.FindGameObjectWithTag("Map").GetComponent<GridScript>().GenerateAttackRange(activeUnit.GetComponent<PlayerMovementTest>().currentTileX,
                activeUnit.GetComponent<PlayerMovementTest>().currentTileZ);
            //gameObject.SetActive(false);
        }
        
    }

    public void CloseMenu(){
        if(activeUnit != null){
            activeUnit.GetComponent<PlayerMovementTest>().wasNotClicked();
        }
        GameObject.FindGameObjectWithTag("Map").GetComponent<GridScript>().TurnOffGrid();
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<UnitSelectorScript>().uiActive = false;
        gameObject.SetActive(false);
    }

    public void EndTurn(){
        if(activeUnit != null){
            activeUnit.GetComponent<PlayerMovementTest>().wasNotClicked();
        }
        GameObject.FindGameObjectWithTag("TurnManager").GetComponent<TurnManagerScript>().NextTurn();
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<UnitSelectorScript>().uiActive = false;
        gameObject.SetActive(false);
    }

}
