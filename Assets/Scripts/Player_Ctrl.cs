using UnityEngine;
using UnityEngine.UI;

public enum PlayerState
{
    Idle,
    Walk,
    Run,
    Attack,
    Dead,
}

public class Player_Ctrl : MonoBehaviour {

    public PlayerState PS;

    public Vector3 lookDirection;
    public float Speed = 0f;
    public float WalkSpeed = 6f;
    public float RunSpeed = 12f;
    public Slider LifeBar;
    
    Animation animation;
    public AnimationClip Idle_Ani;
    public AnimationClip Walk_Ani;
    public AnimationClip Run_Ani;
    
    // 총알 설정, 발사 위치, 발사 효과, 발사 효과음
    public GameObject Bullet;
    public Transform ShotPoint;
    public GameObject ShotFX;
    public AudioClip ShotSound;
    
    public float Max_hp = 100;
    public float hp = 100;

    public Text playerName;

    private string _playerName;

    private float _timer = 0;

    void Start()
    {
        _playerName = PlayerPrefs.GetString("CurrentPlayerName");
        if (_playerName != "")
        {
            playerName.text = _playerName;
        }
        else
        {
            playerName.text = "NoName";
        }
        animation = GetComponent<Animation>();
        ShotFX.SetActive(false);
    }
    
    void Update()
    {
        _timer -= Time.deltaTime;
        if (PS != PlayerState.Dead)
        {
            KeyboardInput();
            LookUpdate();
        }
        else
        {
            PlayManager.instance.GameOver();
        }
        AnimationUpdate();
    }
    
    void KeyboardInput()
    {
        float xx = Input.GetAxisRaw("Vertical");
        float ZZ = Input.GetAxisRaw("Horizontal");

        // 공격 중에 이동 제한
        if (PS != PlayerState.Attack)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) ||
                Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {
                lookDirection = xx * Vector3.forward + ZZ * Vector3.right;
                Speed = WalkSpeed;
                PS = PlayerState.Walk;

                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    Speed = RunSpeed;
                    PS = PlayerState.Run;
                }
            }

            if (xx == 0 && ZZ == 0 && PS != PlayerState.Idle)
            {
                PS = PlayerState.Idle;
                Speed = 0f;
            }
        }
        
        if (Input.GetMouseButton(0) && PS != PlayerState.Dead && _timer <= 0)
        {
            Shot();
            _timer = 0.1f;
        }

        if (Input.GetMouseButtonUp(0))
        {
            ShotFX.SetActive(false);
            PS = PlayerState.Idle;
        }
    }

    void LookUpdate()
    {
        Quaternion R = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, R, 10f);

        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }

    void AnimationUpdate()
    {
        if (PS == PlayerState.Idle)
        {
            animation.CrossFade(Idle_Ani.name, 0.2f);
        }
        else if (PS == PlayerState.Walk)
        {
            animation.CrossFade(Walk_Ani.name, 0.2f);
        }
        else if (PS == PlayerState.Run)
        {
            animation.CrossFade(Run_Ani.name, 0.2f);
        }
        else if (PS == PlayerState.Attack)
        {
            animation.CrossFade(Idle_Ani.name, 0.2f);
        }
        else if (PS == PlayerState.Dead)
        {
            animation.CrossFade(Idle_Ani.name, 0.2f);
        }
    }
    
    public void Shot()
    {
        var bullet = Instantiate
        (
            Bullet,
            ShotPoint.position,
            Quaternion.LookRotation(ShotPoint.forward)
        );
        
        //총알 간에는 충돌 체크 하지 않음
        Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GetComponent<Collider>());
        GetComponent<AudioSource>().clip = ShotSound;
        GetComponent<AudioSource>().Play();
        
        //발사 이펙트 활성
        ShotFX.SetActive(true);

        PS = PlayerState.Attack;
        Speed = 0f;
    }
    
    public void Hurt(float damage)
    {
        if (hp > 0)
        {
            hp -= damage;
            LifeBar.value = hp / Max_hp;
        }
        if (hp <= 0)
        {
            Speed = 0;
            PS = PlayerState.Dead;
        }
    }


}
