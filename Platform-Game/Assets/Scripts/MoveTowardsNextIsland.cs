using System;
using System.Collections;
using UnityEngine;


public class MoveTowardsNextIsland : Movement
{
    private TrackOnWhichIslandController trackOnWhichIslandController;


    private GoalTracker goalTracker;

    private Vector3? positionWhereEdgeHit;

    public override bool canMakeMove()
    {
        updatePositionWhereEdgeHit();
        GoalPoint nextGoal = goalTracker.nextGoal();
        return nextGoal != null && isNotAboutToWalkOffIsland() && isNotStuckAtEdge();
    }

    private void updatePositionWhereEdgeHit()
    {
        if (!positionWhereEdgeHit.HasValue && trackOnWhichIslandController.isOnEdgeOfIsland)
        {
            positionWhereEdgeHit = transform.position;
        } else if(positionWhereEdgeHit.HasValue && Vector3.Distance(positionWhereEdgeHit.Value, transform.position) > 0.001f)
        {
            positionWhereEdgeHit = null;
        }
    }

    private bool isNotAboutToWalkOffIsland()
    {
        return !trackOnWhichIslandController.isOnEdgeOfIsland;
    }

    private bool isNotStuckAtEdge()
    {
        return !positionWhereEdgeHit.HasValue;
    }



 


    // Start is called before the first frame update
    void Start()
    {
        trackOnWhichIslandController = GetComponent<TrackOnWhichIslandController>();

        goalTracker = GetComponent<GoalTracker>();
    }

    public override Transform getTransformToMoveTo()
    {
        GoalPoint nextGoal = goalTracker.nextGoal();
        return nextGoal.transform;
    }

    public override void afterMove()
    {
    }
}
