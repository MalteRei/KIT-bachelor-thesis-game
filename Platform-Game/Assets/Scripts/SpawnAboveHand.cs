using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAboveHand : MonoBehaviour
{

    public GameObject objectToSpawn;
    private int numberSecondsBetweenSpawn = 5;
    // Start is called before the first frame update
    void Start()
    {
       // InvokeRepeating("SpawnObject", numberSecondsBetweenSpawn, numberSecondsBetweenSpawn);
    }


    private void SpawnObject()
    {
        var handJointService = CoreServices.GetInputSystemDataProvider<IMixedRealityHandJointService>();
        if (handJointService != null)
        {
            Transform jointTransform = handJointService.RequestJointTransform(TrackedHandJoint.Palm, Handedness.Right);
            // ...
            Instantiate(objectToSpawn, jointTransform.position + Vector3.up * 4, objectToSpawn.transform.rotation);
        }
        
    }

   
}
