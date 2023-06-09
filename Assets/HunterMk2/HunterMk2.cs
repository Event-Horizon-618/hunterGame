using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class HunterMk2 : MonoBehaviour
{
    // Just initializing some variables first! 
    public float passiveSpeed = 4;
    public float activeSpeed = 7;

    //Detection stuff:
    public float detectionRad = 15;
    public float detectionAng = 80;
    public int detectionInt = 2;
    public int numOfDetectionRays = 90;
    float detectionDistance;
    
    Vector3 directionVec;
    Vector3 hunterVelocity;
    
    Rigidbody rigidBody;
    public List<RaycastHit> hunterLookList = new List<RaycastHit>();

    // Call the stuff we need before the game starts


    void Start(){
        rigidBody = GetComponent<Rigidbody> ();
    }

// Update method!!
    private void Update() {
        hunterLookRoutine();
    }

    public List<RaycastHit> hunterLookRoutine(){
        RaycastHit rayInfo;
        // send out a ray into the world just going forward endlessly.
        // At some point I would love to modify this so that it sends out multiple rays
        // These would then be used to look for object and for the player. 
        for (int i = 0; i * detectionInt <= detectionAng; i++){
            
            // For every degree in a set angle cast a ray and return the value to rayInfo.
            float localAngle = Mathf.Pow(-1,i)*(detectionInt * i);
            Quaternion rayDirection = Quaternion.Euler(0f, localAngle, 0f);
            Ray hunterRay = new Ray(transform.position, rayDirection*transform.forward);


            if(Physics.Raycast(hunterRay , out rayInfo)) {
                // sets a distance to check to and then sees if that's further than 
                // the detection radius. 
                detectionDistance = Mathf.Min(rayInfo.distance,detectionRad);
                Vector3 endPoint = hunterRay.GetPoint(detectionDistance);

                if (rayInfo.distance <= detectionRad){
 
                    if (rayInfo.collider.gameObject.tag == "Terrain" && (rayInfo.distance < 3)) {
                        Debug.DrawLine(transform.position, rayInfo.point, Color.cyan);

                        Vector3 objectNormal = rayInfo.normal;
                        Vector3 reflectionDir = Vector3.Reflect(hunterRay.direction ,objectNormal);
                        // Ray reflectedRay = new Ray(rayInfo.point, reflectionDir);
                        Debug.DrawLine(rayInfo.point, rayInfo.point + reflectionDir, Color.green);

                        //TerrainAvoidance();
                    
                    } else if (rayInfo.collider.gameObject.tag == "Player"){
                        Debug.DrawLine(transform.position, rayInfo.point , Color.red);
                        activeMovement();
                        }

                } else { 
                    Debug.DrawLine(transform.position,endPoint,Color.white);
                    passiveMovement();
                
                }
            }
            // This idea has been scrapped so that I can return the list of values so that it can be used in another 

            //Write something so that if a player is detected, the hunter moves to the last known place
            //the player was detected, which can be updated or it could also try and intercept the player
            // based on the velocity of the player at the moment that it sees the player. 

        }


        //This returns the information from the list at the end of ever for loop. 
        return(hunterLookList);
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
    void passiveMovement(){
        // Pick a random movement direction and move the hunter in that direction.
        Quaternion wanderAngle = Quaternion.Euler(0f,0f,Random.Range(-80,80)); 
        Vector3 directionVec = transform.rotation * wanderAngle * Vector3.forward;
        hunterVelocity = directionVec * passiveSpeed;
        rigidBody.position += hunterVelocity * Time.fixedDeltaTime;
    }

    void activeMovement(){
        
    }
}