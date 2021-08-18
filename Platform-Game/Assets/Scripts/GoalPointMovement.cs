using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GoalPointMovement : Movement
{
    private float deltaDistanceToGoalReached = 0.06f;


    private TrackOnWhichIslandController trackOnWhichIslandController;

    private GoalTracker goalTracker;

    // Start is called before the first frame update
    void Start()
    {
        trackOnWhichIslandController = GetComponent<TrackOnWhichIslandController>();

        goalTracker = GetComponent<GoalTracker>();
    }





    public override bool canMakeMove()
    {
        if (isInReachOfNextGoal())
        {
            goalTracker.nextGoalReached();
        }
        return this.canMoveToNextGoal();
    }

    private bool canMoveToNextGoal()
    {
        GoalPoint nextGoal = goalTracker.nextGoal();

       
        
        return nextGoal != null && !nextGoal.visited && trackOnWhichIslandController.isOnIsland(nextGoal.islandPlacedOn) && !isInReachOfNextGoal();
    }

    public override Transform getTransformToMoveTo()

    {
        GoalPoint nextGoal = goalTracker.nextGoal();
        return nextGoal.transform;
    }

    public override void afterMove()
    {


        /*
        if (isInReachOfNextGoal())
        {
            goalTracker.nextGoalReached();
        }*/
    }

    private bool isInReachOfNextGoal()
    {

        GoalPoint nextGoal = goalTracker.nextGoal();
        if(nextGoal != null)
        {
            Transform nextGoalTransform = nextGoal.transform;
            Vector3 nextGoalPosition = nextGoalTransform.position;
            return Vector3.Distance(nextGoalPosition, transform.position) <= deltaDistanceToGoalReached;
        } else
        {
            return false;
        }
        
    }
}
