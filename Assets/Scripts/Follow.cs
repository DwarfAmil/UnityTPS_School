using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject Target;
    public float Distance = 10f;
    public float Height = 8f;
    public float Speed = 4f;
    Vector3 Pos;
    
    void Update()
    {
        Pos = new Vector3(Target.transform.position.x,Height,Target.transform.position.z - Distance);
        
        //gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, Pos, Speed * Time.deltaTime);
        
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, Pos, Speed * Time.deltaTime);
    }
}
