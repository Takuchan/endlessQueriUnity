using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void LoadScene(){
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
    public void onCloseApp(){
        Application.Quit();
    }
}
