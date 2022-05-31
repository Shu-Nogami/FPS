using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Searchenemy : MonoBehaviour
{
    private float searchAngle = 100f;
    private RaycastHit hitobject;
    public GameObject cpu;

    private void OnTriggerStay(Collider other){
        //CPUがB側かつ、範囲内に入ったCPUがA側またはプレイヤーの時
        if(cpu.tag == "sideB" && (other.tag == "sideA" || other.tag == "Player")){
            //自身と対象との距離および、角度
            Vector3 Direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(transform.forward, Direction);
            //対象が範囲内に入った時
            if(angle <= searchAngle){
                Physics.Raycast(transform.position,Direction,out hitobject);
                //対象が敵だった場合
                if(hitobject.collider.tag == "sideA" || hitobject.collider.tag == "Player"){
                    //停止し、射撃を実行
                    cpu.GetComponent<cpuagent>().encounter = true;
                    cpu.GetComponent<NavMeshAgent>().speed = 0;
                    cpu.transform.LookAt(other.transform);
                    cpu.GetComponent<Animator>().SetBool("shoot", true);
                }
                //対象が敵以外の場合
                else{
                    //移動を続行
                    cpu.GetComponent<cpuagent>().encounter = false;
                    cpu.GetComponent<cpuagent>().time = 0;
                    cpu.GetComponent<NavMeshAgent>().speed = 3.5f;
                    cpu.GetComponent<Animator>().SetBool("shoot", false);
                }
            }
        }
        //CPUがA側かつ、範囲内に入ったCPUがB側の時
        else if(cpu.tag == "sideA" && other.tag == "sideB"){
            //自身と対象との距離および、角度
            Vector3 Direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(transform.forward, Direction);
            //対象が範囲内に入った時
            if(angle <= searchAngle){
                Physics.Raycast(transform.position,Direction,out hitobject);
                //対象が敵だった場合
                if(hitobject.transform.tag == "sideB"){
                    //停止し、射撃を実行
                    cpu.GetComponent<cpuagent>().encounter = true;
                    cpu.GetComponent<NavMeshAgent>().speed = 0;
                    cpu.transform.LookAt(other.transform);
                    cpu.GetComponent<Animator>().SetBool("shoot", true);
                }
                //対象が敵以外の場合
                else{
                    //移動を続行
                    cpu.GetComponent<cpuagent>().encounter = false;
                    cpu.GetComponent<cpuagent>().time = 0;
                    cpu.GetComponent<NavMeshAgent>().speed = 3.5f;
                    cpu.GetComponent<Animator>().SetBool("shoot", false);
                }
            }
        }
    }
}
