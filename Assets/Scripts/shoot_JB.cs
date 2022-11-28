using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot_JB : MonoBehaviour
{
    public GameObject bullet;
    public GameObject shootPoint;
   // private AudioSource shurikenThrow;
    private bool inCoolDown = false;

    void Start()
    {
        shootPoint.transform.position = new Vector3(transform.up.x, transform.up.y + 1, 0);
        //shurikenThrow = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {


        // shootPoint.transform.position = new Vector3().;

        shootPoint.transform.position = this.transform.position;
       // shootPoint.transform.Rotate(0, 0, Constants.C.rotationAmount);
        if (Input.GetKeyDown(KeyCode.Mouse0) && !inCoolDown)
        {
            inCoolDown = true;


            GameObject N = Instantiate(bullet);
            N.transform.position = (shootPoint.transform.position);
            N.transform.Rotate(0, 0, Constants.C.rotationAmount);
            StartCoroutine(CoolDown());
          //  if (!shurikenThrow.isPlaying)
              //  shurikenThrow.PlayOneShot(shurikenThrow.clip, 1.0f);

        }
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(0.7f);
        inCoolDown = false;
    }

}
