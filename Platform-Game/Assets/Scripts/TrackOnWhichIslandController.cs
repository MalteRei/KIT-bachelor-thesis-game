using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackOnWhichIslandController : MonoBehaviour
{
    private Island _currentIslandCharacterOn;
    public Island currentIslandCharacterOn { get => _currentIslandCharacterOn; set 
            {
            if (characterJumpController != null && _currentIslandCharacterOn != value)
            {
                characterJumpController.Jump();
            }
            _currentIslandCharacterOn = value; 
            
        } 
    }

    public bool isOnEdgeOfIsland;

    private JumpController characterJumpController;

    // Start is called before the first frame update
    void Start()
    {
        characterJumpController = GetComponent<JumpController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isOnIsland(GameObject island)
    {
        return currentIslandCharacterOn.name == island.name;
    }

}
