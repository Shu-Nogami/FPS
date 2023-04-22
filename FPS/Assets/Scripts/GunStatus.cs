using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunStatus : MonoBehaviour
{
    public GameObject ammo;
    private GameObject shotPosition;
    [SerializeField] GameObject cpuShotPosition;
    private GameObject i_Ammo;
    public int maxAmmo;
    public int damage;
    public int nowAmmo;
    public float reloadTime;
    public float shotRate;
    public bool isPlayer;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        //プレイヤーの時
        if(isPlayer == true){
            //銃の向きをカメラの中心に向ける
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            transform.rotation = Quaternion.LookRotation(ray.direction);
        }
    }
    //銃を撃つ処理
    public void Shot(string side){
        //銃に弾丸が残っているとき
        if(nowAmmo > 0){
            //弾丸を複製し、サウンドを再生する
            if(isPlayer == true){
                i_Ammo = (GameObject)Instantiate(ammo,shotPosition.transform.position,shotPosition.transform.rotation);
            }
            else{
                i_Ammo = (GameObject)Instantiate(ammo,cpuShotPosition.transform.position,cpuShotPosition.transform.rotation);
            }
            _audioSource.Play();
            i_Ammo.GetComponent<AmmoSpeed>().damage = damage;
            //A側の弾薬の時、弾薬のタグをsideAammoに変更
            if(side == "sideA"){
                i_Ammo.tag = "sideAammo";
            }
            //B側の弾薬の時、弾薬のタグをsideBammoに変更
            else if(side == "sideB"){
                i_Ammo.tag = "sideBammo";
            }
            //現在の弾薬を減らす
            nowAmmo--;
        }
        //CPUの現在の弾薬がなくなった場合、Reloadを実行
        if(isPlayer == false && nowAmmo == 0){
            Reload();
        }
    }
    //武器のリロード処理
    public void Reload(){
        //現在の弾薬と最大弾薬が違うとき
        if(nowAmmo != maxAmmo){
            //reloadtime遅らせてReplenishmentを実行
            Invoke(nameof(Replenishment), reloadTime);
            //アタッチされている対象がプレイヤーの時
            if(isPlayer == true){
                //リロードモーション実行
                GetComponent<Animation>().Play();
            }
        }
    }
    //現在の弾薬を最大弾薬にする
    public void Replenishment(){
        nowAmmo = maxAmmo;
    }
    //弾丸が出る位置を設定
    public void SetShotPosition(GameObject Pshotposition){
        shotPosition = Pshotposition;
    }
}
