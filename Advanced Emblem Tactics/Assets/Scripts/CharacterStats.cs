using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public string Name;
    public int maxHealth;
    public int currHealth;
    public int movementSpeed;
    public GameObject[] Inventory = new GameObject[4];
    public int baseAttack = 1;
    public int currAttack;
    public GameObject equipedWeapon;

    void Start(){
        equipedWeapon = Inventory[0]; 
    }
    
    void Update() {
        if (currHealth <= 0){
            Destroy(this.gameObject);
        }

        currAttack = baseAttack + equipedWeapon.GetComponent<WeaponStats>().attack;
    }
}
