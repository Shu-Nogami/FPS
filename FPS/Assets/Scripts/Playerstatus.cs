using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerstatus : MonoBehaviour
{
    public GameObject maingun;
    private bool usingmaingun = true;
    public GameObject subgun;
    private GameObject i_maingun;
    private GameObject i_subgun;
    private Gunstatus pgunstatus;
    private int scrollcount = 0;
    public GameObject weaponposition;
    [SerializeField] GameObject Pshotposition;
    public GameObject maincamera;
    private float shotrate;
    private float time = 0;
    [SerializeField] int hp = 100;
    public GameObject warehouse;
    public bool training;

    void Start()
    {
        //メイン武器を設定
        i_maingun = (GameObject)Instantiate(maingun,transform.position,transform.rotation);
        i_maingun.transform.position = weaponposition.transform.position;
        i_maingun.transform.parent = maincamera.transform;
        i_maingun.GetComponent<Gunstatus>().SetShotPosition(Pshotposition);
        //サブ武器を設定
        i_subgun = (GameObject)Instantiate(subgun,transform.position,transform.rotation);
        i_subgun.transform.position = weaponposition.transform.position;
        i_subgun.transform.parent = maincamera.transform;
        i_subgun.GetComponent<Gunstatus>().SetShotPosition(Pshotposition);
        i_subgun.SetActive(false);
        shotrate = i_maingun.GetComponent<Gunstatus>().shotrate;
        //メイン武器とサブ武器をリロード
        i_maingun.GetComponent<Gunstatus>().Reload();
        i_subgun.GetComponent<Gunstatus>().Reload();
        if(training == false){
            GetComponent<PlayerUI>().maingun = i_maingun;
            GetComponent<PlayerUI>().subgun = i_subgun;
        }
    }

    void Update()
    {
        //左クリックが押されているかつ、メイン武器を装備している間
        if(Input.GetMouseButton(0) == true && usingmaingun == true){
            //押されている時間が射撃間隔を超えたら
            if(time >= shotrate){
                //メイン武器を撃つ
                i_maingun.GetComponent<Gunstatus>().shot("sideA");
                time = 0;
            }
            //射撃間隔時間を増加
            time += Time.deltaTime;
        }
        //左クリックを離したかつ、メイン武器を装備している間
        else if(Input.GetMouseButtonUp(0) == true && usingmaingun == true){
            //射撃間隔時間reset
            time = 0;
        }
        //左クリックが押されたかつ、サブ武器を装備しているとき
        if(Input.GetMouseButtonDown(0) == true && usingmaingun == false){
            //サブ武器を撃つ
            i_subgun.GetComponent<Gunstatus>().shot("sideA");
        }
        //Rキーが押されたとき
        if(Input.GetKeyDown("r") == true){
            //メイン武器を装備しているとき
            if(usingmaingun == true){
                //メイン武器をリロード
                i_maingun.GetComponent<Gunstatus>().Reload();
            }
            //サブ武器を装備しているとき
            else if(usingmaingun == false){
                //サブ武器をリロード
                i_subgun.GetComponent<Gunstatus>().Reload();
            }
        }
        //マウスホイールのカウントが2で割った時の余りが1の時
        if(scrollcount % 2 == 1){
            //メイン武器をオフにし、サブ武器をオンにする
            usingmaingun = false;
            if(training == false){
                GetComponent<PlayerUI>().usingmaingun = false;
            }
            i_maingun.SetActive(false);
            i_subgun.SetActive(true);
        }
        else{
            //サブ武器をオフにし、メイン武器をオンにする
            usingmaingun = true;
            if(training == false){
                GetComponent<PlayerUI>().usingmaingun = true;
            }
            i_subgun.SetActive(false);
            i_maingun.SetActive(true);
        }
        //マウスホイールが回った時
        if(Input.GetAxis("Mouse ScrollWheel") != 0){
            //カウントを増やす
            scrollcount++;
        }
        //体力がなくなった時
        if(hp <= 0){
            //リスポーン処理
            warehouse.GetComponent<GameController>().Respawn("sideA", gameObject.name, gameObject);
            hp = 100;
        }
    }
    //敵からのダメージ処理
    public void HitDamage(int damage){
        hp -= damage;
    }

    //メイン武器を変更する
    public void Weaponchange(GameObject weapon){
        //今のメイン武器を削除し、新しいメイン武器を設定する
        Destroy(i_maingun.gameObject);
        i_maingun = (GameObject)Instantiate(weapon,transform.position,transform.rotation);
        i_maingun.transform.position = weaponposition.transform.position;
        i_maingun.transform.parent = maincamera.transform;
        pgunstatus = i_maingun.GetComponent<Gunstatus>();
        shotrate = pgunstatus.shotrate;
        pgunstatus.Reload();
        pgunstatus.SetShotPosition(Pshotposition);
        if(training == false){
            GetComponent<PlayerUI>().maingun = i_maingun;
        }
    }
}
