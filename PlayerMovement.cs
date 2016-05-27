using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{


    public float turnSmoothing = 15f;   // A smoothing value for turning the player.
    public float speedDampTime = 0.1f;  // The damping for the speed parameter
    public Transform playerCam;         // Reference to the camera that represents the players view

    private Animator anim;              // Reference to the animator component.
    private HashIDs hash;               // Reference to the HashIDs class.
    private Vector3 forward;            // Refers to the player's camera facing direction
    private Vector3 right;              // Refers to the right-hand direction relative to the facing direction of the player's camera
    private Vector3 moveDir;            // The calculated direction of movement of the character based upon the camera's facing directions
    private float distance;             // The distance of the player from the ground
    private bool gameStart;             // Boolean to reference to the object that the game has started.
    private Rigidbody body;             // The rigidbody component attached to the player for physics purposes.
    private float maxVel;               // Maximum running velocity
    private float maxCrVel;             // Maximum crouch velocity
    private float curVel;               // The current velocity of the player
    private float brakeVel;             // The brake velocity being applied to player

    void Awake()
    {
        // Setting up the references to the gameController object
        anim = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag(tags.gameController).GetComponent<HashIDs>();

        body = GetComponent<Rigidbody>();

        distance = GetComponent<Collider>().bounds.extents.y;
        gameStart = true;

        // Velocity manager floats
        maxVel = 5.0f;
        maxCrVel = 1.0f;

        
      
    }


    void FixedUpdate()
    {
        // Cache the inputs.
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool crouch = Input.GetButton("Crouch");
        bool jump = Input.GetButtonDown("Jump");

        // get the current velocity of the player
        curVel = Vector3.Magnitude(body.velocity);
        
        if(jump && isGrounded())
        {
            
            if(anim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            {
                // do not change the state if already jumping
                
            }
        /*    else if(anim.GetCurrentAnimatorStateInfo(0).IsName("airborne"))
            {
                // do not change the state if already jumping
            }
            else if(anim.GetCurrentAnimatorStateInfo(0).IsName("land"))
            {
                // do not change the state if already jumping
            }
          */else
            {
                anim.SetBool(hash.jumpBool, true);
                body.AddForce(transform.up * 100f, ForceMode.Impulse);
                Invoke("stopJumping", 1f);

            }
            
        }
       
        /*
            here is where the facing direction of the camera is used
            to calculate the moving direction of the character relative
            to the camera's facing direction.
        */

        forward = playerCam.transform.TransformDirection(Vector3.forward);
        forward.y = 0;
        forward = forward.normalized;
        right = new Vector3(forward.z, 0, -forward.x);

        /*
            The moving direction is calculated using the input of both the
            x and z axis input (not the y because the game is in 3D) and then
            multiply by the vectors. forward representing the back and forth direction multiplied
            by the horizontal input, and the right representing the side to side direction multiplied
            by the vertical input.
        */

        moveDir = (h * forward + -v * right).normalized;

        MovementManagement(moveDir.x, moveDir.z, crouch, jump);  
        
        /* 
            This if statement prevents character from moving too fast. 
        */  
        //if()
        if(curVel > maxVel)
        {
            brakeVel = curVel - maxVel;

            Vector3 normalizedVel = body.velocity.normalized;
            Vector3 brakeVelo = normalizedVel * brakeVel;


            body.AddForce(-brakeVelo);
          
        }
        
        // This if-statement does the same as the above, but manage
        //  The player's velocity when he is crouched. 
        if(crouch)
        {
            if (curVel > maxCrVel)
            {
                brakeVel = curVel - maxCrVel;

                Vector3 normalizedVel = body.velocity.normalized;
                Vector3 brakeVelo = normalizedVel * brakeVel;


                body.AddForce(-brakeVelo);

            }

        }

        

    }

    void Update()
    {
        AudioManagement(gameStart);

        if (Time.timeScale == 0.0f)
        {
            gameStart = false;
        }
        else if (Time.timeScale == 1.0f)
        {
            gameStart = true;
        }

        
    }

    /*
        Check to see if the player is currently touching a surface
    */

    bool isGrounded()
    {
        print(Physics.Raycast(transform.position, -Vector3.up, distance + 0.1f));
        return Physics.Raycast(transform.position, -Vector3.up, distance + 0.1f);
   
    }

    /*
        Set the jumping parameter to false
    */

    void stopJumping()
    {
        anim.SetBool(hash.jumpBool, false);
    }

    /*
        MovementManagement(float x, float z, bool c)

        this function takes two float of values representing x axis input and z axis input respectively. 
        With those values, this function determines if the player is moving or not moving. The direction
        of the movement is determined by the function rotating() which the same two values are used for parameters
        rotating();

        the bool c represents the crouch button. If true, the player is pressing crouch, else the player is not 
        pressing crouch.
        When the player does press crouch the speed value is 3.5, compare to if the player does not press crouch 
        the value being 5.5.

        If input of both axis x and z are equal to 0, nothing occurs besides setting the speedfloat to 0.
    */
    void MovementManagement(float horizontal, float vertical, bool crouching, bool jump)
    {
        // Setting the crouching parameter to true if the crouch button is held down
        anim.SetBool(hash.crouchBool, crouching);

        // If there is some axis input...
        if (horizontal != 0f || vertical != 0f)
        {
            if(crouching)
            {
                // ... set the players rotation and set the speed parameter to 3.5f.
                Rotating(horizontal, vertical);
                anim.SetFloat(hash.speedFloat, 3.5f, speedDampTime, Time.deltaTime);
                if (curVel < maxCrVel)
                {
                    body.AddForce(new Vector3(moveDir.x * 1000f, 0, moveDir.z * 1000f));
                }
            }
            else
            {
                // ... set the players rotation and set the speed parameter to 5.5f.
                Rotating(horizontal, vertical);
                anim.SetFloat(hash.speedFloat, 5.7f, speedDampTime, Time.deltaTime);
                //body.velocity = new Vector3(moveDir.x*5.5f,0,moveDir.z * 5.5f);
                if(curVel< maxVel)
                {
                    body.AddForce(new Vector3(moveDir.x * 1000f, 0, moveDir.z * 1000f));
                }
                
               
            }
           
        }
        else
        {
            // Otherwise set the speed parameter to 0.
            anim.SetFloat(hash.speedFloat, 0);
            
        }
            
            
        
    }

    void Rotating(float horizontal, float vertical)
    {
        // Create a new vector of the horizontal and vertical inputs.
        Vector3 targetDirection = new Vector3(horizontal, 0f, vertical);

        // Create a rotation based on this new vector assuming that up is the global y axis.
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

        // Create a rotation that is an increment closer to the target rotation from the player's rotation.
        Quaternion newRotation = Quaternion.Lerp(GetComponent<Rigidbody>().rotation, targetRotation, turnSmoothing * Time.deltaTime);

        // Change the players rotation to this new rotation.
        GetComponent<Rigidbody>().MoveRotation(newRotation);
    }

    /*
        The AudioManagement() function makes sure the footstep soundtrack attached
        to the player is playing when necessary
    */

    
    void AudioManagement(bool started)
    {
        if(started)
        {


            // If the player is currently in the run state...
            if (anim.GetCurrentAnimatorStateInfo(0).fullPathHash == hash.locomotionState)
            {

                // ... and if the footsteps are not playing...
                if (!GetComponent<AudioSource>().isPlaying)
                    // ... play them.
                    GetComponent<AudioSource>().Play();
            }
            else
                // Otherwise stop the footsteps.
                GetComponent<AudioSource>().Stop();

        }
        else
        {
            GetComponent<AudioSource>().Stop();
        }

       

    }

}
