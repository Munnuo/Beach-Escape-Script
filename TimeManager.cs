using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeManager : MonoBehaviour
{
    public static int worldTime;

    Text text;
	// Use this for initialization
	void Awake()
    {
        worldTime = 0;
        text = GetComponent<Text>();
	}
	
    void FixedUpdate()
    {
        worldTime = worldTime + 1;
    }
	// Update is called once per frame
	void LateUpdate()
    {
        text.text = "TIME: " + worldTime;
	}
}
