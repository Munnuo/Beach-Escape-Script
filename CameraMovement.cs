using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    public float smooth = 1.5f;         // the relative speed at which the camera will catch up

    private Transform player;           //reference to the players transform relative to the world
    private Vector3 relCameraPos;       // The relative position of the camera from the player
    private float relCameraPosMag;      // The distance of the camera from the player
    private Vector3 newPos;             // The position the camera is trying to reach

    void Awake()
    {
        // setting up the references here
        player = GameObject.FindGameObjectWithTag(tags.playerview).transform;
        relCameraPos = transform.position;
        relCameraPosMag = relCameraPos.magnitude;
    }

    void FixedUpdate()
    {
        Vector3 standardPos = relCameraPos;
        Vector3 abovePos = player.position + Vector3.up * relCameraPosMag;
        Vector3[] checkPoints = new Vector3[6];
        checkPoints[0] = standardPos;
        checkPoints[1] = Vector3.Lerp(standardPos, abovePos, 0.01f);
        checkPoints[2] = Vector3.Lerp(standardPos, abovePos, 0.25f);
        checkPoints[3] = Vector3.Lerp(standardPos, abovePos, 0.5f);
        checkPoints[4] = Vector3.Lerp(standardPos, abovePos, 0.75f);
        checkPoints[5] = abovePos;

        for(int i = 0; i < checkPoints.Length; i++)
        {
            if(ViewingPosCheck(checkPoints[i]))
            {
                break;
            }
        }

        transform.position = Vector3.Lerp(transform.position, newPos, smooth * Time.deltaTime);
        SmoothLookAt();
    }

    bool ViewingPosCheck(Vector3 checkPos)
    {
        RaycastHit hit;

        if(Physics.Raycast(checkPos, player.position - checkPos, out hit, relCameraPosMag))
        {
            if(hit.transform != player)
            {
                return false;
            }
        }

        newPos = checkPos;
        return true;
    }

    void SmoothLookAt()
    {
        Vector3 relPlayerPosition = player.position - transform.position;
        Quaternion lookAtRotation = Quaternion.LookRotation(relPlayerPosition, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookAtRotation, smooth * Time.deltaTime);
    }
	
}
