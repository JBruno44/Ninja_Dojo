using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMove_JB : MonoBehaviour
{
    public Vector2 direction;
    public int spawnRange = 1;
    public float speed = 0.1f;

    //    // Player animation
    //    public Sprite[] playerSprites;
    //    private int currentTexture = 0;

    //    // OnHand Text (Child Text)
    //    public TMP_Text onHandTxt;

    //    // Private variables
    new private SpriteRenderer renderer;
    private ParticleSystem particles;
    private float localSpeed = 0.0f;

    //    // Check if coroutine is active
    //    bool isActive = false;

    //    // Get direction the player is facing (right by default)
    //    bool isFacingLeft = false;

    // Start is called before the first frame update
    void Start()
    {
        renderer = this.GetComponent<SpriteRenderer>();
        particles = this.GetComponent<ParticleSystem>();
        particles.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        // Stop player from moving in certain directions
        bool blockMovingUp = false;
        bool blockMovingDown = false;
        bool blockMovingRight = false;
        bool blockMovingLeft = false;

        float halfHeight = 60;
        float halfWidth = 60;
        Vector2 currentPosition = Camera.main.WorldToScreenPoint(this.transform.position);

        // Check to see if current position is greater than screen width
        if ((currentPosition.x + halfWidth) > Screen.width)
        {
            blockMovingRight = true;
        }
        // Check to see if current position is greater than screen height
        if (currentPosition.y + halfHeight > Screen.height)
        {
            blockMovingUp = true;
        }
        // Check to see if current position is less than 0
        if (currentPosition.x - halfWidth < 0)
        {
            blockMovingLeft = true;
        }
        // Check to see if current position is less than 0
        if (currentPosition.y - halfHeight < 0)
        {
            blockMovingDown = true;
        }

        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && !(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && !blockMovingUp)
        {
            direction.y = 1;
            if (!(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)))
            {
                localSpeed += speed;
                //if (!isActive)
                //{
                //    StartCoroutine(PlayerAnimation());
                //    isActive = true;
                //}
            }
            //StartCoroutine(PlayParticles());
        }
        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && !blockMovingRight)
        {
            direction.x = 1;
            localSpeed += speed;
            //if (isFacingLeft)
            //{
            //    this.transform.Rotate(0.0f, 180.0f, 0.0f);
            //    // Due to the text being a child of the player, the text will rotate as well. The following will fix this
            //    onHandTxt.transform.Rotate(0.0f, 180.0f, 0.0f);
            //    isFacingLeft = false;
            //}
            //if (!isActive)
            //{
            //    StartCoroutine(PlayerAnimation());
            //    isActive = true;
           // }
            //StartCoroutine(PlayParticles());
        }
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && !(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && !blockMovingLeft)
        {
            direction.x = -1;
            localSpeed += speed;
            //if (!isFacingLeft)
            //{
            //    this.transform.Rotate(0.0f, 180.0f, 0.0f);
            //    onHandTxt.transform.Rotate(0.0f, 180.0f, 0.0f);
            //    isFacingLeft = true;
            //}
            //if (!isActive)
            //{
            //    StartCoroutine(PlayerAnimation());
            //    isActive = true;
            //}
            //StartCoroutine(PlayParticles());
        }
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && !(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && !blockMovingDown)
        {
            direction.y = -1;
            if (!(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)))
            {
                localSpeed += speed;
                //    if (!isActive)
                //    {
                //        StartCoroutine(PlayerAnimation());
                //        isActive = true;
                //    }
            }
                //StartCoroutine(PlayParticles());
         }
            // Stop player animation when player stops moving
            //if (isActive && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && !(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && !(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && !(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
            //{
            //    StopCoroutine(PlayerAnimation());
            //    isActive = false;
            //    currentTexture = 0;
            //    renderer.sprite = playerSprites[currentTexture];
            //}

            if (direction != Vector2.zero)
            {
                direction.Normalize();
            }
            Mathf.Clamp(localSpeed, 0, 2f);
            localSpeed = Mathf.Lerp(localSpeed, 0, 0.13f);
            Vector3 newPosition = new Vector3(localSpeed * direction.x * Time.deltaTime, localSpeed * direction.y * Time.deltaTime, 0);
            this.transform.position += newPosition;
    }

        //IEnumerator PlayParticles()
        //{
        //    particles.Play();
        //    yield return new WaitForSeconds(0.5f);
        //    particles.Stop();
        //}

        //IEnumerator PlayerAnimation()
        //{
        //    renderer.sprite = playerSprites[currentTexture];
        //    currentTexture = (currentTexture + 1) % playerSprites.Length;
        //    yield return new WaitForSeconds(0.1f);
        //    if (isActive)
        //    {
        //        StartCoroutine(PlayerAnimation());
        //    }
        //}
    }
