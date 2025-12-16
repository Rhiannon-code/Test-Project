using UnityEngine;

public class MouseMovement : MonoBehaviour
{

public float mouseSensitivity = 100f;

float xRotation = 0f;
float yRotation = 0f;

public float topClamp = -90f;
public float bottomClamp = 90f;

    void Start()
    {
        Cursor.lockState=CursorLockMode.Locked;
    }


    void Update()
    {
        //Get Mouse Inputs
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; 
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //X Axis Rotation
        xRotation -= mouseY;

        //Clamp Rotation
        xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);

        //Y Axis Rotation
        yRotation += mouseX;

        //Apply Rotations to Transform
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
