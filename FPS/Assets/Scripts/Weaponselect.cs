using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weaponselect : MonoBehaviour
{
    public GameObject player;
    public GameObject weapon;
    public void OnTriggerEnter(Collider other){
        //触れたオブジェクトがプレイヤーの時
        if(other.name == player.name){
            //メイン武器を変更
            player.GetComponent<Playerstatus>().Weaponchange(weapon);
        }
    }
}
