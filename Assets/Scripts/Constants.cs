using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
    static public Constants C;

    public int currentscore = 0;

    public float rotationAmount = 0;

    private void Awake()
    {
        C = this; // C now points to Constants for entire
                  // time the game runs
    }
    public void Test()
    {
        Debug.Log("In test");
    }

}