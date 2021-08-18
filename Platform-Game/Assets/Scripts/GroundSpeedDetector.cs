using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpeedDetector : MonoBehaviour
{

    public CharacterMovement characterMovement;
    

    private void OnTriggerEnter(Collider other)
    {
        GroundSpeed groundSpeedComponent = other.GetComponent<GroundSpeed>();
        if(groundSpeedComponent != null && characterMovement != null)
        {
            characterMovement.SetSpeed(groundSpeedComponent.Speed);
            
        }
    }
}
