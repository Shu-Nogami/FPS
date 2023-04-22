using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelect : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        //プレイヤーが指定のオブジェクトに乗った時
        if(other.name == "FPSController"){
            //シーン移動
            SceneManager.LoadScene(transform.name);
        }
    }
}