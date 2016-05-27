using UnityEngine;
using System.Collections;

public class rotatingPlatform : MonoBehaviour
{ 

    private float spinSpeed;
    private float curSpeed;
    private float brakeSpeed;
    private Rigidbody platform;
    private Transform dock;

    private Quaternion increment;

	
	void Start ()
    {
        spinSpeed = 0.3f;
        platform = GetComponent<Rigidbody>();
        
        dock = GetComponent<Transform>();
        
	}
	
	// Update is called once per frame
	void FixedUpdate()
    {
        /*
        if (dock.rotation.y < 0.99f)
        {
            increment = new Quaternion(dock.rotation.x, dock.rotation.y+spinIncrement, dock.rotation.z, dock.rotation.w);
            dock.rotation = increment;
        }
        else
        {
            increment = new Quaternion(dock.rotation.x, -0.999f, dock.rotation.z, dock.rotation.w);
            dock.rotation = increment;
        }
        */
        curSpeed = Vector3.Magnitude(platform.angularVelocity);

        if(curSpeed < spinSpeed)
        {
            platform.AddTorque(new Vector3(0f, 1000f, 0f));
        }
        else
        {
            brakeSpeed = curSpeed - spinSpeed;

            Vector3 normalizedAngVel = platform.angularVelocity.normalized;
            Vector3 brakeAngVel = normalizedAngVel * brakeSpeed;


            platform.AddTorque(-brakeAngVel);
        }
        
	}

    void rotatePlatform(float spinIn)
    {
        
    }
}
