using System.Collections;
using System.Collections.Generic;
using UnityEngine;
   

public class HunterMk2 : MonoBehaviour
{
    // Just initializing some variables first! 
    public float passiveSpeed = 4;
    public float activeSpeed = 7;
    
    //Detection stuff:
    public float detectionRad = 5;
    public float detectionAng = 90;
    public int detectionInt = 1;
    public int numOfDetectionRays = 90;

    Vector3 directionVec;
    float detectionDistance;
    Rigidbody rigidBody;


    // Call the stuff we need before the game starts

    void Start(){

        rigidBody = GetComponent<Rigidbody> ();
    }

    // // Update is called once per frame
    void Update(){
        RaycastHit rayInfo;
        // send out a ray into the world just going forward endlessly.
        // At some point I would love to modify this so that it sends out multiple rays
        // These would then be used to look for object and for the player. 
        for (int i = 0; i*detectionInt <= detectionAng; i++){
            
            // For every degree in a set angle cast a ray and return the value to rayInfo.
            float localAngle = detectionInt * i;
            Quaternion rayDirection = Quaternion.Euler(0f,localAngle, 0f);
            Ray hunterRay = new Ray(transform.position, rayDirection*transform.forward);

            if(Physics.Raycast(hunterRay , out rayInfo)) {
                // sets a distance to check to and then sees if that's further than 
                // the detection radius. 
                detectionDistance = Mathf.Min(rayInfo.distance,detectionRad);
                Vector3 endPoint = hunterRay.GetPoint(detectionDistance);
                if (rayInfo.distance <= detectionRad){
                
                // Colors rays based on what the ray hits.
                    if (rayInfo.collider.gameObject.tag == "Terrain"){
                        Debug.DrawLine(transform.position, rayInfo.point, Color.cyan);}
                    else if (rayInfo.collider.gameObject.tag == "Player"){
                        Debug.DrawLine(transform.position, rayInfo.point, Color.red);}
                } else { Debug.DrawLine(transform.position,endPoint,Color.white);}
            
            }
            //Write something so that if a player is detected, the hunter moves to the last known place
            //the player was detected, which can be updated or it could also try and intercept the player
            // based on the velocity of the player at the moment that it sees the player. 
        }
    }
    void FixedUpdate(){
        rigidBody.position = transform.position;

        //Setup collisions so that if the hunter touches the player 
        //destroy the player object. 

    }
    void OnCollisionEnter (Collision collision){
        print(collision.gameObject.name);

        if (collision.gameObject.tag == "Player") {
            Destroy(collision.gameObject);

        if (collision.gameObject.tag == "Terrain"){
            Ray newDir = new Ray(transform.position, transform.TransformDirection(Vector3.back));
            RaycastHit collisionHit;

            if (Physics.Raycast(newDir, out collisionHit)){
                directionVec = -(collisionHit.normal);
                
            }
        }
        }
    }



}


