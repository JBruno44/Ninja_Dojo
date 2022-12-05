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
    public GameObject playerPrefab;
    public GameObject collectablePrefab;
    private GameObject player;
    private GameObject collectable;
    public AudioClip bloodSplat;
    private AudioSource death;
    public GameObject ninja;
    public ParticleSystem blood;
    void Start()
    {
        death = GetComponent<AudioSource>();
        score = PlayerPrefs.GetInt("cur_score");
        highScore = PlayerPrefs.GetInt("high_score");
        livesCount = PlayerPrefs.GetInt("lives");
        Debug.Log(score);
        Debug.Log(highScore);
        Debug.Log(livesCount);
        player = Instantiate(playerPrefab);
        player.transform.position = new Vector2(-0.1875f, 0.125f);
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
    public void NinjaKilled(float ninjaX, float ninjaY) //dear gosh make sure you got the tagging and the collision up.  Someone, anyone
    {
        score += 10;
        kills++;
        //insert kill here;
        ParticleSystem bloodThing = Instantiate(blood);
        bloodThing.transform.position = new Vector2(ninjaX, ninjaY);
        bloodThing.Play();
        if (!death.isPlaying)
        {
            death.PlayOneShot(bloodSplat, .8f);
        }
        //insert sound;
        #region coroutinecall
        StartCoroutine(instantiateEnemy());
        #endregion
    }

    public void NinjaDied() //for when the ninja dies.  you deal with Iframes.  Also, Where should I have them spawn for sanity reasons?
    {
        livesCount--;
    }


    public void BonusPoints()
    {
        score += 30; //call BonusPoints when you get a collectable
    }

    private IEnumerator instantiateEnemy()
    {
        int timeSpawn = Random.Range(2, 5);
        yield return new WaitForSeconds(timeSpawn);
        int randomSide = Random.Range(1, 2);
        int randomHeight = Random.Range(1, 4);
        if (randomSide == 1)
        {
            GameObject instance = Instantiate(ninja);
            instance.transform.position = new Vector2(-10.1875f, randomHeight);
        }
        else
        {
            GameObject instance = Instantiate(ninja);
            instance.transform.position = new Vector2(10.1875f, randomHeight);
        }
    }
}
