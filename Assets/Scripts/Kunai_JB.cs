using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai_JB : MonoBehaviour
{
    public Manager_DC manager;
    public Vector3 mousePosition;


    void Start()
    {

        Destroy(this.gameObject, 5);

        //rotates kunai
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = mousePosition - transform.position;

        transform.up = direction;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = new Vector3(20 * transform.up.x * Time.deltaTime, 20 * transform.up.y * Time.deltaTime, 0);
        this.transform.position += newPosition;

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject gameObject = collision.gameObject;
        if (gameObject.tag == "Ninja")
        {
            float ninjaX = 0.0f;
            float ninjaY = 0.0f;
            ninjaX = gameObject.transform.position.x;
            ninjaY = gameObject.transform.position.y;
            Destroy(gameObject);
            manager.NinjaKilled(ninjaX, ninjaY);

            Destroy(this.gameObject);

        }



    }


}
