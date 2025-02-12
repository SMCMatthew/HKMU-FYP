using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class GameManager : MonoBehaviour
{
    public CurrentObjectTracker[] gameObjects; // Array to hold references to the GameObjects

    public GameObject[] answer;

    public bool isAllCorrect;

    void Start()
    {
        
    }

    private void Update()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].requireObject = answer[i].gameObject;
        }
    }

    void CheckAnswer()
    {

    }
}
