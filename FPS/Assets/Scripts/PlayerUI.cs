using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI ammo;
    public TextMeshProUGUI sideA;
    public TextMeshProUGUI sideB;
    public TextMeshProUGUI time;
    public bool usingmaingun = true;
    public GameObject maingun;
    public GameObject subgun;
    public Image sideAgray;
    public Image sideBgray;
    public Image timegray;
    public TextMeshProUGUI wintext;
    void Update()
    {
        //メイン武器とサブ武器の残り弾丸を表示
        if(usingmaingun == true){
            ammo.text = maingun.GetComponent<GunStatus>().nowAmmo + " / " + maingun.GetComponent<GunStatus>().maxAmmo;
        }
        else{
            ammo.text = subgun.GetComponent<GunStatus>().nowAmmo + " / " + subgun.GetComponent<GunStatus>().maxAmmo;
        }
    }
}
