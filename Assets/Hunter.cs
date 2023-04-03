using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour{

    public float hunterSpeed = 5;
    Rigidbody hunterRigid;

    // Start is called before the first frame update
    void Start(){
        // When the game starts give this object rigidbody physics
        hunterRigid = GetComponent<Rigidbody> ();
    }

    void FixedUpdate(){
        hunterRigid.position = transform.position;
    }

 // Checks if the player is touched by the hunter and destroys player if true
 // (the program needs to be told that the rigid body is moving)
    void OnCollisionEnter(Collision collision){
        print(collision.gameObject.name);
        if (collision.gameObject.tag == "Player"){
            Destroy (collision.gameObject);
        }
    }

    // Update is called once per frame
    void Update(){

        // Send out a ray and show it
        Ray hunterRay = new Ray (transform.position, transform.forward);
        RaycastHit rayReturn;

        // if the ray touches nothing, it's green, objects are cyan, and players are red

        if (Physics.Raycast (hunterRay, out rayReturn)){
            Debug.DrawLine (hunterRay.origin, rayReturn.point, Color.cyan);
            if (rayReturn.collider.gameObject.tag == "Player") {
                Debug.DrawLine (hunterRay.origin, rayReturn.point, Color.red);
            }
        } else {
            Debug.DrawLine (hunterRay.origin, hunterRay.origin + hunterRay.direction * 25, Color.green);
        }
    }
}