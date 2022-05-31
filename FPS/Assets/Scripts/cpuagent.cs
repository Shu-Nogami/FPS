using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class cpuagent : MonoBehaviour
{
    NavMeshAgent agent;
    public Vector3[] goalpoint;
    private int goalnumber = -3;
    public bool encounter = false;
    public float time = 0;
    private float shotrate;
    public int hp = 100;
    private GameObject warehouse;
    public TextMeshProUGUI textname;
    public Canvas canvas;
    private GameObject player;

    void Start()
    {
        //CPUが持っている銃をリロード
        GetComponent<Gunstatus>().Reload();
        //ルートを設定
        Goalmaking();
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(goalpoint[goalnumber]);
        //射撃間隔設定
        shotrate = GetComponent<Gunstatus>().shotrate;
        //CPUの名前設定
        textname.text = gameObject.name;
        if(gameObject.tag == "sideA"){
            textname.color = Color.blue;
        }
        else{
            textname.color = Color.red;
        }
        //プレイヤーのオブジェクト情報をplayerに代入
        player = GameObject.Find("FPSController");
    }

    void Update()
    {
        //ネームタグが常にプレイヤーに向くようにする
        canvas.transform.LookAt(player.transform);
        //CPUが指定の座標についた時
        if(transform.position.x == goalpoint[goalnumber].x && transform.position.z == goalpoint[goalnumber].z){
            //ルートを再設定
            Goalmaking();
            agent.SetDestination(goalpoint[goalnumber]);
        }
        //CPUが接敵した時
        if(encounter == true){
            //一定間隔に射撃
            if(time >= shotrate){
                GetComponent<Gunstatus>().shot(tag);
                time = 0;
            }
            time += Time.deltaTime;
        }
        //体力がなくなった時
        if(hp <= 0){
            warehouse = GameObject.Find("Stage (Warehouse)");
            //アタッチされているオブジェクトのタグがsideAの時、GameControllerのRespawnを実行
            if(tag == "sideA"){
                warehouse.GetComponent<GameController>().Respawn(tag, gameObject.name, gameObject);
            }
            //アタッチされているオブジェクトのタグがsideBの時、GameControllerのRespawnを実行
            else{
                warehouse.GetComponent<GameController>().Respawn(tag, gameObject.name, gameObject);
            }
            hp = 100;
        }
    }
    //現在位置から次に進むルートを決定する
    void Goalmaking(){
        //アタッチされているオブジェクトのタグがsideAの時
        if(tag == "sideA"){
            //goalnumberから次の移動する座標を決定する
            if(goalnumber / 3 < 0){
                goalnumber = 0;
            }
            else if(goalnumber / 3 == 2){
                goalnumber = 3;
            }
            else{
                goalnumber = goalnumber / 3 * 3 + 3;
            }
            goalnumber = goalnumber + Random.Range(0,3);
        }
        //アタッチされているオブジェクトのタグがsideBの時
        else if(tag == "sideB"){
            //goalnumberから次の移動する座標を決定する
            if(goalnumber / 3 < 0){
                goalnumber = 8;
            }
            else if(goalnumber / 3 == 0){
                goalnumber = 5;
            }
            else{
                goalnumber = goalnumber / 3 * 3 - 1;
            }
            goalnumber = goalnumber - Random.Range(0,3);
        }
    }
}
