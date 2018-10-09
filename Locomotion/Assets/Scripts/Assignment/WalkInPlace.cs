using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkInPlace : MonoBehaviour {

    public float stepHMDThreshold = 0.1f;
    public float stepHandThreshold = 0.3f;
    public float shakeHMDThreshold = 0.1f;
    public float shakeHandThreshold = 0.3f;
    //public float stepHMDRunThreshold = 0.1f;
    public float stepHandRunThreshold = 0.8f;
    public float walkSpeed = 1f;
    public float runSpeed = 2.3f;
    public float framesPerStep = 30;

    public SteamVR_Controller.Device controller;
    public SteamVR_Controller.Device controller2;
    public SteamVR_Controller.Device HMD;

    public GameObject vivecontroller;
    public GameObject vivecontroller2;
    public GameObject viveHMD;

    private int stepFrame = 0;
    private Vector3 dirForward;

    // Use this for initialization
    void Start () {
        HMD = SteamVR_Controller.Input((int)SteamVR_TrackedObject.EIndex.Hmd);
        controller = SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost));
        controller2 = SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost));
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        UpdateMovement();
    }

    void UpdateMovement()
    {
        stepFrame++;
        if (stepFrame >= (framesPerStep / 2.0f))
        {
            dirForward = (vivecontroller.transform.forward + vivecontroller2.transform.forward);
            dirForward = viveHMD.transform.forward;
            dirForward = dirForward.normalized;
             
            if (stepDetect()) { 
                if (runDetect())
                {
                    dirForward *= runSpeed;
                    dirForward.y = 0;
                    this.transform.position = this.transform.position + dirForward;
                }
                else
                {
                    dirForward *= walkSpeed;
                    dirForward.y = 0;
                    this.transform.position = this.transform.position + dirForward;
                }
            }
            stepFrame = 0;
            
        }
    }

    private bool stepDetect()
    {
        Vector3 velHMD = HMD.velocity;
        //Debug.Log("HMD Vel X: " + velHMD.x);
        //Debug.Log("HMD Vel Y: " + velHMD.y);
        //Debug.Log("HMD Vel Z: " + velHMD.z);
        Vector3 velHand1 = controller.velocity;
        //Debug.Log("hand 1 Vel X: " + velHand1.x);
        //Debug.Log("hand 1 Vel Y: " + velHand1.y);
        //Debug.Log("hand 1 Vel Z: " + velHand1.z);
        Vector3 velHand2 = controller2.velocity;
        //Debug.Log("hand 2 Vel X: " + velHand2.x);
        //Debug.Log("hand 2 Vel Y: " + velHand2.y);
        //Debug.Log("hand 2 Vel Z: " + velHand2.z);
        float hand1Sign = Mathf.Sign(velHand1.y);
        float hand2Sign = Mathf.Sign(velHand2.y);
        if ((hand1Sign != hand2Sign)
            //&& (velHMD.y > stepHMDThreshold || velHMD.y < -stepHMDThreshold)
            && ((velHand1.y > stepHandThreshold || velHand1.y < -stepHandThreshold)
                || (velHand2.y > stepHandThreshold || velHand2.y < -stepHandThreshold))
            //&& (velHMD.x < shakeHMDThreshold && velHMD.x > -shakeHMDThreshold)
            //&& (velHMD.z < shakeHMDThreshold && velHMD.z > -shakeHMDThreshold)
            //&& (velHand1.x < stepHandThreshold && velHand1.x > -stepHandThreshold)
            //&& (velHand1.z < stepHandThreshold && velHand1.z > -stepHandThreshold)
            //&& (velHand2.x < stepHandThreshold && velHand2.x > -stepHandThreshold)
            //&& (velHand2.z < stepHandThreshold && velHand2.z > -stepHandThreshold)
            )
        {
            //Debug.Log("Step Taken to X: " + player.transform.position.x + " Z: " + player.transform.position.z);
            return true;
        }
        return false;
    }

    private bool runDetect()
    {
        Vector3 velHMD = HMD.velocity;
        Vector3 velHand1 = controller.velocity;
        Vector3 velHand2 = controller2.velocity;
        return (((velHand1.y > stepHandRunThreshold || velHand1.y < -stepHandRunThreshold)
                        || (velHand2.y > stepHandRunThreshold || velHand2.y < -stepHandRunThreshold))
                        );
        //&& (velHMD.y > stepHMDRunThreshold || velHMD.y < -stepHMDRunThreshold));
    }
}
