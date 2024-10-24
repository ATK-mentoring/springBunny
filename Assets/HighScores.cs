using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HighScores : MonoBehaviour
{
    public GameObject Mainmenubutton;
    public HighscoreController HSController;

    const string privateCode = "tMO8dwnEHECZaPewRY93Lw3HNNrT0DzUi8MxH4IaL0HQ";  //Key to Upload New Info
    const string publicCode = "66d111b38f40bb108870f21a";   //Key to download
    const string webURL = "https://cors-anywhere.herokuapp.com/http://dreamlo.com/lb/"; //  Website the keys are for
    //http://dreamlo.com/lb/tMO8dwnEHECZaPewRY93Lw3HNNrT0DzUi8MxH4IaL0HQ

    public PlayerScore[] scoreList;
    DisplayHighscores myDisplay;
    

    static HighScores instance; //Required for STATIC usability
    void Awake()
    {
        instance = this; //Sets Static Instance
        myDisplay = GetComponent<DisplayHighscores>();
    }
    
    public static void UploadScore(string username, int score)  //CALLED when Uploading new Score to WEBSITE
    {//STATIC to call from other scripts easily
        //Debug.Log("uploadScore " + username + " " + score);
        instance.StartCoroutine(instance.DatabaseUpload(username,score)); //Calls Instance
    }

    IEnumerator DatabaseUpload(string userame, int score) //Called when sending new score to Website
    {
        //string uri = webURL  + "/add/" + UnityWebRequest.EscapeURL(userame) + "/" + score;
        string uri = webURL + privateCode + "/add/" + UnityWebRequest.EscapeURL(userame) + "/" + score;
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    //Mainmenubutton.SetActive( true);
                    break;
            }
        }



        //WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(userame) + "/" + score);
        //yield return www;

        //if (string.IsNullOrEmpty(www.error))
        //{
        //    print("Upload Successful");
        //    DownloadScores();
        //}
        //else print("Error uploading" + www.error);
    }

    public void DownloadScores()
    {
        //Debug.Log("downloading scores");
        StartCoroutine("DatabaseDownload");
    }
    IEnumerator DatabaseDownload()
    {
        //WWW www = new WWW(webURL + publicCode + "/pipe/"); //Gets the whole list
        //WWW www = new WWW(webURL + publicCode + "/pipe/0/10"); //Gets top 10
        string uri = webURL + publicCode + "/pipe/0/10";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    OrganizeInfo(webRequest.downloadHandler.text);
                    if (myDisplay != null)
                    {
                        myDisplay.SetScoresToMenu(scoreList);
                    }
                    if (HSController != null)
                    {
                        HSController.recieveHoghScores(scoreList);
                    }
                    
                    break;
            }
        }


        //UnityWebRequest www = new UnityWebRequest(webURL + publicCode + "/pipe/0/10");
        //yield return www;

        //if (string.IsNullOrEmpty(www.error))
        //{
        //    OrganizeInfo(www.text);
        //    myDisplay.SetScoresToMenu(scoreList);
        //}
        //else print("Error uploading" + www.error);
    }

    void OrganizeInfo(string rawData) //Divides Scoreboard info by new lines
    {
        string[] entries = rawData.Split(new char[] {'\n'}, System.StringSplitOptions.RemoveEmptyEntries);
        scoreList = new PlayerScore[entries.Length];
        for (int i = 0; i < entries.Length; i ++) //For each entry in the string array
        {
            string[] entryInfo = entries[i].Split(new char[] {'|'});
            string username = entryInfo[0];
            int score = int.Parse(entryInfo[1]);
            scoreList[i] = new PlayerScore(username,score);
            print(scoreList[i].username + ": " + scoreList[i].score);
        }
    }
}

public struct PlayerScore //Creates place to store the variables for the name and score of each player
{
    public string username;
    public int score;

    public PlayerScore(string _username, int _score)
    {
        username = _username;
        score = _score;
    }
}