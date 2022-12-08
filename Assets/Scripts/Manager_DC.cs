using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager_DC : MonoBehaviour
{
    // Start is called before the first frame update
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
    public GameObject playerPrefab;
    public GameObject collectablePrefab;
    private GameObject player;
    public ParticleSystem blood;
    public AudioClip bloodSplat;
    private AudioSource death;
    public GameObject ninja;
    public TMP_Text scoreCounter;
    public TMP_Text goalLine;
    public TMP_Text lives;
    void Start()
    {
        death = GetComponent<AudioSource>();
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
            PlayerPrefs.SetInt(curScoreKey, 0);
        }
        else
        {
            PlayerPrefs.SetInt(curScoreKey, 0);
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
        score = 0;
        scoreCounter.text = "Score: "+ score;
        lives.text = "Lives: " + livesCount.ToString();
        goalLine.text = kills.ToString() + "/" + killGoal.ToString();
        //player = Instantiate(playerPrefab);  I don't know if I should use it.
        //collectable = Instantiate(collectablePrefab);
        #region coroutinecall
        StartCoroutine(instantiateEnemy());
        #endregion

        #region coroutinecall
        StartCoroutine(instantiateCollectable());
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        if (livesCount > 0 && kills < killGoal)
        {
            if (scoreCounter == null)
            {
                Debug.Log("counter");
            }
            if (score == null)
            {
                Debug.Log("score");
            }
            //scoreCounter.text = "Score: " + score.ToString();
           // lives.text = "Lives: " + livesCount.ToString();
           // goalLine.text = kills.ToString() + "/" + killGoal.ToString();
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
                UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
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
        //insert sound;  I'll add it next week
        #region coroutinecall
        StartCoroutine(instantiateEnemy());
        #endregion
    }

    public void NinjaDied() //use this for when the player is hit.  Whoever is dealing with the player, you deal with I-frames.
    {
        livesCount--;
        #region coroutinecall
        StartCoroutine(instantiateEnemy());
        #endregion
    }

    public void BonusPoints()
    {
        score += 30; //call BonusPoints when you get a collectable
        #region coroutinecall
        StartCoroutine(instantiateCollectable());
        #endregion
    }

    private IEnumerator instantiateEnemy()
    {
        int timeSpawn = Random.Range(2, 5);
        yield return new WaitForSeconds(timeSpawn);
        int randomSide = Random.Range(1, 2);
        int randomHeight = Random.Range(1, 4);
        GameObject instance = Instantiate(ninja);
        if (randomSide == 1)
        {
            instance.transform.position = new Vector2(-10.1875f, randomHeight);
        }
        else
        {
            instance.transform.position = new Vector2(10.1875f, randomHeight);
        }
        AI_Move_JF ninjaThing = instance.GetComponent<AI_Move_JF>();
        //ninjaThing.manager = this; //there should be a manager here, right?
    }

    private IEnumerator instantiateCollectable()
    {
        int timeSPawn = Random.Range(2, 5);
        yield return new WaitForSeconds(timeSPawn);
        Vector2 placement = new Vector2(0, 0);
        float xPos = Random.Range(-10.5f, 10.5f);
        float yPos = Random.Range(-3.8f, 3.8f);
        GameObject instance = Instantiate(collectablePrefab);
        instance.transform.position = new Vector2(xPos, yPos);
        Collectable_DC collectable = instance.GetComponent<Collectable_DC>();
        collectable.manager = this;
    }
}
