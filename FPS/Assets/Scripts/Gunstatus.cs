using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunstatus : MonoBehaviour
{
    public GameObject ammo;
    private GameObject shotposition;
    [SerializeField] GameObject cpushotposition;
    private GameObject i_ammo;
    public int maxammo;
    public int damage;
    private bool ads;
    private bool equipped;
    public int nowammo;
    public float reloadtime;
    public float shotrate;
    public bool playerbool;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        //プレイヤーの時
        if(playerbool == true){
            //銃の向きをカメラの中心に向ける
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            transform.rotation = Quaternion.LookRotation(ray.direction);
        }
    }
    //銃を撃つ処理
    public void shot(string side){
        //銃に弾丸が残っているとき
        if(nowammo > 0){
            //弾丸を複製し、サウンドを再生する
            if(playerbool == true){
                i_ammo = (GameObject)Instantiate(ammo,shotposition.transform.position,shotposition.transform.rotation);
            }
            else{
                i_ammo = (GameObject)Instantiate(ammo,cpushotposition.transform.position,cpushotposition.transform.rotation);
            }
            audioSource.Play();
            i_ammo.GetComponent<ammospeed>().damage = damage;
            //A側の弾薬の時、弾薬のタグをsideAammoに変更
            if(side == "sideA"){
                i_ammo.tag = "sideAammo";
            }
            //B側の弾薬の時、弾薬のタグをsideBammoに変更
            else if(side == "sideB"){
                i_ammo.tag = "sideBammo";
            }
            //現在の弾薬を減らす
            nowammo--;
        }
        //CPUの現在の弾薬がなくなった場合、Reloadを実行
        if(playerbool == false && nowammo == 0){
            Reload();
        }
    }
    //武器のリロード処理
    public void Reload(){
        //現在の弾薬と最大弾薬が違うとき
        if(nowammo != maxammo){
            //reloadtime遅らせてReplenishmentを実行
            Invoke("Replenishment", reloadtime);
            //アタッチされている対象がプレイヤーの時
            if(playerbool == true){
                //リロードモーション実行
                GetComponent<Animation>().Play();
            }
        }
    }
    //現在の弾薬を最大弾薬にする
    public void Replenishment(){
        nowammo = maxammo;
    }
    //弾丸が出る位置を設定
    public void SetShotPosition(GameObject Pshotposition){
        shotposition = Pshotposition;
    }
}
