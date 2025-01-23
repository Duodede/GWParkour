using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        gameManager.startPos = transform;
    }
    void OnDistroy()
    {
        gameManager.startPos = null;
    }

}
