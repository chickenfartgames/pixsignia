using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementScript : MonoBehaviour
{
    public bool inRange;
    public bool unitAttacked;
    public int currentTileX;
    public int currentTileZ;
    public GameObject currTarget;
    public GameObject lastTile;
    // Start is called before the first frame update
    void Start()
    {
        GetCurrentTile();
    }

    // Update is called once per frame
    void Update()
    {
        
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
