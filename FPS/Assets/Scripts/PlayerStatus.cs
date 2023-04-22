using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public GameObject mainGun;
    private bool isUsingMainGun = true;
    public GameObject subGun;
    private GameObject i_MainGun;
    private GameObject i_SubGun;
    private GunStatus _playerGunStatus;
    private int scrollCount = 0;
    public GameObject weaponPosition;
    [SerializeField] GameObject playerShotPosition;
    public GameObject mainCamera;
    private float shotRate;
    private float time = 0;
    [SerializeField] int hp = 100;
    public GameObject wareHouse;
    public bool isTraining;

    void Start()
    {
        //メイン武器を設定
        i_MainGun = (GameObject)Instantiate(mainGun,transform.position,transform.rotation);
        i_MainGun.transform.position = weaponPosition.transform.position;
        i_MainGun.transform.parent = mainCamera.transform;
        i_MainGun.GetComponent<GunStatus>().SetShotPosition(playerShotPosition);
        //サブ武器を設定
        i_SubGun = (GameObject)Instantiate(subGun,transform.position,transform.rotation);
        i_SubGun.transform.position = weaponPosition.transform.position;
        i_SubGun.transform.parent = mainCamera.transform;
        i_SubGun.GetComponent<GunStatus>().SetShotPosition(playerShotPosition);
        i_SubGun.SetActive(false);
        shotRate = i_MainGun.GetComponent<GunStatus>().shotRate;
        //メイン武器とサブ武器をリロード
        i_MainGun.GetComponent<GunStatus>().Reload();
        i_SubGun.GetComponent<GunStatus>().Reload();
        if(isTraining == false){
            GetComponent<PlayerUI>().maingun = i_MainGun;
            GetComponent<PlayerUI>().subgun = i_SubGun;
        }
    }

    void Update()
    {
        //左クリックが押されているかつ、メイン武器を装備している間
        if(Input.GetMouseButton(0) == true && isUsingMainGun == true){
            //押されている時間が射撃間隔を超えたら
            if(time >= shotRate){
                //メイン武器を撃つ
                i_MainGun.GetComponent<GunStatus>().Shot("sideA");
                time = 0;
            }
            //射撃間隔時間を増加
            time += Time.deltaTime;
        }
        //左クリックを離したかつ、メイン武器を装備している間
        else if(Input.GetMouseButtonUp(0) == true && isUsingMainGun == true){
            //射撃間隔時間reset
            time = 0;
        }
        //左クリックが押されたかつ、サブ武器を装備しているとき
        if(Input.GetMouseButtonDown(0) == true && isUsingMainGun == false){
            //サブ武器を撃つ
            i_SubGun.GetComponent<GunStatus>().Shot("sideA");
        }
        //Rキーが押されたとき
        if(Input.GetKeyDown("r") == true){
            //メイン武器を装備しているとき
            if(isUsingMainGun){
                //メイン武器をリロード
                i_MainGun.GetComponent<GunStatus>().Reload();
            }
            //サブ武器を装備しているとき
            else{
                //サブ武器をリロード
                i_SubGun.GetComponent<GunStatus>().Reload();
            }
        }
        //マウスホイールのカウントが2で割った時の余りが1の時
        if(scrollCount % 2 == 1){
            //メイン武器をオフにし、サブ武器をオンにする
            isUsingMainGun = false;
            if(isTraining == false){
                GetComponent<PlayerUI>().isUsingMainGun = false;
            }
            i_MainGun.SetActive(false);
            i_SubGun.SetActive(true);
        }
        else{
            //サブ武器をオフにし、メイン武器をオンにする
            isUsingMainGun = true;
            if(isTraining == false){
                GetComponent<PlayerUI>().isUsingMainGun = true;
            }
            i_SubGun.SetActive(false);
            i_MainGun.SetActive(true);
        }
        //マウスホイールが回った時
        if(Input.GetAxis("Mouse ScrollWheel") != 0){
            //カウントを増やす
            scrollCount++;
        }
        //体力がなくなった時
        if(hp <= 0){
            //リスポーン処理
            wareHouse.GetComponent<GameController>().Respawn("sideA", gameObject.name, gameObject);
            hp = 100;
        }
    }
    //敵からのダメージ処理
    public void HitDamage(int damage){
        hp -= damage;
    }

    //メイン武器を変更する
    public void WeaponChange(GameObject weapon){
        //今のメイン武器を削除し、新しいメイン武器を設定する
        Destroy(i_MainGun.gameObject);
        i_MainGun = (GameObject)Instantiate(weapon,transform.position,transform.rotation);
        i_MainGun.transform.position = weaponPosition.transform.position;
        i_MainGun.transform.parent = mainCamera.transform;
        _playerGunStatus = i_MainGun.GetComponent<GunStatus>();
        shotRate = _playerGunStatus.shotRate;
        _playerGunStatus.Reload();
        _playerGunStatus.SetShotPosition(playerShotPosition);
        if(isTraining == false){
            GetComponent<PlayerUI>().maingun = i_MainGun;
        }
    }
}
