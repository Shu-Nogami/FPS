using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammohit : MonoBehaviour
{
    public bool isPlayer;
    public bool isCPU;
    public GameObject cpu;
    public GameObject player;
    public AudioClip sound;
    private AudioSource _audioSource;
    public GameObject confirmaion;

    private void OnTriggerEnter(Collider other){
        //当てられた対象がプレイヤーかつ、当たったオブジェクトがB側の弾丸の時
        if(isPlayer == true && other.CompareTag("sideBammo")){
            //弾丸のダメージ分プレイヤーの体力を減らす
            player.GetComponent<PlayerStatus>().HitDamage(other.GetComponent<AmmoSpeed>().damage);
        }
        //当てられた対象がCPUかつ、A側のCPUがB側の弾丸が当たったまたは、B側のCPUがA側の弾丸が当たった時
        else if(isCPU == true && ((cpu.CompareTag("sideA") && other.CompareTag("sideBammo")) || (cpu.CompareTag("sideB") && other.CompareTag("sideAammo")))){
            Debug.Log("hitCPU");
            //弾丸のダメージ分CPUの体力を減らす
            cpu.GetComponent<CPUAgent>().hp -= other.GetComponent<AmmoSpeed>().damage;
        }
        //当てられた対象が建築物かつ、当たったオブジェクトが弾丸の時
        else if(CompareTag("Object") && (other.CompareTag("sideAammo") || other.CompareTag("sideBammo"))){
            //設定されたサウンドを再生
            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = sound;
            _audioSource.Play();
        }
        //当てられた対象がTrainingにあるターゲットかつ、当たったオブジェクトがA側の弾丸の時
        else if(CompareTag("Training Object") && other.CompareTag("sideAammo")){
            //設定されたサウンドを再生
            _audioSource = confirmaion.GetComponent<AudioSource>();
            _audioSource.Play();
        }
        //当たったオブジェクトが弾丸の時
        if(other.CompareTag("sideAammo") || other.CompareTag("sideBammo")){
            //弾丸削除
            other.GetComponent<AmmoSpeed>().Destroy();
        }
    }
}
