using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 30f;
    public float Power = 12f;
    public float Life = 2f;
    
    void Update () {
        //시간 경과에 따라 life 감소
        Life -= Time.deltaTime;
        
        if (Life <= 0)
        {
            Destroy(gameObject);
        }
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }
    
    void OnCollisionEnter(Collision collision)
    {
        // 탄환제거
        Destroy(gameObject);
        
        if (collision.gameObject.tag == "Enemy")
        {
            // sEnemy스크립트에 접근
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            
            // Enemy 스크립트의 ES가 Die가 아닌 경우 Hurt 함수 실행
            if (enemy.ES != EnemyState.Die)
            {
                enemy.Hurt(Power);
            }
        }
    }

}
