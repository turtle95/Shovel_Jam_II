using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour {

    public float sensitivity = 0.125f; //sensitivity of mouse when moving the camera
    public float rangeY = 100f; //movement range for y mouse look
    public float mouseX = 0;
    public float mouseY = 0; //values for mouse input



    bool inputDecided = false;

    public Transform camDown;
    public Transform mainCam;

   
    bool controller = false;



    void Start()
    {
        transform.rotation = camDown.rotation;
        Cursor.lockState = CursorLockMode.Locked;

    }

    private void Update()
    {
        if (!inputDecided)
            TestForController();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (controller)
            sensitivity = 4;
        else
            sensitivity = 1;



        //Takes input from the mouse and gives it a speed

        mouseX += Input.GetAxis("Mouse X") * sensitivity;// * camDown.rotation;
        mouseY += Input.GetAxis("Mouse Y") * sensitivity;// * camDown.rotation;

        mouseY = Mathf.Clamp(mouseY, -rangeY, rangeY);
        //gives the y camera movement a maximum/minimum movement range

        transform.rotation = camDown.rotation * Quaternion.Euler(-mouseY, mouseX, 0);

        float angle = transform.localEulerAngles.x;
        angle = (angle > 180) ? angle - 360 : angle;

       

    }


    //Checks if a keyboard/mouse button is pressed or a controller button is pressed and sets the sensitivity based on that
    void TestForController()
    {
        if (Input.GetKey(KeyCode.Joystick1Button0) ||
                     Input.GetKey(KeyCode.Joystick1Button1) ||
                     Input.GetKey(KeyCode.Joystick1Button2) ||
                     Input.GetKey(KeyCode.Joystick1Button3) ||
                     Input.GetKey(KeyCode.Joystick1Button4) ||
                     Input.GetKey(KeyCode.Joystick1Button5) ||
                     Input.GetKey(KeyCode.Joystick1Button6) ||
                     Input.GetKey(KeyCode.Joystick1Button7) ||
                     Input.GetKey(KeyCode.Joystick1Button8) ||
                     Input.GetKey(KeyCode.Joystick1Button9) ||
                     Input.GetKey(KeyCode.Joystick1Button10) ||
                     Input.GetKey(KeyCode.Joystick1Button11) ||
                     Input.GetKey(KeyCode.Joystick1Button12) ||
                     Input.GetKey(KeyCode.Joystick1Button13) ||
                     Input.GetKey(KeyCode.Joystick1Button14) ||
                     Input.GetKey(KeyCode.Joystick1Button15) ||
                     Input.GetKey(KeyCode.Joystick1Button16) ||
                     Input.GetKey(KeyCode.Joystick1Button17) ||
                     Input.GetKey(KeyCode.Joystick1Button18) ||
                     Input.GetKey(KeyCode.Joystick1Button19))
        {
            controller = true;
            inputDecided = true;
            Debug.Log("Controller Mode");
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) ||
            Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow)
            || Input.GetKey(KeyCode.LeftArrow))
        {
            controller = false;
            inputDecided = true;
        }

    }
}
