using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    //First and third person cameras
    public GameObject thirdViewCam;
    public GameObject firstViewCam;

    //Listens for changes ingame
    AudioListener thirdViewAudioLis;
    AudioListener firstViewAudioLis;

    // Start is called before the first frame update
    void Start()
    {
        //Setup Camera listerners
        thirdViewAudioLis = thirdViewCam.GetComponent<AudioListener>();
        firstViewAudioLis = firstViewCam.GetComponent<AudioListener>();

        //Camera positioning
        CameraPositionChange(PlayerPrefs.GetInt("Camera Position"));
    }

    void Update()
    {
        Switch();
    }

    void CameraPositionChange(int position)
    {
        //Reset the counter so that counter can only be either 1 or 0 for simplicity
        if (position > 1)
        {
            position = 0;
        }

        //Set camera position database
        PlayerPrefs.SetInt("CameraPosition", position);

        //Set camera position 1 for third person perspective
        if (position == 0)
        {
            thirdViewCam.SetActive(true);
            thirdViewAudioLis.enabled = true;

            firstViewAudioLis.enabled = false;
            firstViewCam.SetActive(false);
        }

        //Set camera position 2 for first person perspective
        if (position == 1)
        {
            firstViewCam.SetActive(true);
            firstViewAudioLis.enabled = true;

            thirdViewAudioLis.enabled = false;
            thirdViewCam.SetActive(false);
        }
    }

    void CameraCounter()
    {
        int counter = PlayerPrefs.GetInt("CameraPosition");
        counter++;
        CameraPositionChange(counter);
    }

    void Switch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CameraCounter();
        }
    }

    //Function to change the camera positioning
    void CameraPosition()
    {
        CameraCounter();
    }
}
