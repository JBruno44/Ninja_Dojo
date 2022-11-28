using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager_DC : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text scoreCounter;
    public TMP_Text goalLine;
    public TMP_Text lives;
   // public TMP_Text ammo; //idk if this is the right idea.  Might be best for me to wait until I get information about the ammo script before I code in the UI for it.
    //private int levelNum = 1; //might want to ignore this.  Or, we can have one level change backgrounds to make the score transfering easier.  Which do you think is better?
    private int livesCount = 3;
    public string highScoreKey = "high_score";
    private int pastScore = 0;
    public string curScoreKey = "cur_score";
    public string livePref = "lives";
    private int score = 0;
    private int kills = 0;
    public int killGoal = 25;
    void Start()
    {
        if (PlayerPrefs.HasKey(highScoreKey))
        {
            pastScore = PlayerPrefs.GetInt(highScoreKey);
            Debug.Log(PlayerPrefs.GetInt(highScoreKey));
        }
        else
        {
            PlayerPrefs.SetInt(highScoreKey, score);
            Debug.Log("Loading High Score");
        }
        if (PlayerPrefs.HasKey(curScoreKey))
        {
            Debug.Log(PlayerPrefs.GetInt(curScoreKey));
        }
        else
        {
            PlayerPrefs.SetInt(curScoreKey, score);
            Debug.Log("Loading Score Save");
        }
        if (PlayerPrefs.HasKey(livePref))
        {
            PlayerPrefs.SetInt(livePref, livesCount);
        }
        else
        {
            PlayerPrefs.SetInt(livePref, 3);
            Debug.Log("Loading Lives Save");
        }
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
                PlayerPrefs.SetInt(curScoreKey, score);
                if (score > pastScore)
                {
                    pastScore = score;
                    PlayerPrefs.SetInt(highScoreKey, pastScore);
                }
            }
            else if (kills >= killGoal)
            {
                Debug.Log("Level Win!");
                PlayerPrefs.SetInt(curScoreKey, score);
                PlayerPrefs.SetInt(livePref, livesCount);
                UnityEngine.SceneManagement.SceneManager.LoadScene("Sample2");
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
