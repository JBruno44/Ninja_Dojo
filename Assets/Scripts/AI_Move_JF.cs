using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Move_JF : MonoBehaviour
{
    public enum AIType { vector, delta, evade, none, patternMove };
    public AIType aiType = AIType.none;
    public float speed = 5;

    private GameObject player;
    private string name;
   // private AIPatterns patterns;
   // private Waypoints waypoints;

    private bool isInvisible = false;
    private Renderer renderer;
    void Start()
    {
        renderer = GetComponent<Renderer>();
        name = "AI_Enemy" + Time.time.ToString();
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
            Debug.Log("AI::Start couldn't find player for " + name.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 temp = speed * ProcessAI() * Time.deltaTime * WrapPlayer();
        transform.position += new Vector3(temp.x, temp.y, 0);

    }

    Vector2 ProcessAI()
    {

        Vector3 dir = player.transform.position - this.transform.position;
        Vector2 returnDir = Vector2.zero;
        //returnDir = new Vector2(dir.x, dir.y);
        switch (aiType)
        {
            case AIType.none:
                break;
            case AIType.vector:
                returnDir = VectorTrack(dir);
                speed = 1;
                break;
            case AIType.delta:
                returnDir = deltaTrack(dir);
                speed = 0.5f;
                break;
            case AIType.evade:
                returnDir = evade(dir);
                break;
            /*case AIType.patternMove:
                patterns = GetComponent<AIPatterns>();
                returnDir = patterns.evaluatePattern();
                break;
            case AIType.waypoints:
                //returnDir= waypoints.closestWaypoint();
                returnDir = waypoints.evaluateWaypoints();
                break;
            default:
                break;*/
        }
        //Quick and dirty fix for "random close" movement
        if (Mathf.Abs(returnDir.x) < 0.1)
            returnDir.x = 0;
        if (Mathf.Abs(returnDir.y) < 0.1)
            returnDir.y = 0;
        return returnDir;
    }
    private Vector2 deltaTrack(Vector3 rawDirection)
    {
        Vector2 tempDir = Vector2.zero;
        float deltaX = player.transform.position.x - transform.position.x;
        float deltaY = player.transform.position.y - transform.position.y;
        if (deltaX > 0.1)
        {
            tempDir.x = 1;
        }
        else if (deltaX < -0.1)
        {
            tempDir.x = -1;
        }
        else if (deltaY > 0.1)
        {
            tempDir.y = 1;
        }
        else if (deltaY < -0.1)
        {
            tempDir.y = -1;
        }
        float distance = rawDirection.magnitude;
        if (distance < 2)
            speed = 1;
        else
            speed = 5;
        return tempDir;
    }

    private Vector2 evade(Vector3 rawDirection)
    {
        Vector2 tempDir = Vector2.zero;
        float distance = rawDirection.magnitude;
        if (distance < 2)
        {
            tempDir = -1 * VectorTrack(rawDirection);
            return tempDir;
        }
        return VectorTrack(rawDirection);
    }
    private Vector2 VectorTrack(Vector3 rawDirection)
    {
        Vector2 temp = new Vector2(rawDirection.x, rawDirection.y);
        temp.Normalize();
        return temp;
    }

    private Vector2 WrapPlayer()
    {
        if (renderer.isVisible)
        {
            isInvisible = false;
            return new Vector2(1, 1);
        }
        if (isInvisible)
        {
            return new Vector2(1, 1);
        }
        float xFix = 1;
        float yFix = 1;
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0 || pos.x > 1)
            xFix = -1;
        if (pos.y < 0 || pos.y > 1)
            yFix = -1;
        isInvisible = true;
        return new Vector2(xFix, yFix);
    }
}
