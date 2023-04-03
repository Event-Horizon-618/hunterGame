using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    Vector3 playerVelocity;
    Rigidbody rigidBody;
    public Camera viewCamera;
    public float velMag = 6;
    

    // Start is called before the first frame update
    void Start(){
        rigidBody = GetComponent<Rigidbody>();
        viewCamera = Camera.main;

    }

    // Update is called once per frame
    void Update(){

        // Making the character move in the direction of the mouse if the w 
        // key is being pressed. if the w key is being pressed.
        Ray cameraPoint = viewCamera.ScreenPointToRay (Input.mousePosition);
        RaycastHit cameraPointInfo;

        // Checks to see if the ray hits anything, and if it does, assign the 
        // output stuff to cameraPointInfo.
        if (Physics.Raycast(cameraPoint,out cameraPointInfo)){
            print(cameraPointInfo.point);
            print(cameraPointInfo.point.normalized);
        }

        // makes the object look in the direction that the mouse is pointed. 
        Vector3 playerLookDirection = cameraPointInfo.point.normalized;
        transform.LookAt (cameraPointInfo.point);

        float distToMouse = (cameraPointInfo.point - transform.position).magnitude;

        // if the w key is pressed, move the character in the direction the object points
        if ((Input.GetKey("w")) && distToMouse > 1.2){
            Vector3 moveDirection = (cameraPointInfo.point - transform.position).normalized;
            Vector3 playerVelocity = moveDirection * velMag;
            rigidBody.position += playerVelocity * Time.deltaTime;
        }

        //playerVelocity =  * velMag;

}
    void FixedUpdate() {
        rigidBody.position += playerVelocity * Time.fixedDeltaTime;
    }
}
