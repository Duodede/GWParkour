using UnityEngine;

public class RigidbodyPartDestroyer : MonoBehaviour
{
    void FixedUpdate()
    {
        if(transform.position.y<-1000)
        {
            Destroy(gameObject);
        }
    }
}
