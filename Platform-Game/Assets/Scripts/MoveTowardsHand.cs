using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using UnityEngine;


public class MoveTowardsHand : Movement
{

    private TrackOnWhichIslandController trackOnWhichIslandController;

    private Transform latestHandTransform;

    public override bool canMakeMove()
    {
        return isHandOnSameIsland() && isCharacterNotOnHand();
    }

    private bool isHandOnSameIsland()
    {
        
        if (trackOnWhichIslandController != null && trackOnWhichIslandController.currentIslandCharacterOn != null)
        {
            Island currentIslandCharacterOn = trackOnWhichIslandController.currentIslandCharacterOn;
            latestHandTransform = getHandTransformToMoveTo();
            return latestHandTransform != null && currentIslandCharacterOn.isInProximity(latestHandTransform.position);
            /*
            IslandHandTracker islandHandTracker = trackOnWhichIslandController.currentIslandCharacterOn.GetComponentInChildren<IslandHandTracker>();
            return islandHandTracker.isHandInProximityOrOnThisIsland;*/
        }
        return false;

    }

    private bool isCharacterNotOnHand()
    {
        //TODO implement
        return true;
    }



    // Use this for initialization
    void Start()
    {
        trackOnWhichIslandController = GetComponent<TrackOnWhichIslandController>();

    }

    public override Transform getTransformToMoveTo()
    {
        return latestHandTransform;
    }

    private Transform getHandTransformToMoveTo()
    {
        var handJointService = CoreServices.GetInputSystemDataProvider<IMixedRealityHandJointService>();
        if (handJointService != null)
        {
            Transform palmTransform = handJointService.RequestJointTransform(TrackedHandJoint.Palm, Handedness.Right);
            if (palmTransform != null)
            {
                return palmTransform;
            }

        }

        return null;
    }

    public override void afterMove()
    {
    }
}