using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayManager : MonoBehaviour
{
    public static PlayManager instance;

    public bool PlayEnd; //플레이가끝났는지 안 끝났는지 체크
    public float Limit_Time = 60f; //제한시간
    public int Enemy_Count = 10; //사냥해야될 몬스터 수

    public TextMeshProUGUI TimeLabel; //시간표기
    public TextMeshProUGUI EnemyLabel; //남은몬스터 수 표기
    public GameObject FinalGUI; //최종결과 UI들 묶어 놓은 오브젝트
    public TextMeshProUGUI FinalMessage; //최종결과 표기
    public TextMeshProUGUI FinalScoreLabel; //최종점수

    [SerializeField] private GameObject _bestScoreUI;

    [SerializeField] private GameObject _escPanel;

    private void Awake()
    {
        instance = this;
    }

    void Start(){
        _bestScoreUI.SetActive(false);
        _escPanel.SetActive(false);
        FinalGUI.SetActive(false);
        EnemyLabel.text = string.Format("Enemy : {0}", Enemy_Count);
        TimeLabel.text = string.Format("Time : {0:N2}", Limit_Time);
    }

    void Update()
    {
        if (PlayEnd != true)
        {
            if (Limit_Time > 0)
            {
                Limit_Time -= Time.deltaTime;
                TimeLabel.text = string.Format("Time : {0:N2}", Limit_Time);
            }
            else
            {
                GameOver();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            _escPanel.SetActive(true);
        }
    }

    public void Clear()
    {
        if (PlayEnd != true)
        {
            Time.timeScale = 0;
            PlayEnd = true;
            FinalMessage.text = "Clear!!";

            //플레이어를 찾아서 Player_Ctrl을 PC라는 이름으로 가져오기.
            Player_Ctrl PC = GameObject.Find("Player").GetComponent<Player_Ctrl>();

            //최종점수 공식 : 클리어점수 + 남은시간 보너스 + 남은 HP 보너스.
            float score = 12345f + Limit_Time * 123f + PC.hp * 123f;
            if (score > PlayerPrefs.GetInt("HighScore"))
            {
                _bestScoreUI.SetActive(true);
                PlayerPrefs.SetInt("HighScore", (int)score);
                PlayerPrefs.SetString("HighScorePlayerName", PlayerPrefs.GetString("CurrentPlayerName"));
            }
            FinalScoreLabel.text = string.Format("{0:N0}", score);

            //최종결과화면 GUI 활성화 시키기.
            FinalGUI.SetActive(true);

        }
    }


    public void GameOver()
    {
        if (PlayEnd != true)
        {
            Time.timeScale = 0;
            PlayEnd = true;
            FinalMessage.text = "Fail...";
            float score = 1234f + Enemy_Count * 123f;
            if (score > PlayerPrefs.GetInt("HighScore"))
            {
                _bestScoreUI.SetActive(true);
                PlayerPrefs.SetInt("HighScore", (int)score);
                PlayerPrefs.SetString("HighScorePlayerName", PlayerPrefs.GetString("CurrentPlayerName"));
            }
            FinalScoreLabel.text = string.Format("{0:N0}", score);
            FinalGUI.SetActive(true);
        }

        //플레이어를 찾아서 Player_Ctrl을 PC라는 이름으로 가져오기.
        Player_Ctrl PC = GameObject.Find("Player").GetComponent<Player_Ctrl>();
        PC.PS = PlayerState.Dead;
    }

    public void Replay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainBG");
    }

    public void Quit()
    {
        Application.Quit();
    }
    
    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Title");
    }

    public void EnemyDie()
    {
        Enemy_Count--;
        EnemyLabel.text = string.Format("Enemy : {0}", Enemy_Count);
        if (Enemy_Count <= 0)
        {
            Clear();
        }
    }

    public void EscClose()
    {
        _escPanel.SetActive(false);
        Time.timeScale = 1f;
    }
    
}
