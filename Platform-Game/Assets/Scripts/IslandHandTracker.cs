using System.Collections;
using UnityEngine;


public class IslandHandTracker : MonoBehaviour
{
    public bool isHandInProximityOrOnThisIsland { get; set; } = false;


    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Left Palm" || other.name == "Right Palm")
        {
            isHandInProximityOrOnThisIsland = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Left Palm" || other.name == "Right Palm")
        {
            isHandInProximityOrOnThisIsland = false;
        }
    }
}
