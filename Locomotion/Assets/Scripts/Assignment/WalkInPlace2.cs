using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkInPlace2 : MonoBehaviour {
    public GameObject controller;
    public GameObject head;

    //Your speed
    public float speed = 1;

    //How much your head has to move to walk, jog, or run
    private const float walkingThreshhold = .00028f;
    private const float joggingThreshhold = .0035f;
    private const float runningThreshhold = .006f;

    //Jogging and running multiplies your speed
    private const float walkingMultiplier = 1.0f;
    private const float joggingMultiplier = 1.8f;
    private const float runningMultiplier = 2.7f;

    //Used to keep track of where your head was in the last frame and current frame
    private float prevY;
    private float currentY;

    //Stuff used for jumping
    private bool isJumping;
    private float initialVelocity;
    private float currentVelocity;
    private const float gravity = 9.81f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //This just keeps track of the previous head height and current head height.
        prevY = currentY;
        currentY = head.transform.localPosition.y;

        //This is the different between head heights between frames
        var deltaY = Mathf.Abs(currentY - prevY);

        //This is the part where you actually move.
        {
            //You walk in the direction of your HMD (controller)
            float angle = head.transform.rotation.eulerAngles.y / 180 * Mathf.PI;

            //If your head moves past the threshhold, your character moves at a certain speed.
            if (deltaY > runningThreshhold) transform.Translate(new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * speed * runningMultiplier * Time.deltaTime, Space.World);//not sure does it need Space.World
            else if (deltaY > joggingThreshhold) transform.Translate(new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * speed * joggingMultiplier * Time.deltaTime, Space.World);
            else if (deltaY > walkingThreshhold) transform.Translate(new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * speed * walkingMultiplier * Time.deltaTime, Space.World);

        }

    }
}
