using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackBoxScript : MonoBehaviour
{
    public Text enemyNameText;
    public Text enemyHpText;
    public Text weaponText;
    public GameObject enemyUnit;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update() {
        enemyNameText.text = "Enemy Unit: " + enemyUnit.GetComponent<CharacterStats>().Name;
        enemyHpText.text = "HP: " + enemyUnit.GetComponent<CharacterStats>().currHealth + "/ " + enemyUnit.GetComponent<CharacterStats>().maxHealth;
        weaponText.text = enemyUnit.GetComponent<CharacterStats>().equipedWeapon.name; 
    }

    public void AttackButton(){
        GameObject.FindGameObjectWithTag("Map").GetComponent<GridScript>().DoDamage();
        GameObject.FindGameObjectWithTag("Map").GetComponent<GridScript>().TurnOffGrid();
        this.gameObject.SetActive(false);
    }
    public void CancelButton(){
        GameObject.FindGameObjectWithTag("Map").GetComponent<GridScript>().TurnOffGrid();
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<UnitSelectorScript>().unitInfo.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
