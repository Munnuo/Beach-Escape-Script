using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class setLives : MonoBehaviour
{
    private Text text;
    public int lives;
    private Animator anim;
    private HashIDs hash;

	// Use this for initialization
	void Awake()
    {
        
        text = GetComponent<Text>();
        hash = GameObject.FindGameObjectWithTag(tags.gameController).GetComponent<HashIDs>();
        anim = GameObject.FindGameObjectWithTag(tags.player).GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update()
    {
        text.text = "" + lives;

        if(anim.GetBool(hash.deathBool))
        {
            
        }
   
	}

    void reduceLife()
    {
        
    }
}
