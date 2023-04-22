using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class CPUAgent : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    public Vector3[] goalpoint;
    private int goalNumber = -3;
    public bool isEncount = false;
    public float time = 0;
    private float shotRate;
    public int hp = 100;
    private GameObject wareHouse;
    public TextMeshProUGUI cpuName;
    public Canvas canvas;
    private GameObject player;

    void Start()
    {
        //CPUが持っている銃をリロード
        GetComponent<GunStatus>().Reload();
        //ルートを設定
        Goalmaking();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.SetDestination(goalpoint[goalNumber]);
        //射撃間隔設定
        shotRate = GetComponent<GunStatus>().shotRate;
        //CPUの名前設定
        cpuName.text = gameObject.name;
        if(gameObject.tag == "sideA"){
            cpuName.color = Color.blue;
        }
        else{
            cpuName.color = Color.red;
        }
        //プレイヤーのオブジェクト情報をplayerに代入
        player = GameObject.Find("FPSController");
    }

    void Update()
    {
        //ネームタグが常にプレイヤーに向くようにする
        canvas.transform.LookAt(player.transform);
        //CPUが指定の座標についた時
        if(transform.position.x == goalpoint[goalNumber].x && transform.position.z == goalpoint[goalNumber].z){
            //ルートを再設定
            Goalmaking();
            _navMeshAgent.SetDestination(goalpoint[goalNumber]);
        }
        //CPUが接敵した時
        if(isEncount == true){
            //一定間隔に射撃
            if(time >= shotRate){
                GetComponent<GunStatus>().Shot(tag);
                time = 0;
            }
            time += Time.deltaTime;
        }
        //体力がなくなった時
        if(hp <= 0){
            wareHouse = GameObject.Find("Stage (Warehouse)");
            //アタッチされているオブジェクトのタグがsideAの時、GameControllerのRespawnを実行
            if(tag == "sideA"){
                wareHouse.GetComponent<GameController>().Respawn(tag, gameObject.name, gameObject);
            }
            //アタッチされているオブジェクトのタグがsideBの時、GameControllerのRespawnを実行
            else{
                wareHouse.GetComponent<GameController>().Respawn(tag, gameObject.name, gameObject);
            }
            hp = 100;
        }
    }
    //現在位置から次に進むルートを決定する
    void Goalmaking(){
        //アタッチされているオブジェクトのタグがsideAの時
        if(tag == "sideA"){
            //goalnumberから次の移動する座標を決定する
            if(goalNumber / 3 < 0){
                goalNumber = 0;
            }
            else if(goalNumber / 3 == 2){
                goalNumber = 3;
            }
            else{
                goalNumber = goalNumber / 3 * 3 + 3;
            }
            goalNumber = goalNumber + Random.Range(0,3);
        }
        //アタッチされているオブジェクトのタグがsideBの時
        else if(tag == "sideB"){
            //goalnumberから次の移動する座標を決定する
            if(goalNumber / 3 < 0){
                goalNumber = 8;
            }
            else if(goalNumber / 3 == 0){
                goalNumber = 5;
            }
            else{
                goalNumber = goalNumber / 3 * 3 - 1;
            }
            goalNumber = goalNumber - Random.Range(0,3);
        }
    }
}
