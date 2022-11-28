using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai_JB : MonoBehaviour
{
   

    void Start()
    {

        Destroy(this.gameObject, 5);

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = new Vector3(20 * transform.up.x * Time.deltaTime, 20 * transform.up.y * Time.deltaTime, 0);
        this.transform.position += newPosition;

    }



//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        GameObject gameObject = collision.gameObject;
//        if (gameObject.tag == "")
//        {
//            Constants.C.currentscore += 5;
//            Destroy(gameObject);
//            GameObject g = Instantiate(largeAsteroid);
//            g.transform.position = new Vector3(Random.Range(-4, 4), Random.Range(-4, 4), 0);

//            Destroy(this.gameObject);
//        }

//        if (gameObject.tag == "Large")
//        {
//            Constants.C.currentscore += 1;
//            Destroy(gameObject);
//            GameObject g = Instantiate(smallAsteroid1);
//            GameObject h = Instantiate(smallAsteroid1);
//            g.transform.position = new Vector3(Random.Range(-4, 4), Random.Range(-4, 4), 0);
//            h.transform.position = new Vector3(Random.Range(-4, 4), Random.Range(-4, 4), 0);


//            Destroy(this.gameObject);

//        }

//    }


}
