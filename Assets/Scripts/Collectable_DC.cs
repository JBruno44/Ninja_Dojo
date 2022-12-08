using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable_DC : MonoBehaviour
{
    // Start is called before the first frame update
    public Manager_DC manager;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject gameObject = collision.gameObject;
        if (gameObject.tag == "Player")
        {
            manager.BonusPoints();
            Destroy(this.gameObject);
        }
    }
}
