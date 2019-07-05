using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryBoxScript : MonoBehaviour
{

    public Text NameText;
    public Text BaseAttackText;
    public Text CurrentWeaponText;
    public Text CurrentWeaponAttackText;
    public GameObject Unit;
    public Text[] inventoryButtons = new Text[4];
    public GameObject[] unitInventory = new GameObject[4];
    // Start is called before the first frame update
    void Start()
    {
        Unit = GameObject.FindGameObjectWithTag("ActiveUnit");
    }

    // Update is called once per frame
    void Update()
    {
        Unit = GameObject.FindGameObjectWithTag("ActiveUnit");
        unitInventory = Unit.GetComponent<CharacterStats>().Inventory;   
        NameText.text = "Unit: " + Unit.GetComponent<CharacterStats>().Name;
        BaseAttackText.text = "Base Attack: " + Unit.GetComponent<CharacterStats>().baseAttack;
        CurrentWeaponText.text = "Current Weapon: " + Unit.GetComponent<CharacterStats>().equipedWeapon.name;
        CurrentWeaponAttackText.text = "Weapon Bonus: " + Unit.GetComponent<CharacterStats>().equipedWeapon.gameObject.GetComponent<WeaponStats>().attack;
        
        for(int x = 0; x < inventoryButtons.Length; x++){
            if(unitInventory[x] == null){
                inventoryButtons[x].text = "Empty";
            }else{
                inventoryButtons[x].text = unitInventory[x].GetComponent<WeaponStats>().name;
            }
            
        }

    }

    public void Inventory1Button(){
        if(unitInventory[0] == null){

        }else{
            Unit.GetComponent<CharacterStats>().equipedWeapon = unitInventory[0].gameObject;
        }
    }

    public void Inventory2Button(){
        if(unitInventory[1] == null){

        }else{
            Unit.GetComponent<CharacterStats>().equipedWeapon = unitInventory[1].gameObject;
        }
    }

    public void Inventory3Button(){
        if(unitInventory[2] == null){

        }else{
            Unit.GetComponent<CharacterStats>().equipedWeapon = unitInventory[2].gameObject;
        }
    }

    public void Inventory4Button(){
        if(unitInventory[3] == null){

        }else{
            Unit.GetComponent<CharacterStats>().equipedWeapon = unitInventory[3].gameObject;
        }
    }


    public void CancelButton(){
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<UnitSelectorScript>().unitInfo.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
