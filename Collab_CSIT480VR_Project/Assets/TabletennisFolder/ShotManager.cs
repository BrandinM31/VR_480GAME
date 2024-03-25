using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This SCRIPT (shotManager) WILL DECIDE WHAT KIND OF SHOT THE UER WANTS TO DO IN GAME based on the input given e.g(flatshot, topspin)

[System.Serializable] // Serialization is the process of converting an object into a format that can be stored or transmitted and later reconstructed back into the original object.
public class Shot
{
    public float upForce;
    public float hitforce; //Can Maybe be later used to sync woth velocty of the users's swing
}

public class ShotManager : MonoBehaviour
{
    public Shot topSpin;
    public Shot flat;
    //public Shot drop; // type of shot that a user may be able to use

    //Types of serves that can happen
    public Shot flatServe;
    public Shot kickServe;

}
