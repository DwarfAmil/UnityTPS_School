using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSelf : MonoBehaviour
{
    public float Speed = 5f;
    
    void Update () {
        transform.Rotate(new Vector3(0f, Speed, 0f));
    }

}
