using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTest : MonoBehaviour
{
    public bool unitMoved;
    public bool unitAttacked;
    public int currentTileX;
    public int currentTileZ;
    public GameObject lastTile;
    public Material normalColor;
    public Material selectedColor;
    public Material usedColor;
    // Start is called before the first frame update
    void Start()
    {
        GetCurrentTile();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void unitWasClicked(){
        gameObject.tag = "ActiveUnit";
        GetCurrentTile();
        StateCheck();
    }

    public void wasNotClicked(){
        GameObject.FindGameObjectWithTag("Map").GetComponent<GridScript>().SelectedUnit = null;
        gameObject.tag = "PlayerUnit";
        StateCheck();   
    }

    public void StateCheck(){
        if(unitMoved == true){
            gameObject.GetComponent<Renderer>().material = usedColor;
        }else if(gameObject.tag == "ActiveUnit"){
            gameObject.GetComponent<Renderer>().material = selectedColor;
        }else{
            gameObject.GetComponent<Renderer>().material = normalColor;
        }
    }

    public void GetCurrentTile(){
       Collider[] hitTiles = Physics.OverlapSphere(gameObject.transform.position,0.2f);
       if(hitTiles.Length > 0){
           for(int i=0; i < hitTiles.Length; i++){
               Debug.Log(hitTiles[i]);
               if(hitTiles[i].transform.tag == "Tile"){
                   currentTileX = Mathf.RoundToInt(hitTiles[i].gameObject.transform.position.x);
                   currentTileZ = Mathf.RoundToInt(hitTiles[i].gameObject.transform.position.z);
                   lastTile = hitTiles[i].gameObject;
                   lastTile.GetComponent<TileInfo>().isWalkable = false;
               }
            }
        }else{
            Debug.Log("nothing hit");
        }
       
    }
    
}
