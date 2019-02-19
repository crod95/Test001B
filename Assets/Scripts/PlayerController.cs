using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

//In Unity, the constraints changed so that the y position is frozen and the x and z rotations are frozen
//Change mass and drag as desired depending on how you want the player to move

public class PlayerController : MonoBehaviour
{
    //Collection variables along with text
    private int pickupCount;
    private int pickup2Count;
    public GameObject partEffect;
    public GameObject door01;
    public GameObject door02;
    public GameObject testObject;
    //Attach in Unity inspector
    //public Animation doorAnimate;

    private string moveInputAxis = "Vertical";
    private string turnInputAxis = "Horizontal";
    public float moveSpeed = 10;
    public float rotationSpeed = 360;
    public float doorSpeed = 5;

    //Reference to the rigidbody to use later in implementation
    Rigidbody me;
    public float jumpForce = 100;
    public float distToGround = 0.5f;

    private void Start()
    {
        me = GetComponent<Rigidbody>();
        pickupCount = 0;
        door01 = GameObject.FindGameObjectWithTag("Right Door");
        door02 = GameObject.FindGameObjectWithTag("Left Door");
        testObject = GameObject.FindGameObjectWithTag("Test");
    }

    // Update is called once per frame
    void Update()
    {
        //variables to hold the input for turning and moving
        float moveAxis = Input.GetAxis(moveInputAxis);
        float turnAxis = Input.GetAxis(turnInputAxis);

        //function called whenever the user is trying to move or turn
        ApplyInput(moveAxis, turnAxis);
    }

    private void ApplyInput(float input1, float input2)
    {
        //input1 represents input for moving while input2 represents input for turning
        movePlayer(-input1);
        turnPlayer(input2);
    }

    //Function that handles moving the player's character around
    private void movePlayer(float moveInput)
    {
        me.AddForce(transform.forward * moveInput * moveSpeed, ForceMode.Force);
        // Debug.Log("Move Pressed");
    }

    //Function that handles turning the player's character
    private void turnPlayer(float turnInput)
    {
        transform.Rotate(0, turnInput * rotationSpeed * Time.deltaTime, 0);
        //Debug.Log("Turn Pressed");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            pickupCount++;
            Instantiate(partEffect, other.transform.position, other.transform.rotation);
            other.gameObject.SetActive(false);
            //Debug.Log(pickupCount);
            //**Note**
            //There have been issues where the door works for the first few times but then later fails in certain tests
            //Check if all of the orbs have been collected
            if (pickupCount == 6)
            {
                //doorAnimate = GetComponent<Animation>();
                //doorAnimate.Play("DoorAnimation");
                //doorAnimate["DoorAnimation"].time = 20.0F;
                Debug.Log("Pickup count reached");
                //Debug.Log("X" + door01.transform.position.x);
                //Debug.Log("Y" + door01.transform.position.y);
                //Debug.Log("Z" + door01.transform.position.z);
                for (int a = 0; a < 3; a++)
                {
                    door01.transform.Translate(Vector3.back);
                    Instantiate(partEffect, door01.transform.position, door01.transform.rotation);
                    //Debug.Log("Moving X: " + door01.transform.position.x);
                }
            }
        }
        if (other.gameObject.CompareTag("Pickup2"))
        {
            pickup2Count++;
            Instantiate(partEffect, other.transform.position, other.transform.rotation);
            other.gameObject.SetActive(false);
            if (pickup2Count == 4)
            {
                for (int b = 0; b < 3; b++)
                {
                    door02.transform.Translate(Vector3.forward);
                    Instantiate(partEffect, door02.transform.position, door02.transform.rotation);
                    //Debug.Log("Moving X: " + door02.transform.position.x);
                }
            }
        }
    }
}