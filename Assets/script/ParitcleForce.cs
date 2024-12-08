using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParitcleForce: MonoBehaviour
{
    public Vector2 forceVector;
    public float force;
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(forceVector*force);
        }
    }
}
