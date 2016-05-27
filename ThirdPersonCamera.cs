using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;	// public reference to camera view object transform that the editor selects
    public GameObject player;	// public reference to any game object that editor selects inside unity editor
    public float speed = 0.1f;	// float that defines the speed of the camera movement.

    private float distance = -2f;	// distance between the camera and the targeted player (Game Object)
    private float height = 0f;		// height of the camera in the Y axis relative to the player
    private float cHeight;		// Change in height of the camera in the Y axis
    private Transform userTransform;	// the transform of input received from the user
    private Animator anim;		// The reference to the animator component 
    private HashIDs hash;		// The reference to the hash IDs used to label specific objects in-game
    private bool camXNeg;		// The negative boolean for camera movement
    private bool camXPos;		// The positive boolean for camera movement
    private Vector3 offsetX;		
    private float angX;
   

    

	// Use this for initialization
	void Start ()
    {
        if(target == null)	// Check to see if a camera view object is selected
        {
            Debug.LogWarning("No target selected");
        }

        if(player == null)	// check to see if a player object is selected
        {
            Debug.LogWarning("A player is needed");
        }


        userTransform = transform;	// The transform of the camera

        anim = player.GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag(tags.gameController).GetComponent<HashIDs>();

        offsetX = new Vector3(distance, height + cHeight, 0);

    }

    // FixedUpdate is called once per frame, at the same time intervals from each other
    // used to update settings of the players new position in the world
    // In this update it also responds to the players change to the camera position
	void FixedUpdate()
    {
        float c = Input.GetAxisRaw("CameraX");
        speed = Mathf.Abs(c);

        // The rotation around the character on the Y axis
        if (c < 0)
        {
            angX = -45;
        }
        else if(c > 0)
        {
            angX = 45;
        }
        else
        {
            angX = 0;
        }
        
       
       // Change the camera position while the character is crouching.
	    if(anim.GetBool(hash.crouchBool))
        {
            if(offsetX.y < 0.5f)
            {
                cHeight = 0.5f;
            }
            else if(offsetX.y == 0.5f)
            {
                cHeight = 0;
            }
            else
            {

            }
            
  
        }
        else
        {
            if (offsetX.y > 0)
            {
                cHeight = -0.5f;
            }
            else
            {
                cHeight = 0f;
            }
            
      
        }

        offsetX = new Vector3(offsetX.x, offsetX.y + cHeight, offsetX.z);
    }


    //LateUpdate is called once per frame, after every other update function has been called.
    // This is utilized to make sure the camera reacts to the player movement and not before the changes.

    void LateUpdate()
    {
        
        offsetX = Quaternion.AngleAxis(angX * speed*.2f, Vector3.up) * offsetX; // This value keeps the camera at a constant distance
        									// away from the player

        if(!anim.GetBool(hash.deathBool)) // if the player dies, the camera stops following them.
        userTransform.position = target.position + offsetX; // This line changes the position of the camera to follow the player
      
        userTransform.LookAt(target); // this changes the camera view to look at the targeted camera view

    }
}
