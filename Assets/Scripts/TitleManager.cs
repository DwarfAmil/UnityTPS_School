using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private InputField _inputField;

    [SerializeField] private Text _highScorePlayerNameText;

    [SerializeField] private TextMeshProUGUI _highScoreText;

    private void Start()
    {
        _highScorePlayerNameText.text = PlayerPrefs.GetString("HighScorePlayerName");
        _highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

    public void OnStart()
    {
        PlayerPrefs.SetString("CurrentPlayerName", _inputField.text);
        Debug.Log(_inputField.text);
        SceneManager.LoadScene("MainBG");
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
