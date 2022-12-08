using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameOver_DC : MonoBehaviour
{
    // Start is called before the first frame update
    private int highScore;
    private int curScore;
    private string limiter = "limit";
    public TMP_Text score;
    public TMP_Text high;
    private int limit;
    void Start()
    {
        highScore = PlayerPrefs.GetInt("high_score");
        curScore = PlayerPrefs.GetInt("cur_score");
        score.text = "Score: " + curScore.ToString();
        if (PlayerPrefs.HasKey(limiter))
        {
            Debug.Log("All good");
        }
        else
        {
            Debug.Log("Starting the limiter");
            PlayerPrefs.SetInt(limiter, 0);
        }
        limit = PlayerPrefs.GetInt(limiter);
        if (limit < highScore)
        {
            PlayerPrefs.SetInt(limiter, highScore);
            high.text = "New High Score: " + highScore.ToString();
        }
        else
        {
            high.text = "High Score: " + highScore.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
        }
    }
}
