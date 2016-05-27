using UnityEngine;
using System.Collections;

public class PlaySound : MonoBehaviour
{
    private Animator anim;
    private HashIDs hash;

	// Use this for initialization
	void Start ()
    {
        anim = GameObject.FindGameObjectWithTag(tags.player).GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag(tags.gameController).GetComponent<HashIDs>();
    }
	
	// Update is called once per frame
	void FixedUpdate()
    {
        if (anim.GetBool(hash.deathBool))
        {
            if (!GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().Play();
        }
        else
        {
            GetComponent<AudioSource>().Stop();
        }
	}
}
