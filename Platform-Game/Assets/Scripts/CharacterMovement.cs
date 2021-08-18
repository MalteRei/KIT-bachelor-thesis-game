using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    private readonly float deltaDistance = 0.05f;
    
    public void makeMove(float speed)
    {
        if (this.canMakeMove())
        {
            Transform destination = getTransformToMoveTo();
            if (destination != null && Vector3.Distance(destination.position, transform.position) > deltaDistance)
            {
               // transform.LookAt(destination);
                transform.LookAt(new Vector3(destination.position.x,transform.position.y , destination.position.z));
                Vector3 moveTowardsDestination = Vector3.MoveTowards(transform.position, destination.position, speed);
                transform.position = new Vector3(moveTowardsDestination.x, transform.position.y, moveTowardsDestination.z);

            }

            afterMove();
        }
    }
    public abstract bool canMakeMove();
    public abstract Transform getTransformToMoveTo();

    public abstract void afterMove();


   
}

public class CharacterMovement : MonoBehaviour
{


    public Movement[] possibleMovements;
    private float speed = 0.004f;
    public readonly static float FAST_SPEED = 0.006f;
    public readonly static float NORMAL_SPEED = 0.004f;
    public readonly static float SLOW_SPEED = 0.002f;


    // Animation
    private Animator playerAnimator;
    private readonly static float SPEED_F_WAITING = 0;
    private readonly static float SPEED_F_WALKING = 0.3f;
    private readonly static string SPEED_F = "Speed_f";

    public HandActuatorClient handActuatorClient;

    public AudioSource walkingAudioSource;


    // Start is called before the first frame update
    void Start()
    {
        SetSpeed(NORMAL_SPEED);
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var movement in possibleMovements)
        {
            if (movement.canMakeMove())
            {
                movement.makeMove(speed);
                WalkAnimation();
                return;
            }
        }

        WaitAnimation();

    }


    void WalkAnimation()
    {
        playerAnimator.SetFloat(SPEED_F, speed);
       this.handActuatorClient.PlayVibration();
        if(walkingAudioSource != null && !walkingAudioSource.isPlaying)
        {
            
            walkingAudioSource.Play();
        }
    }

    void WaitAnimation()
    {
        playerAnimator.SetFloat(SPEED_F, SPEED_F_WAITING);
        this.handActuatorClient.StopVibration();
        if (walkingAudioSource != null && walkingAudioSource.isPlaying)
        {
            walkingAudioSource.Stop();
        }
    }

    public void SetSpeed(float newSpeed)
    {   if(speed != newSpeed)
        {
            this.speed = newSpeed;
            if(speed <= SLOW_SPEED)
            {
               walkingAudioSource.pitch = 1f;
                this.handActuatorClient.SetSlowWalkVibration();
            } else if(speed >= FAST_SPEED)
            {
                walkingAudioSource.pitch = 2f;

                this.handActuatorClient.SetRunVibration();
            } else
            {
                walkingAudioSource.pitch = 1.5f;

              this.handActuatorClient.SetWalkVibration();
            }
        }

    }

}
