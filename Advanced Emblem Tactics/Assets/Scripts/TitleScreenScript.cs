using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreenScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }
        
    }

    public void StartGame(){
        SceneManager.LoadScene(1);
    }
    public void Credits(){
        SceneManager.LoadScene(2);
    }
    public void ExitButton(){
        Application.Quit();
    }
}
