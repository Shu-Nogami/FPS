using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammohit : MonoBehaviour
{
    public bool playerbool;
    public bool cpubool;
    public GameObject cpu;
    public GameObject player;
    public AudioClip sound;
    private AudioSource audioSource;
    public GameObject confirmaion;

    private void OnTriggerEnter(Collider other){
        //当てられた対象がプレイヤーかつ、当たったオブジェクトがB側の弾丸の時
        if(playerbool == true && other.tag == "sideBammo"){
            //弾丸のダメージ分プレイヤーの体力を減らす
            player.GetComponent<Playerstatus>().HitDamage(other.GetComponent<ammospeed>().damage);
        }
        //当てられた対象がCPUかつ、A側のCPUがB側の弾丸が当たったまたは、B側のCPUがA側の弾丸が当たった時
        else if(cpubool == true && ((cpu.tag == "sideA" && other.tag == "sideBammo") || (cpu.tag == "sideB" && other.tag == "sideAammo"))){
            Debug.Log("hitCPU");
            //弾丸のダメージ分CPUの体力を減らす
            cpu.GetComponent<cpuagent>().hp -= other.GetComponent<ammospeed>().damage;
        }
        //当てられた対象が建築物かつ、当たったオブジェクトが弾丸の時
        else if(tag == "Object" && (other.tag == "sideAammo" || other.tag == "sideBammo")){
            //設定されたサウンドを再生
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = sound;
            audioSource.Play();
        }
        //当てられた対象がTrainingにあるターゲットかつ、当たったオブジェクトがA側の弾丸の時
        else if(tag == "Training Object" && other.tag == "sideAammo"){
            //設定されたサウンドを再生
            audioSource = confirmaion.GetComponent<AudioSource>();
            audioSource.Play();
        }
        //当たったオブジェクトが弾丸の時
        if(other.tag == "sideAammo" || other.tag == "sideBammo"){
            //弾丸削除
            other.GetComponent<ammospeed>().Destroy();
        }
    }
}
