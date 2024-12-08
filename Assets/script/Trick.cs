using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trick : MonoBehaviour
{
    public UnityEvent OnTrigger;
    public UnityEvent OnRecover;
    public bool recover;
    public float recoverTime;
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            OnTrigger.Invoke();
            if(recover)
            {
                StartCoroutine(Recover());
            }
        }
    }
    IEnumerator Recover()
    {
        yield return new WaitForSeconds(recoverTime);
        OnRecover.Invoke();
        StopCoroutine(Recover());
    }
}
