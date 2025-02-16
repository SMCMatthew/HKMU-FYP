using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public CurrentObjectTracker[] gameObjects; // Array to hold references to the GameObjects

    public GameObject[] answer;



    public bool isAllCorrect;

    void Start()
    {
        // Set the array automaticly
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i] = GameObject.Find("/WordContainer/GridParent/GridPlaceHolder/GridBlock (" + (i + 1) + ")/Snap Interactable " + (i + 1)).GetComponent<CurrentObjectTracker>();
        }
    }

    private void Update()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].requireObject = answer[i].gameObject;
        }
    }

    public void CheckAnswer()
    {
        print("check");

        isAllCorrect = true;

        for (int i = 0; i < gameObjects.Length; i++)
        {
            bool answer = gameObjects[i].isCorrect;
            if (!answer)
            {
                gameObjects[i].isWrongAnswer();
                isAllCorrect = false;
            }
        }
    }
}
