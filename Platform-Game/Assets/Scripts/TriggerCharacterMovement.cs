using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCharacterMovement : MonoBehaviour
{

    private CharacterMovement characterMovement;

    // Start is called before the first frame update
    void Start()
    {
        characterMovement = GameObject.Find("Character").GetComponent<CharacterMovement>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Left Palm" || other.name == "Right Palm")
        {
            //characterMovement.StartMovingToPosition(1);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Left Palm" || other.name == "Right Palm")
        {
            //characterMovement.StopMovingToPosition();
        }
    }
}
