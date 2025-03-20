using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Unity.VisualScripting;
using TMPro;

public class GameManager : MonoBehaviour
{
    public CurrentObjectTracker[] gameObjects; // Array to hold references to the GameObjects

    public GameObject[] answer;

    public bool isAllCorrect;

    public bool isAllInked;

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

        // Print the word if all Block is correct and every Block is inked
        if (Input.GetKeyDown(KeyCode.P))
        {
            PrintWord();
            //if (isAllCorrect && isAllInked)
            //{
            //    PrintWord();
            //}
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

    // Check if the word is filled with ink
    public void CheckIfInked()
    {
        isAllInked = true;

        for (int i = 0; i < gameObjects.Length; i++)
        {
            GameObject gameObject = gameObjects[i].GetComponentInParent<GameObject>().GetComponent<CurrentObjectTracker>().currentObject.transform.GetChild(0).gameObject;
            if (gameObject.GetComponent<Renderer>().material.color != new Color32(0, 0, 0, 255))
            {
                gameObjects[i].MissingInk();
                isAllInked = false;
            }
        }         
    }

    public GameObject paperPrefab; // Prefab for the paper
    public Transform printPosition; // Position where the paper will appear
    public string wordToPrint;
    public TextMeshProUGUI textMesh;
    public Animator wordContainerAnimator;

    // Method to handle printing
    public void PrintWord()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i].currentObject != null)
            {
                if ((i + 1) % 8 == 0) wordToPrint += "\n";

                // Make empty when the block is "Space"
                if (gameObjects[i].currentObject.name == "Space" || gameObjects[i].currentObject.name == "Space(Clone)" || gameObjects[i].currentObject == null)
                {
                    wordToPrint += " ";
                }
                else if (gameObjects[i].currentObject.name == "Comma" || gameObjects[i].currentObject.name == "Comma(Clone)")
                {
                    wordToPrint += ",";
                }
                else if (gameObjects[i].currentObject.name == "Point" || gameObjects[i].currentObject.name == "Point(Clone)")
                {
                    wordToPrint += ".";
                }
                else if (gameObjects[i].currentObject.name == "Exclamation Mark" || gameObjects[i].currentObject.name == "Exclamation Mark(Clone)")
                {
                    wordToPrint += "!";
                }
                else if (gameObjects[i].currentObject.name == "Question Mark" || gameObjects[i].currentObject.name == "Question Mark(Clone)")
                {
                    wordToPrint += "?";
                }
                else if (gameObjects[i].currentObject.name.Contains("(Clone)"))
                {
                    wordToPrint += gameObjects[i].currentObject.name.Replace("(Clone)", string.Empty);
                }
                else
                {
                    wordToPrint += gameObjects[i].currentObject.name;
                }
            }
        }

        if (!string.IsNullOrEmpty(wordToPrint))
        {
            // Set the printed word on the paper
            /*TextMeshProUGUI */textMesh = paperPrefab.GetComponentInChildren<TextMeshProUGUI>();
            // Alternatively, use a UI Text component if you're using UI
            if (textMesh != null)
            {
                textMesh.text = wordToPrint;
            }
        }
    }
}
