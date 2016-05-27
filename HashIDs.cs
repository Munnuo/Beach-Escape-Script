using UnityEngine;
using System.Collections;

public class HashIDs : MonoBehaviour
{
    /* 
       This class is used to reference all the different states of the animator
       as well as the different parameters defined in the animator
       Any changes I want to be made to the parameter or the animator 
       I do so by referencing this variables through this class

    */

    public int idleState; // The Idle state of the player animator
    public int locomotionState; // The motion state of the player animator
    public int speedFloat; // the speed parameter of the player animator
    public int crouchState; // the crouching state of the player animator
    public int crouchBool; // the crouching parameter of the player animator
    public int crouchWalk; // the crouch walk state of the player animator
    public int jumpState; // the jump state of the player animator
    public int jumpSpeed; // the jump speed parameter of the player animator
    public int jumpBool; // the jump parameter of the player animator
    public int deathBool; // the death flag of the player

  
   

    void Awake()
    {
        idleState = Animator.StringToHash("Base Layer.Idle");
        locomotionState = Animator.StringToHash("Base Layer.Locomotion");
        speedFloat = Animator.StringToHash("Speed");
        crouchState = Animator.StringToHash("Base Layer.Crouch");
        crouchWalk = Animator.StringToHash("Base Layer.Crouch Walk");
        crouchBool = Animator.StringToHash("Crouch");
        jumpState = Animator.StringToHash("Base Layer.Jump");
        jumpSpeed = Animator.StringToHash("JumpSpeed");
        jumpBool = Animator.StringToHash("Jump");
        deathBool = Animator.StringToHash("Death");
        
        
    }
	
}
