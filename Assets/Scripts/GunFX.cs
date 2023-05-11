using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFX : MonoBehaviour
{
    public Light light;
    
    //프레임마다 light의 밝기,회전,사이즈를 랜덤하게 변경
    void Update () {
        light.range = Random.Range(4f, 10f);
        transform.localScale = Vector3.one * Random.Range(2f, 4f);
        transform.localEulerAngles = new Vector3(270f, 0, Random.Range(0f, 90.0f));
    }
}
