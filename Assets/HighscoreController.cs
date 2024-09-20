using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighscoreController : MonoBehaviour
{

    //public string PlayerName;
    //public int score;
    public HighScores HS;
    public TMPro.TextMeshProUGUI playerName;
    public TMPro.TMP_InputField Pname;
    public TMPro.TextMeshProUGUI playerScore;
    public Button saveButton;
    public GameObject scoreUI;


    public void NameValueChanged()
    {
        Debug.Log("test");
        Pname.text = Pname.text.ToUpper();
        if (Pname.text.Length > 3)
        {
            Debug.Log("long " + Pname.text.Substring(0, 3));
            Pname.text = Pname.text.Substring(0, 3);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void isHighScore()
    {
        HS.DownloadScores();
        
    }

    public void recieveHoghScores(PlayerScore[] highscoreList)
    {
        Debug.Log(highscoreList.Length);
        for(int i = 0; i < 5; i++)
        {
            Debug.Log(i);
            Debug.Log(highscoreList[i]);
            if (int.Parse(this.playerScore.text) > highscoreList[i].score)
            {
                Debug.Log("playerscore = " + this.playerScore.text + " highscore = " + highscoreList[i].score);
                scoreUI.SetActive(true);
            }

        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void SaveScore()
    {
        HighScores.UploadScore(this.playerName.text, int.Parse( this.playerScore.text));
        saveButton.interactable = false;
    }

    //public void SetScores()
    //{
    //    Debug.Log("download clicked");
    //    HS.DownloadScores();
    //}
}
