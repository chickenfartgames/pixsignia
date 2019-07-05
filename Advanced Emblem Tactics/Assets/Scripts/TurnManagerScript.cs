using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TurnManagerScript : MonoBehaviour
{

    public List<GameObject> EnemyUnits;
    public List<GameObject> PlayerUnits;
    public Text turnText;
    public int turnCounter = 1;
    public bool gameActive = true;
    // Start is called before the first frame update
    void Start()
    {
        PlayerUnits = new List<GameObject>();
        EnemyUnits = new List<GameObject>();

        EnemyUnits.AddRange(GameObject.FindGameObjectsWithTag("EnemyUnit"));
        PlayerUnits.AddRange(GameObject.FindGameObjectsWithTag("PlayerUnit"));

        
    }

    // Update is called once per frame
    void Update()
    {
        turnText.text = "Turn : " + turnCounter;

        if(PlayerUnits[0].gameObject == null){
            gameActive = false;
            LoseTheGame();
        }
        if(EnemyUnits[0].gameObject == null){
            gameActive = false;
            WinTheGame();
        }

    }

    public void NextTurn(){
        if(gameActive == false){

        }else{
            PlayerUnits = new List<GameObject>();
            PlayerUnits.AddRange(GameObject.FindGameObjectsWithTag("PlayerUnit"));
            foreach (GameObject Unit in PlayerUnits)
            {
                Unit.GetComponent<PlayerMovementTest>().unitMoved = false;
                Unit.GetComponent<PlayerMovementTest>().unitAttacked = false;
                Unit.GetComponent<PlayerMovementTest>().StateCheck();
            }
            EnemyTurn();
            GameObject.FindGameObjectWithTag("Map").GetComponent<GridScript>().ClearTiles(EnemyUnits, PlayerUnits);
            turnCounter++;
        }
       
    }

    public void EnemyTurn(){
        EnemyUnits = new List<GameObject>();
        EnemyUnits.AddRange(GameObject.FindGameObjectsWithTag("EnemyUnit"));
        foreach (GameObject enemy in EnemyUnits)
        {  
            for(int x = 0; x < PlayerUnits.Count-1; x++)
            {
                float dist1 = Vector3.Distance(PlayerUnits[x].transform.position,enemy.transform.position);
                float dist2 = Vector3.Distance(PlayerUnits[x+1].transform.position,enemy.transform.position);
                
                if (dist1 < dist2){
                    enemy.GetComponent<EnemyMovementScript>().currTarget = PlayerUnits[x].gameObject;
                }else{
                    enemy.GetComponent<EnemyMovementScript>().currTarget = PlayerUnits[x+1].gameObject;
                }
                
            }
            if(PlayerUnits.Count == 1){
                enemy.GetComponent<EnemyMovementScript>().currTarget = PlayerUnits[0].gameObject;
            }
            if(PlayerUnits.Count == 0){
                LoseTheGame();
            }
                 
        }

        foreach (GameObject enemy in EnemyUnits)
        {
            GameObject.FindGameObjectWithTag("Map").GetComponent<GridScript>().selectedEnemyUnit = enemy.gameObject;
            enemy.GetComponent<EnemyMovementScript>().GetCurrentTile();
            if(Vector3.Distance(enemy.transform.position, enemy.GetComponent<EnemyMovementScript>().currTarget.transform.position) <= 1f){
                GameObject.FindGameObjectWithTag("Map").GetComponent<GridScript>().AttackPlayerUnit(enemy.GetComponent<EnemyMovementScript>().currTarget);
            }else{
                GameObject.FindGameObjectWithTag("Map").GetComponent<GridScript>().MoveEnemy();
            }
            
            
        }


    }

    public void LoseTheGame(){
        gameActive = false;
        turnText.text = "You Lose";
        SceneManager.LoadScene(0);
    }

    public void WinTheGame(){
        gameActive = false;
        turnText.text = "You win";
        SceneManager.LoadScene(0);
    }
}
