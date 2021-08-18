using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private float yBelowToRespawn = -0.9f;
    private GoalTracker goalTracker;
    // Start is called before the first frame update
    void Start()
    {
        goalTracker = GetComponent<GoalTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y <= yBelowToRespawn)
        {
            RespawnCharacter();
        }
    }


    private void RespawnCharacter()
    {
        if (goalTracker != null)
        {
            GoalPoint lastReachedGoal = goalTracker.LastReachedGoal();
            if (lastReachedGoal != null)
            {
                gameObject.transform.position = lastReachedGoal.gameObject.transform.position;

            }
        }


    }
}
