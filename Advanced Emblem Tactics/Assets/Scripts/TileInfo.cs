using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour
{
    public enum TileType{
        Grass,
        Mountain,
        Desert
    }
    public GameObject grid;
    public bool isWalkable;
    public int TileX;
    public int TileZ;
    public bool inRange;
    public bool attackRange;
    public Material defaultColor;
    public Material inRangeColor;
    public Material attackRangeColor;

    public int walkingCost = 1;
    private void Awake() {
        grid = GameObject.FindGameObjectWithTag("Map");
    }
    private void OnMouseUp() {
        if(inRange == true){
            if(isWalkable != false){
                int TileX = Mathf.RoundToInt(gameObject.transform.position.x);
                int TileZ = Mathf.RoundToInt(gameObject.transform.position.z);
                grid.GetComponent<GridScript>().MoveSelectedUnitTo(TileX,TileZ);
            }
            
        }
        
    }
    public void StateCheck(){
        if(inRange == true){
            gameObject.GetComponent<Renderer>().material = inRangeColor;
        }else if(attackRange == true){
            gameObject.GetComponent<Renderer>().material = attackRangeColor;
        }else{
            gameObject.GetComponent<Renderer>().material = defaultColor;
        }
    }

}
