using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{

    public float playerSpeed = 10;
    Rigidbody myRigidBody;

    Vector3 velocity;
    int coinCount;
    // Start is called before the first frame update
    void Start(){
        myRigidBody = GetComponent<Rigidbody> ();

    }

    // Update is called once per frame
    void Update(){
        
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));
        Vector3 direction = input.normalized;
        velocity = direction * playerSpeed;
    }
    
    // This tells Unity to move the object while being able to detect
    // collisions with the physics engine.
    void FixedUpdate() {
        myRigidBody.position += velocity * Time.fixedDeltaTime;
    }
    
    // This code will tell the game that a coin has been picked up, and will destroy
    // it from the scene as it's (picked up)
    
    // Interesting to note is that the code knows which object is being referenced 
    // just because each instance of a "coin" has a unique value that the computer
    // uses to identify it!

    void OnTriggerEnter(Collider triggerCollider) {
        if (triggerCollider.tag == "Coin"){
            Destroy (triggerCollider.gameObject);
            coinCount++;
            print(coinCount);
        }
    }
}
