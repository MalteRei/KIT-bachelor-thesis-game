using System.Collections;
using UnityEngine;

public class GoalTracker : MonoBehaviour
{
    private int currentGoal = 0;
    public GoalPoint[] goals;

    public void nextGoalReached()
    {
        GoalPoint nextGoal = this.nextGoal();
        nextGoal.VisitGoalPoint();
        currentGoal++;
    }

    public GoalPoint nextGoal()
    {
        if (currentGoal < goals.Length - 1)
        {
            return goals[currentGoal + 1];
        }
        return null;

    }

    public GoalPoint LastReachedGoal()
    {
        if(currentGoal < goals.Length)
        {
            return goals[currentGoal];
        }
        return null;
    }
}
