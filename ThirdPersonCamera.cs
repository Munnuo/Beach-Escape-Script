using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;
    public GameObject player;
    public float speed = 0.1f;

    private float distance = -2f;
    private float height = 0f;
    private float cHeight;
    private Transform userTransform;
    private Animator anim;
    private HashIDs hash;
    private bool camXNeg;
    private bool camXPos;
    private Vector3 offsetX;
    private float angX;
   

    

	// Use this for initialization
	void Start ()
    {
        if(target == null)
        {
            Debug.LogWarning("No target selected");
        }

        if(player == null)
        {
            Debug.LogWarning("A player is needed");
        }


        userTransform = transform;

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
        
        offsetX = Quaternion.AngleAxis(angX * speed*.2f, Vector3.up) * offsetX;

        if(!anim.GetBool(hash.deathBool))
        userTransform.position = target.position + offsetX;
      
        userTransform.LookAt(target);

    }
}
