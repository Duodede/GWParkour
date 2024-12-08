using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEffect : MonoBehaviour
{
    public GameObject effect;
    public bool trigger;
    public float height;
    public float strength;
    ParticleSystem particle;
    private void Start()
    {
        particle = effect.GetComponent<ParticleSystem>();
        particle.Stop();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rg)&&!trigger)
        {
            //Debug.Log(collision.gameObject.tag);
            if(rg.velocity.magnitude != 0)
            {
                effect.active = true;
                effect.transform.position = new Vector3(rg.transform.position.x, this.transform.position.y + height, 0);
                particle.emissionRate = strength* (rg.velocity.magnitude/10f);
            }
            else
            {
                particle.Stop();
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rg) && !trigger)
        {
            particle.Stop();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rg) && trigger)
        {
            //Debug.Log(collision.gameObject.tag);
            if (rg.velocity.magnitude != 0)
            {
                effect.active = true;
                effect.transform.position = new Vector3(rg.transform.position.x, this.transform.position.y + height, 0);
                particle.emissionRate = strength * (rg.velocity.magnitude / 10f);
            }
            else
            {
                particle.Stop();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rg) && trigger)
        {
            particle.Stop();
        }
    }
}
