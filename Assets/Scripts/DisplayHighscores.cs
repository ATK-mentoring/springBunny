using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class DisplayHighscores : MonoBehaviour 
{
    public TMPro.TextMeshProUGUI[] rNames;
    public TMPro.TextMeshProUGUI[] rScores;
    HighScores myScores;

    void Start() //Fetches the Data at the beginning
    {
        for (int i = 0; i < rNames.Length;i ++)
        {
            rNames[i].text = "";
        }
        myScores = GetComponent<HighScores>();
        StartCoroutine("RefreshHighscores");
    }
    public void SetScoresToMenu(List<PlayerScore> highscoreList) //Assigns proper name and score for each text value
    {
        //Debug.Log("setting scores to menu");
        //Debug.Log(highscoreList[0]);
        for (int i = 0; i < rNames.Length;i ++)
        {
            rNames[i].text = "";
            if (highscoreList.Count > i)
            {
                rScores[i].text = highscoreList[i].score.ToString();
                rNames[i].text = UnityWebRequest.UnEscapeURL(highscoreList[i].name);
            }
        }
    }
    IEnumerator RefreshHighscores() //Refreshes the scores every 30 seconds
    {
        while(true)
        {
            myScores.DownloadScores();
            yield return new WaitForSeconds(30);
        }
    }
}
