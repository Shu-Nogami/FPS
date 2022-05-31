using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelect : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        //プレイヤーが指定のオブジェクトに乗った時
        if(other.name == "FPSController"){
            //シーン CPUに移動
            if(transform.name == "TDM"){
                SceneManager.LoadScene("CPU");
            }
            //シーン Training rommに移動
            else if(transform.name == "Training room"){
                SceneManager.LoadScene("Training room");
            }
        }
    }
}
