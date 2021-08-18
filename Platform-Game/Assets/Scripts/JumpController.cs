using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{

    private Rigidbody characterRigidbody;
    public float jumpForce = .2f;
    public float fallMultiplier = 1.2f;

   /* public float radiusGroundedCheckSphere = 0.02f;
    private readonly Vector3 groundedCheckSpherePositionTransform = Vector3.down * 0.005f;
   */
    private readonly int groundLayer = 1;

    private CapsuleCollider capsuleCollider;


    private Animator playerAnimator;
    private readonly static string JUMP_TRIGGER = "Jump_trig";

    public AudioSource jumpAudioSource;



    // Start is called before the first frame update
    void Start()
    {
        characterRigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /* private void OnTriggerExit(Collider other)
     {
         if (other.CompareTag("JumpOffTrigger"))
         {
             JumpTrigger jumpTrigger = other.GetComponent<JumpTrigger>();
             if (jumpTrigger != null)
             {
                 if (!trackOnWhichIslandController.isOnIsland(jumpTrigger.parentPlatform))
                 {
                     // We are not on this island an thus should not try to jump off it
                     return;
                 }
             }
             Jump();
         }


     }

     private void OnTriggerEnter(Collider other)
     {
         if (other.CompareTag("JumpOnTrigger"))
         {
             JumpTrigger jumpTrigger = other.GetComponent<JumpTrigger>();
             if (jumpTrigger != null)
             {
                 if (trackOnWhichIslandController.isOnIsland(jumpTrigger.parentPlatform))
                 {
                     // We are already on this island an thus should not try to jump on it
                     return;
                 }

                 //trackOnWhichIslandController.currentIslandCharacterOn = jumpTrigger.parentPlatform;
             }
             Jump();
         }

     }*/

    /*private bool IsGrounded()
    {
        //get the radius of the players capsule collider, and make it a tiny bit smaller than that
        // float radius = collider.radius * 0.9f;
        //get the position (assuming its right at the bottom) and move it up by almost the whole radius
        // Vector3 pos = transform.position + (Vector3.down * 0.2f) + Vector3.up * (radius * 0.9f);
        //returns true if the sphere touches something on that layer
        //isGrounded = Physics.CheckSphere(pos, radius, groundLayer);
        // Bit shift the index of the layer (0) to get a bit mask
        int layerMask = 1;
        bool isGrounded = Physics.CheckSphere(transform.position + groundedCheckSpherePositionTransform, radiusGroundedCheckSphere, layerMask);
        var colliders = Physics.OverlapSphere(transform.position + groundedCheckSpherePositionTransform, radiusGroundedCheckSphere, layerMask);
        foreach (var item in colliders)
        {
            Debug.Log(item);
        }
        return isGrounded;
    }*/

    private bool IsGrounded()
    {
        //get the radius of the players capsule collider, and make it a tiny bit smaller than that
        float radius = capsuleCollider.radius * 0.9f;
        //get the position (assuming its right at the bottom) and move it up by almost the whole radius
        Vector3 pos = transform.position + (Vector3.down * 0.2f) + Vector3.up * (radius * 0.9f);
        //returns true if the sphere touches something on that layer
        bool isGrounded = Physics.CheckSphere(pos, radius, groundLayer);
        return isGrounded;
    }

    public void Jump()
    {
        if (IsGrounded())
        {
            characterRigidbody.velocity = new Vector3(characterRigidbody.velocity.x, jumpForce, characterRigidbody.velocity.z);
            jumpAudioSource.Play();
            JumpAnimation();
            MakeJumpCrisp();
        }



    }

    void MakeJumpCrisp()
    {
        if (characterRigidbody.velocity.y < 0)
        {
            // falling => apply fall multiplier
            characterRigidbody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

    }

    private void JumpAnimation()
    {
        playerAnimator.SetTrigger(JUMP_TRIGGER);
    }




}
