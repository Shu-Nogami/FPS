using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sceneselect : MonoBehaviour

{
    public bool lobby;
    public bool training;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other){
        if(other.name == "FPSController"){
            if(lobby == true){
                other.transform.position = new Vector3(0.123f, 1.371f, -2.46f);
                if(transform.name == "TDM"){                
                    SceneManager.LoadScene("CPU");
                }
                else if(transform.name == "Training room"){
                    SceneManager.LoadScene("Training room");
                }
            }
            else if(training == true){
                other.transform.position = new Vector3(459.01f, 1.37f, 315.3f);
                if(transform.name == "Lobby"){
                    SceneManager.LoadScene("Game Lobby");
                }
            }
        }
    }
}
