using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSpeed : MonoBehaviour
{
    public int baseSpeed;
    public int damage;
    void Start()
    {
        //10秒後Destroy実行
        Invoke(nameof(Destroy), 10f);
    }
    void Update()
    {
        //オブジェクトを前に進める
        transform.position += transform.forward * baseSpeed;
    }
    //自身を削除
    public void Destroy(){
        Destroy(gameObject);
    }
}
