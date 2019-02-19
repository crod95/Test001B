using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonScript : MonoBehaviour
{

    //Private variables for limiting the angles of camera
    private const float YAngleMin = -180.0f;
    private const float YAngleMax = 180.0f;

    //Variables for the camera
    public Transform focusOn;
    public Transform cameraMove;
    private Camera thirdViewCam;
    private float dist = 4.0f;

    //Record the current x and y values of the camera
    private float currentX = 0.0f;
    private float currentY = 0.0f;

    //Control how sensitive the camea moves in the x and y axis
    private float sensitivityX = 10.0f;
    private float sensitivityY = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        //Focus on the main camera
        cameraMove = transform;
        thirdViewCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        currentX += Input.GetAxis("Mouse X");
        currentY += Input.GetAxis("Mouse Y");

        //Lock how far the camera can move on the y-axis
        currentX = Mathf.Clamp(currentX, YAngleMin, YAngleMax);

    }

    private void LateUpdate()
    {
        Vector3 direct = new Vector3(dist, 0, 0);
        Quaternion rotate = Quaternion.Euler(0, currentX * 2, currentY * 2);
        //Put the camera on top of the player and rotate depending on the player's movements
        cameraMove.position = focusOn.position + rotate * direct;
        cameraMove.LookAt(focusOn.position);
    }
}
