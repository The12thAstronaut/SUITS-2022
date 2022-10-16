using System.Collections;
using System.Collections.Generic;
using Photon.Voice.Unity;
using UnityEngine;

public class muteVoice : MonoBehaviour
{

    public GameObject voiceObject;
    public Recorder userRecorder;
    public bool muteState;

    // Start is called before the first frame update
    void Start()
    {
        //Un mute voice upon start
        muteState = false;

        // Get the parent gameobject
        voiceObject = this.gameObject;
        // Get the recorder component
        userRecorder = voiceObject.GetComponent("Recorder") as Recorder;
        print("The recorer is: ");
        print(userRecorder);
        
    }

    public void Update()
    {
        // If the M key is pressed, toggle the mute state
        if (Input.GetKeyUp(KeyCode.M))
        {
            toggleMute();
            print("Mute state is now:");
            print(muteState);
        }

    }

    public void toggleMute()
    {
        muteState = !muteState;

        //Set the transmit enabled state to the opposite of the mute state
        // If muting is enabled, disable transmission, and vice versa
        userRecorder.TransmitEnabled = !muteState;
        print("Transmit state in the recorder is now:");
        print(userRecorder.TransmitEnabled);
    }

}