using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class death : MonoBehaviour
{
    public string level;

    private PlayerMovement player;
    private Transform body;
    private Vector3 pos;
    private Animator anim;
    private HashIDs hash;
    private Collider water;
    private bool touchingWater;


	void Start ()
    {
        body = GetComponent<Transform>();
        hash = GameObject.FindGameObjectWithTag(tags.gameController).GetComponent<HashIDs>();
        player = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
       
	}
	
    void FixedUpdate()
    {
        
        if ( body.position.y < -2.3f)
        {
            anim.SetBool(hash.deathBool, true);
        }

    }

    void Update()
    {
        if(anim.GetBool(hash.deathBool))
        {
            player.enabled = false;
            
        }

        if(body.position.y < -9)
        {
            SceneManager.LoadScene(level);
        }
    }

    
}
