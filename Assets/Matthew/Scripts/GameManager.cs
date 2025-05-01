using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Unity.VisualScripting;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public CurrentObjectTracker[] gameObjects; // Array to hold references to the GameObjects

    public int currentQuestion = 1;
    public GameObject paper1;
    public GameObject paper2;
    public GameObject paper3;
    public GameObject answerPaper1;
    public GameObject answerPaper2;
    public GameObject answerPaper3;

    public GameObject[] answer1;
    public bool isAllCorrect1;
    public bool isAllInked1;

    public GameObject[] answer2;
    public bool isAllCorrect2;
    public bool isAllInked2;

    public GameObject[] answer3;
    public bool isAllCorrect3;
    public bool isAllInked3;

    public Animator workTableAnimator;
    public Animator workTable2Animator;
    public Animator tutorialAnimator;
    public Animator museumObjectAnimator;

    void Start()
    {
        // Set the array automaticly
        //for (int i = 0; i < gameObjects.Length; i++)
        //{
        //    gameObjects[i] = GameObject.Find("WordContainer/GridParent/GridPlaceHolder/GridBlock (" + (i + 1) + ")/Snap Interactable " + (i + 1)).GetComponent<CurrentObjectTracker>();
        //}

        //rotateAnimator.speed = 0;
        //rotateAnimator.Play(rotateClip.name, 0, 0f);
    }

    private void Update()
    {
        switch (currentQuestion)
        {
            case 1:
                // Set the answer for question 1
                for (int i = 0; i < gameObjects.Length; i++)
                {
                    gameObjects[i].requireObject = answer1[i].gameObject;
                }
            break;
            case 2:
                // Set the answer for question 1
                for (int i = 0; i < gameObjects.Length; i++)
                {
                    gameObjects[i].requireObject = answer2[i].gameObject;
                }
            break;
            case 3:
                // Set the answer for question 1
                for (int i = 0; i < gameObjects.Length; i++)
                {
                    gameObjects[i].requireObject = answer3[i].gameObject;
                }
            break;
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //workTableAnimator.Play("WorkTableAndObjectSet");
            //workTable2Animator.Play("WorkTable2");
            tutorialAnimator.Play("TutorialObject");
            museumObjectAnimator.Play("Tutorial");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            RotateMuseumObject();
        }

        // For museum object rotation
        if (isRotating)
        {
            rotateTimeCount += Time.deltaTime;
        }

        // Stop the animation once time is reach
        if ((int)Math.Floor(rotateTimeCount) > lastStopSecond)
        {
            print("number: " + (int)Math.Floor(rotateTimeCount));
            rotateAnimator.speed = 0;
            isRotating = false;
        }
        lastStopSecond = (int)Math.Floor(rotateTimeCount);

        // Reset rotate timer
        if (rotateTimeCount > 5f)
        {
            rotateTimeCount = 0f;
            rotateAnimator.Play("RightRotate", 0, 0f);
        }
    }

    public void CheckAnswer()
    {
        print("check");

        isAllCorrect1 = true;

        for (int i = 0; i < gameObjects.Length; i++)
        {
            bool answer = gameObjects[i].isCorrect;
            if (!answer)
            {
                gameObjects[i].isWrongAnswer();
                isAllCorrect1 = false;
            }
        }
    }

    public Animator rotateAnimator; // 動畫控制器
    public float[] keyframeTimes; 
    public int currentKeyframeIndex = 0; 
    public bool isRotating = false;
    public float rotateTimeCount = 0f;
    private int lastStopSecond = 0;

    public void RotateMuseumObject()
    {
        isRotating = true;
        rotateAnimator.speed = 1f;
        rotateAnimator.Play("RightRotate");
    }

    public void ChangeQuestion2()
    {
        paper1.SetActive(false);
        answerPaper1.SetActive(false);
        paper3.SetActive(false);
        answerPaper3.SetActive(false);
        paper2.SetActive(true);
        answerPaper2.SetActive(true);
    }

    public void ChangeQuestion3()
    {
        paper1.SetActive(false);
        answerPaper1.SetActive(false);
        paper2.SetActive(false);
        answerPaper2.SetActive(false);
        paper3.SetActive(true);
        answerPaper3.SetActive(true);
    }

    // Check if the word is filled with ink
    public void CheckIfInked()
    {
        isAllInked1 = true;

        for (int i = 0; i < gameObjects.Length; i++)
        {
            GameObject gameObject = gameObjects[i].GetComponentInParent<GameObject>().GetComponent<CurrentObjectTracker>().currentObject.transform.GetChild(0).gameObject;
            if (gameObject.GetComponent<Renderer>().material.color != new Color32(0, 0, 0, 255))
            {
                gameObjects[i].MissingInk();
                isAllInked1 = false;
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

            currentQuestion++;
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
