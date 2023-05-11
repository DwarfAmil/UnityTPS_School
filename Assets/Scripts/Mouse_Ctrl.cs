using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_Ctrl : MonoBehaviour
{
    public Transform Target;
    public GameObject Curser;
    public Player_Ctrl PC;
    
    void Update () {
        RaycastHit hit;
        
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        //raycast hit 되는 위치는 검출해서
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            //검출된 위치에 curser 출력
            Curser.transform.position = new Vector3(hit.point.x, 1f, hit.point.z);
            
            //마우스 클릭하면, 해당 위치를 향하도록 회전 후 사격
            if (Input.GetMouseButton(0) && PC.PS != PlayerState.Dead)
            {
                Target.position = new Vector3(hit.point.x, 1f, hit.point.z);
                PC.lookDirection = Target.position - PC.gameObject.transform.position;
            }
        }
    }

}
