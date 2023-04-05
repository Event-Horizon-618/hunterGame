using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{ 
    public static List<RaycastHit> UpdateList()
{
    HunterMk2 hunterMk2 = GameObject.FindObjectOfType<HunterMk2>();
    if (hunterMk2 == null){
        Debug.LogError("Could not find HunterMk2 object");
        return null;
    }

    List<RaycastHit> rayCastInformation = hunterMk2.rayCastInformation;

    // Do something with rayCastInformation...

    return rayCastInformation;


    void Update(){

        //I want the computer to be able to see the points that are obstructed and avoid those things
        // Maybe make it so that it will try to avoid terrain keeping a distance of like 2 units
        // and persuing the player and constantly moving forward. 

        //later I can add things like tracking where the player is going and trying to intercept and such. 
        int count = rayCastInformation.Count;

        for (int i = 0; i < (count); i++){
            print ("suck my ween");
        }
    }
}
}
