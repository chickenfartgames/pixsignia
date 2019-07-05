using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class GridScript : MonoBehaviour
{
    public GameObject SelectedUnit;
    public GameObject[] allTiles;
    public GameObject[,] sortedTiles;
    public List<GameObject> inRangeTiles;
    public List<GameObject> attackRangeTiles;
    public GameObject selectedEnemyUnit;
    public GameObject idealTile;
    public int MapSizeZ = 0;
    public int MapSizeX = 0;
    void Start(){
        SelectedUnit = null;
        inRangeTiles = new List<GameObject>();
        attackRangeTiles = new List<GameObject>();
        CreateGrid();
    }

    void CreateGrid(){

        GameObject temp;
        int i = 0;
        allTiles = new GameObject[transform.childCount];

        foreach (Transform child in transform){
            allTiles[i] = child.gameObject;
            i += 1;
        }

        // sort the tiles to find the lowest X position
        for(int x = 0; x < allTiles.Length - 1; x++){
            for(int j = x+1; j < allTiles.Length; j++){
                if(allTiles[x].gameObject.transform.position.x > allTiles[j].gameObject.transform.position.x){
                    temp = allTiles[x];
                    allTiles[x] = allTiles[j];
                    allTiles[j] = temp;
                }
            }
        }

        //sort again to find the lowest Z position
        for(int x = 0; x < allTiles.Length - 1; x++){
            for(int j = x+1; j < allTiles.Length; j++){
                if(allTiles[x].gameObject.transform.position.z > allTiles[j].gameObject.transform.position.z){
                    temp = allTiles[x];
                    allTiles[x] = allTiles[j];
                    allTiles[j] = temp;
                }
            }
        }

        //get the amount of rows
        int tempRow = Mathf.RoundToInt(allTiles[0].gameObject.transform.position.z);
        for(int x = 0; x < allTiles.Length; x++){
               if(allTiles[x].gameObject.transform.position.z == tempRow){
                    MapSizeZ++;
                    tempRow++;
                } 
        }
            

        //get the amount of columns
        int tempColumn = Mathf.RoundToInt(allTiles[0].gameObject.transform.position.x);
        for(int x = 0; x < allTiles.Length; x++){
               if(allTiles[x].gameObject.transform.position.x == tempColumn){
                    MapSizeX++;
                    tempColumn++;
                } 
        }       

        sortedTiles = new GameObject[MapSizeX,MapSizeZ]; 
        foreach (GameObject child in allTiles)
        {
            child.gameObject.GetComponent<TileInfo>().TileX = Mathf.RoundToInt(child.transform.position.x);
            child.gameObject.GetComponent<TileInfo>().TileZ = Mathf.RoundToInt(child.transform.position.z);
            int _TileX = child.gameObject.GetComponent<TileInfo>().TileX;
            int _TileZ = child.gameObject.GetComponent<TileInfo>().TileZ;
            sortedTiles[_TileX,_TileZ] = child.gameObject;
            bool walkable = child.gameObject.GetComponent<TileInfo>().isWalkable;
        }
            
        
    }

    void OnDrawGizmos() {
        if(allTiles != null){
            foreach(GameObject child in allTiles){
                Gizmos.DrawCube(child.transform.position, new Vector3(0.1f,0.1f,0.1f));
            }
        }
    }

    public void MoveSelectedUnitTo(int x, int z){
        if(SelectedUnit != null){
            if(SelectedUnit.GetComponent<PlayerMovementTest>().unitMoved != true){
                TurnOffGrid();
                int tempX = SelectedUnit.GetComponent<PlayerMovementTest>().currentTileX;
                int tempZ = SelectedUnit.GetComponent<PlayerMovementTest>().currentTileZ;
                sortedTiles[tempX, tempZ].GetComponent<TileInfo>().isWalkable = true;
                SelectedUnit.transform.position = new Vector3(x,0.2f,z);
                SelectedUnit.GetComponent<PlayerMovementTest>().unitMoved = true;
                SelectedUnit.GetComponent<PlayerMovementTest>().GetCurrentTile();
                SelectedUnit.GetComponent<PlayerMovementTest>().wasNotClicked();
            }
            
        }
        
    }


    public void GenerateMovementRange(int range, int tempX, int tempZ){
        
        for(int i = 0; i <= range; i++){
            for(int j = 0; j <= range; j++){
                if(tempX-i >= 0){
                    if(sortedTiles[tempX-i,tempZ].GetComponent<TileInfo>().isWalkable == true){
                        inRangeTiles.Add(sortedTiles[tempX-i,tempZ]);
                        sortedTiles[tempX-i,tempZ].GetComponent<TileInfo>().inRange = true;
                        sortedTiles[tempX-i,tempZ].GetComponent<TileInfo>().StateCheck();
                    }
                }
                if (tempX-i >= 0 && tempZ-j >= 0){
                    if(sortedTiles[tempX-i,tempZ-j].GetComponent<TileInfo>().isWalkable == true){
                        inRangeTiles.Add(sortedTiles[tempX-i,tempZ-j]);
                        sortedTiles[tempX-i,tempZ-j].GetComponent<TileInfo>().inRange = true;
                        sortedTiles[tempX-i,tempZ-j].GetComponent<TileInfo>().StateCheck();
                    }
                }
                if(tempX-i >= 0 && tempZ+j <= MapSizeZ-1){
                    if(sortedTiles[tempX-i,tempZ+j].GetComponent<TileInfo>().isWalkable == true){
                        inRangeTiles.Add(sortedTiles[tempX-i,tempZ+j]);
                        sortedTiles[tempX-i,tempZ+j].GetComponent<TileInfo>().inRange = true;
                        sortedTiles[tempX-i,tempZ+j].GetComponent<TileInfo>().StateCheck();
                    } 
                }
                if(tempX+i <= MapSizeX-1){
                    if(sortedTiles[tempX+i,tempZ].GetComponent<TileInfo>().isWalkable == true){
                        inRangeTiles.Add(sortedTiles[tempX+i,tempZ]);
                        sortedTiles[tempX+i,tempZ].GetComponent<TileInfo>().inRange = true;
                        sortedTiles[tempX+i,tempZ].GetComponent<TileInfo>().StateCheck();
                    } 
                }
                if(tempX+i <= MapSizeX-1 && tempZ+j <= MapSizeZ-1){
                    if(sortedTiles[tempX+i,tempZ+j].GetComponent<TileInfo>().isWalkable == true){
                        inRangeTiles.Add(sortedTiles[tempX+i,tempZ+j]);
                        sortedTiles[tempX+i,tempZ+j].GetComponent<TileInfo>().inRange = true;
                        sortedTiles[tempX+i,tempZ+j].GetComponent<TileInfo>().StateCheck();
                    }
                }
                if(tempZ-j >= 0){
                    if(sortedTiles[tempX,tempZ-j].GetComponent<TileInfo>().isWalkable == true){
                       inRangeTiles.Add(sortedTiles[tempX,tempZ-j]);
                        sortedTiles[tempX,tempZ-j].GetComponent<TileInfo>().inRange = true;
                        sortedTiles[tempX,tempZ-j].GetComponent<TileInfo>().StateCheck(); 
                    } 
                }
                if(tempZ+j <= MapSizeZ-1){
                    if(sortedTiles[tempX,tempZ+j].GetComponent<TileInfo>().isWalkable == true){
                        inRangeTiles.Add(sortedTiles[tempX,tempZ+j]);
                        sortedTiles[tempX,tempZ+j].GetComponent<TileInfo>().inRange = true;
                        sortedTiles[tempX,tempZ+j].GetComponent<TileInfo>().StateCheck();
                    }  
                }
                if(tempZ-j >= 0 && tempX+i <= MapSizeX-1){
                    if(sortedTiles[tempX+i,tempZ-j].GetComponent<TileInfo>().isWalkable == true){
                        inRangeTiles.Add(sortedTiles[tempX+i,tempZ-j]);
                        sortedTiles[tempX+i,tempZ-j].GetComponent<TileInfo>().inRange = true;
                        sortedTiles[tempX+i,tempZ-j].GetComponent<TileInfo>().StateCheck();
                    }
                }
            }
           
        }
    }

    public void GenerateAttackRange(int tempX, int tempZ){
        int range = 1;

        for(int i = 0; i <= range; i++){
            for(int j = 0; j <= range; j++){
                if(tempX > 0){
                    attackRangeTiles.Add(sortedTiles[tempX-i,tempZ]);
                    sortedTiles[tempX-i,tempZ].GetComponent<TileInfo>().attackRange = true;
                    sortedTiles[tempX-i,tempZ].GetComponent<TileInfo>().StateCheck();
                }
                if (tempX > 0 && tempZ > 0){
                    attackRangeTiles.Add(sortedTiles[tempX-i,tempZ-j]);
                    sortedTiles[tempX-i,tempZ-j].GetComponent<TileInfo>().attackRange = true;
                    sortedTiles[tempX-i,tempZ-j].GetComponent<TileInfo>().StateCheck();
                }
                if(tempX > 0 && tempZ+i <= MapSizeZ-1){
                    attackRangeTiles.Add(sortedTiles[tempX-i,tempZ+j]);
                    sortedTiles[tempX-i,tempZ+j].GetComponent<TileInfo>().attackRange = true;
                    sortedTiles[tempX-i,tempZ+j].GetComponent<TileInfo>().StateCheck();
                }
                if(tempX+i <= MapSizeX-1){
                    attackRangeTiles.Add(sortedTiles[tempX+i,tempZ]);
                    sortedTiles[tempX+i,tempZ].GetComponent<TileInfo>().attackRange = true;
                    sortedTiles[tempX+i,tempZ].GetComponent<TileInfo>().StateCheck();
                }
                if(tempX+i <= MapSizeX-1 && tempZ+i <= MapSizeZ-1){
                    attackRangeTiles.Add(sortedTiles[tempX+i,tempZ+j]);
                    sortedTiles[tempX+i,tempZ+j].GetComponent<TileInfo>().attackRange = true;
                    sortedTiles[tempX+i,tempZ+j].GetComponent<TileInfo>().StateCheck();
                }
                if(tempZ > 0){
                    attackRangeTiles.Add(sortedTiles[tempX,tempZ-j]);
                    sortedTiles[tempX,tempZ-j].GetComponent<TileInfo>().attackRange = true;
                    sortedTiles[tempX,tempZ-j].GetComponent<TileInfo>().StateCheck();
                }
                if(tempZ+i <= MapSizeZ-1){
                    attackRangeTiles.Add(sortedTiles[tempX,tempZ+j]);
                    sortedTiles[tempX,tempZ+j].GetComponent<TileInfo>().attackRange = true;
                    sortedTiles[tempX,tempZ+j].GetComponent<TileInfo>().StateCheck();
                }
                if(tempZ > 0 && tempX+i <= MapSizeX-1){
                    attackRangeTiles.Add(sortedTiles[tempX+i,tempZ-j]);
                    sortedTiles[tempX+i,tempZ-j].GetComponent<TileInfo>().attackRange = true;
                    sortedTiles[tempX+i,tempZ-j].GetComponent<TileInfo>().StateCheck();
                }
            }
           
        }
    }

    public void CheckInRange(){
        foreach( GameObject tile in attackRangeTiles){
            if(selectedEnemyUnit.transform.position.x == tile.transform.position.x && selectedEnemyUnit.transform.position.z == tile.transform.position.z ){
                selectedEnemyUnit.GetComponent<EnemyMovementScript>().inRange = true;
            }
        }
    }

    public void TurnOffGrid(){
        foreach(GameObject tile in inRangeTiles){
            tile.GetComponent<TileInfo>().inRange = false;
            tile.GetComponent<TileInfo>().StateCheck();
        }

        inRangeTiles.Clear();

        foreach(GameObject tile in attackRangeTiles){
            tile.GetComponent<TileInfo>().attackRange = false;
            tile.GetComponent<TileInfo>().StateCheck();
        }

        attackRangeTiles.Clear();

    }

    public void ClearTiles(List<GameObject> enemyUnits, List<GameObject> playerUnits){
        foreach(GameObject tile in allTiles){
            tile.GetComponent<TileInfo>().isWalkable = true;
        }
        foreach(GameObject unit in enemyUnits){
            unit.GetComponent<EnemyMovementScript>().GetCurrentTile();
        }
        foreach(GameObject unit in playerUnits){
            unit.GetComponent<PlayerMovementTest>().GetCurrentTile();
        }
    }

    public void DoDamage(){
        GameObject enemyWeapon = selectedEnemyUnit.GetComponent<CharacterStats>().equipedWeapon;
        GameObject unitWeapon = SelectedUnit.GetComponent<CharacterStats>().equipedWeapon;
        int tempAttack = SelectedUnit.GetComponent<CharacterStats>().currAttack;

        if(unitWeapon.name == enemyWeapon.name){
            tempAttack = tempAttack * 1;
        }else if(unitWeapon.name == "Sword" && enemyWeapon.name == "Spear"){
            tempAttack = tempAttack * 2;
        }else if(unitWeapon.name == "Sword" && enemyWeapon.name == "Axe"){
            tempAttack = tempAttack/2;
        }else if(unitWeapon.name == "Spear" && enemyWeapon.name == "Sword"){
            tempAttack = tempAttack/2;
        }else if(unitWeapon.name == "Spear" && enemyWeapon.name == "Axe"){
            tempAttack = tempAttack*2;
        }else if(unitWeapon.name == "Axe" && enemyWeapon.name == "Sword"){
            tempAttack = tempAttack*2;
        }else if(unitWeapon.name == "Axe" && enemyWeapon.name == "Spear"){
            tempAttack = tempAttack/2;
        }
        
        selectedEnemyUnit.GetComponent<CharacterStats>().currHealth -= tempAttack;
        selectedEnemyUnit.GetComponent<EnemyMovementScript>().inRange = false;
        SelectedUnit.GetComponent<PlayerMovementTest>().unitAttacked = true;

    }

    public void MoveEnemy(){
        GenerateMovementRange(selectedEnemyUnit.GetComponent<CharacterStats>().movementSpeed,
        selectedEnemyUnit.GetComponent<EnemyMovementScript>().currentTileX, selectedEnemyUnit.GetComponent<EnemyMovementScript>().currentTileZ);

        GameObject _currTarget = selectedEnemyUnit.GetComponent<EnemyMovementScript>().currTarget;

        if(Vector3.Distance(selectedEnemyUnit.transform.position, _currTarget.transform.position) <= 1f){
            AttackPlayerUnit(_currTarget);
        }else{
            inRangeTiles = inRangeTiles.OrderBy(x => Vector3.Distance(x.transform.position, _currTarget.transform.position)).ToList();

            
            idealTile = inRangeTiles[0];
            
        }

            int TileX = Mathf.RoundToInt(idealTile.transform.position.x);
            int TileZ = Mathf.RoundToInt(idealTile.transform.position.z);

            MoveEnemyUnitTo(TileX, TileZ);
        
    }


    public void MoveEnemyUnitTo(int x, int z){
        TurnOffGrid();
        int tempX = selectedEnemyUnit.GetComponent<EnemyMovementScript>().currentTileX;
        int tempZ = selectedEnemyUnit.GetComponent<EnemyMovementScript>().currentTileZ;
        sortedTiles[tempX, tempZ].GetComponent<TileInfo>().isWalkable = true;
        selectedEnemyUnit.transform.position = new Vector3(x,0.2f,z);
        selectedEnemyUnit.GetComponent<EnemyMovementScript>().GetCurrentTile();   
    }

    public void AttackPlayerUnit(GameObject target){
        TurnOffGrid();
        selectedEnemyUnit.GetComponent<EnemyMovementScript>().GetCurrentTile(); 
        GameObject enemyWeapon = selectedEnemyUnit.GetComponent<CharacterStats>().equipedWeapon;
        GameObject unitWeapon = target.GetComponent<CharacterStats>().equipedWeapon;
        int tempAttack = selectedEnemyUnit.GetComponent<CharacterStats>().currAttack;

        if(enemyWeapon.name == unitWeapon.name){
            tempAttack = tempAttack * 1;
        }else if(enemyWeapon.name == "Sword" && unitWeapon.name == "Spear"){
            tempAttack = tempAttack * 2;
        }else if(enemyWeapon.name == "Sword" && unitWeapon.name == "Axe"){
            tempAttack = tempAttack/2;
        }else if(enemyWeapon.name == "Spear" && unitWeapon.name == "Sword"){
            tempAttack = tempAttack/2;
        }else if(enemyWeapon.name == "Spear" && unitWeapon.name == "Axe"){
            tempAttack = tempAttack*2;
        }else if(enemyWeapon.name == "Axe" && unitWeapon.name == "Sword"){
            tempAttack = tempAttack*2;
        }else if(enemyWeapon.name == "Axe" && unitWeapon.name == "Spear"){
            tempAttack = tempAttack/2;
        }
        
        target.GetComponent<CharacterStats>().currHealth -= tempAttack;
        selectedEnemyUnit.GetComponent<EnemyMovementScript>().unitAttacked = true;
    }

}
