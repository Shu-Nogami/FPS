using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammospeed : MonoBehaviour
{
    public int basespeed;
    public int damage;
    void Start()
    {
        //10秒後Destroy実行
        Invoke("Destroy", 10f);
    }
    void Update()
    {
        //オブジェクトを前に進める
        transform.position += transform.forward * basespeed;
    }
    //自身を削除
    public void Destroy(){
        Destroy(gameObject);
    }
}
