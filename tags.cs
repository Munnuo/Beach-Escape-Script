using UnityEngine;
using System.Collections;

public class tags : MonoBehaviour
{   /*
        This class is used to reference the in-game tags identify by the creator
        to allow other scripts and class to access components of other in-game objects
        without directly creator variables in each class for each tag.

    */
    public const string player = "Player";
    public const string playerview = "PlayerView";
    public const string gameController = "GameController";
    public const string water = "Water";
	
}
