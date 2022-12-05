using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager3_DC : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text lives;
    private int score;
    private int highScore;
    private int livesCount;
    private bool win = false;
    void Start()
    {
        score = PlayerPrefs.GetInt("cur_score");
        highScore = PlayerPrefs.GetInt("high_score");
        livesCount = PlayerPrefs.GetInt("lives");
    }

    // Update is called once per frame
    void Update()
    {
        lives.text = "Lives: " + livesCount.ToString();
        if (livesCount < 0 || win == true)
        {
            Debug.Log("Game Over");
            if (score > highScore)
            {
                highScore = score;
                PlayerPrefs.SetInt("high_score", highScore);
            }
            PlayerPrefs.SetInt("cur_score", score);
        }
    }

    public void NinjaDied()
    {
        livesCount--;
    }

    public void NinjaWin()
    {
        Debug.Log("Win");
        score += 5000;
        win = true;
    }
}