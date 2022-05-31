using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int sideA;
    public int sideB;
    public GameObject player;
    public GameObject cpu;
    public Vector3[] spawnpoint;
    public float time = 20f;
    private bool ingame = false;
    private bool endgame = false;
    public GameObject sideAshutter;
    public GameObject sideBshutter;
    private GameObject[] cpuarray = new GameObject[11];
    private bool gamesetting = false;
    private int timecount = 6;
    public AudioClip sound;
    private AudioSource audioSource;

    void Update()
    {
        time -= Time.deltaTime;
        //シーン開始時10秒経過するまで（メイン武器選択時）の時間表示
        if(endgame == false && ingame == false && gamesetting == false && time > 10){
            player.GetComponent<PlayerUI>().time.text = (int)time - 10 + "";
        }
        //試合開始から終了までの時間表示
        else{
            player.GetComponent<PlayerUI>().time.text = (int)time + "";
        }
        //試合開始前
        if(endgame == false && ingame == false && gamesetting == false && time <= 10){
            //ゲーム開始時の設定処理
            GameSetting();
            //プレイヤーのUIにAとB側のテキスト表示
            player.GetComponent<PlayerUI>().sideA.text = sideA + "";
            player.GetComponent<PlayerUI>().sideB.text = sideB + "";
            player.GetComponent<PlayerUI>().sideAgray.gameObject.SetActive(true);
            player.GetComponent<PlayerUI>().sideBgray.gameObject.SetActive(true);
            //ゲーム開始前のboolをtrueにする
            gamesetting = true;
        }
        //試合開始前のカウントダウン
        if(endgame == false && ingame == false && time <= timecount && timecount != 1){
            //サウンドを再生
            audioSource = player.GetComponent<AudioSource>();
            audioSource.clip = sound;
            audioSource.Play();
            timecount--;
        }
        //ゲーム開始時
        if(endgame == false && ingame == false && time <= 0){
            //A側とB側のシャッターを動かす
            sideAshutter.transform.position = Vector3.MoveTowards(sideAshutter.transform.position, new Vector3(sideAshutter.transform.position.x, 28, sideAshutter.transform.position.z), 0.1f);
            sideBshutter.transform.position = Vector3.MoveTowards(sideBshutter.transform.position, new Vector3(sideBshutter.transform.position.x, 28, sideBshutter.transform.position.z), 0.1f);
        }
        //シャッターが止まった時
        if(endgame == false && ingame == false && sideAshutter.transform.position == new Vector3(sideAshutter.transform.position.x, 28, sideAshutter.transform.position.z) && sideBshutter.transform.position == new Vector3(sideBshutter.transform.position.x, 28, sideBshutter.transform.position.z)){
            //CPUを動かす
            for(int i = 0;i < 11;i++){
                cpuarray[i].GetComponent<NavMeshAgent>().speed = 3 + Random.Range(0,6) * 0.1f;
                cpuarray[i].GetComponent<Animator>().SetBool("idle", false);
            }
            //試合時間設定
            time = 270;
            //ゲーム中のboolをtrueにする
            ingame = true;
        }
        //試合終了時
        if(endgame == false && (sideA <= 0 || sideB <= 0 || (time <= 0 && ingame == true))){
            //ゲーム中のboolをfalseにし、試合状況をリセットする
            ingame = false;
            Reset();
            //10秒間待機
            time = 10f;
            //試合終了のboolをtreuにする
            endgame = true;
        }
        //試合終了後の10秒間待機後
        if(endgame == true && time <= 0f){
            //シーン　Game Lobbyに移動
            SceneManager.LoadScene("Game Lobby");
        }
    }
    //ゲーム開始時の設定
    public void GameSetting(){
        for(int i = 0;i < 12;i++){
            //プレイヤーの座標設定
            if(i == 0){
                player.transform.position = spawnpoint[i];
                player.transform.rotation = Quaternion.Euler(0,-90,0);
            }
            //A側のCPUの設定
            else if(i < 6){
                //CPUを複製し、タグと名前を設定する
                GameObject instance = (GameObject)Instantiate(cpu,spawnpoint[i],Quaternion.Euler(0,0,0));
                instance.tag = "sideA";
                instance.transform.GetChild(0).gameObject.tag = "sideA";
                instance.name = "blue cpu";
                //CPUを待機状態にし、インスタンスを配列に入れる
                instance.GetComponent<NavMeshAgent>().speed = 0;
                instance.GetComponent<Animator>().SetBool("idle", true);
                cpuarray[i-1] = instance;
            }
            //B側のCPUの設定
            else{
                //CPUを複製し、タグと名前を設定する
                GameObject instance = (GameObject)Instantiate(cpu,spawnpoint[i],Quaternion.Euler(0,180,0));
                instance.tag = "sideB";
                instance.transform.GetChild(0).gameObject.tag = "sideB";
                instance.name = "red cpu";
                //CPUを待機状態にし、インスタンスを配列に入れる
                instance.GetComponent<NavMeshAgent>().speed = 0;
                instance.GetComponent<Animator>().SetBool("idle", true);
                cpuarray[i-1] = instance;
            }
        }
    }
    public void Respawn(string side,string name, GameObject cpu){
        //A側の時
        if(side == "sideA"){
            //A側のリスポーンのチケットを減らす
            sideA--;
            player.GetComponent<PlayerUI>().sideA.text = sideA + "";
            //プレイヤーの時、６カ所のリスポーン座標から１つランダムで決める
            if(name == "FPSController"){
                player.transform.position = spawnpoint[Random.Range(0,6)];
            }
            //CPUの時、６カ所のリスポーン座標から１つランダムで決める
            else{
                cpu.transform.position = spawnpoint[Random.Range(0, 6)];
            }
        }
        //B側の時
        else if(side == "sideB"){
            //B側のリスポーンのチケットを減らし、６カ所のリスポーン座標から1つランダムで決める
            sideB--;
            player.GetComponent<PlayerUI>().sideB.text = sideB + "";
            cpu.transform.position = spawnpoint[Random.Range(6,12)];
        }
    }
    //試合終了時、状況をリセットする
    public void Reset(){
        //CPUを削除
        for(int i = 0;i < 11;i++){
            Destroy(cpuarray[i]);
        }
        //試合終了時のテキスト表示
        player.GetComponent<PlayerUI>().wintext.gameObject.SetActive(true);
        //A側の勝利時
        if(sideA <= 0){
            player.GetComponent<PlayerUI>().wintext.text = "Red team WIN";
            player.GetComponent<PlayerUI>().wintext.color = Color.red;
        }
        //B側の勝利時
        else if(sideB <= 0){
            player.GetComponent<PlayerUI>().wintext.text = "Blue team WIN";
            player.GetComponent<PlayerUI>().wintext.color = Color.blue;
        }
        //時間経過による試合終了時
        else if(time <= 0){
            player.GetComponent<PlayerUI>().wintext.text = "Draw";
            player.GetComponent<PlayerUI>().wintext.color = Color.black;
        }
    }
}
