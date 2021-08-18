using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDetectorController : MonoBehaviour
{
    public JumpController characterJumpController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Right Palm" || other.name == "Left Palm")
        {
            characterJumpController.Jump();
        }
    }
}
