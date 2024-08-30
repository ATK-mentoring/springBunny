using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighscoreController : MonoBehaviour
{

    //public string PlayerName;
    //public int score;
    //public HighScores HS;
    public TMPro.TextMeshProUGUI playerName;
    public TMPro.TextMeshProUGUI playerScore;
    public Button saveButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("StartScene");
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
