using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class L2Manager_DC : MonoBehaviour
{
    // Start is called before the first frame update
    private int score;
    private int highScore;
    private int livesCount;
    private int kills = 0;
    public int killGoal = 50;
    public TMP_Text scoreCounter;
    public TMP_Text goalLine;
    public TMP_Text lives;
    public TMP_Text ammo; //again, no idea how to write the UI for this.  I'll wait after we send in the first build
    void Start()
    {
        score = PlayerPrefs.GetInt("cur_score");
        highScore = PlayerPrefs.GetInt("high_score");
        livesCount = PlayerPrefs.GetInt("lives");
        Debug.Log(score);
        Debug.Log(highScore);
        Debug.Log(livesCount);
    }

    // Update is called once per frame
    void Update()
    {
        if (livesCount > 0 && kills < killGoal)
        {
            scoreCounter.text = "Score: " + score.ToString();
            lives.text = "Lives: " + livesCount.ToString();
            goalLine.text = kills.ToString() + "/" + killGoal.ToString();
        }
        else
        {
            if (livesCount <= 0)
            {
                Debug.Log("Game Over");
                PlayerPrefs.SetInt("cur_score", score);
                if (score > highScore)
                {
                    highScore = score;
                    PlayerPrefs.SetInt("high_score", highScore);
                }
            }
            else if (kills >= killGoal)
            {
                Debug.Log("Level Win!");
                PlayerPrefs.SetInt("cur_score", score);
                PlayerPrefs.SetInt("lives", livesCount);
            }
        }
    }
    public void NinjaKilled()
    {
        score += 10;
        kills++;
    }

    public void NinjaDied()
    {
        livesCount--;
    }

    public void BonusPoints()
    {
        score += 30; //call BonusPoints when you get a collectable
    }
}
