using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerLook_JB : MonoBehaviour
{
    public Vector3 mousePosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = mousePosition - transform.position;

        transform.up = direction;

    }
}


