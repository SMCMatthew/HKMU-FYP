using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class WordContainerPrintTrigger : MonoBehaviour
{
    public Animator wordContainerAnimator;

    TextMeshPro TextMeshPro;

    public bool isPrinted = false;

    public GameManager gameManager;

    public int thisQuestionNumber = 0;

    private void Start()
    {
        wordContainerAnimator = GameObject.Find("WordContainer").GetComponent<Animator>();
        wordContainerAnimator.enabled = false;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        print("aaa");
        if (other.transform.tag == "WordContainer" && !isPrinted)
        {
            print("hit print trigger");
            switch (thisQuestionNumber)
            {
                case 1:
                    if (gameManager.isAllCorrect1)
                    {
                        wordContainerAnimator.enabled = true;
                        wordContainerAnimator.Play("Printing");
                        isPrinted = true;
                        gameManager.ChangeQuestion2();
                    }
                break;
                case 2:
                    if (gameManager.isAllCorrect2)
                    {
                        wordContainerAnimator.enabled = true;
                        wordContainerAnimator.Play("Printing");
                        isPrinted = true;
                        gameManager.ChangeQuestion3();
                    }
                break;
                case 3:
                    if (gameManager.isAllCorrect3)
                    {
                        wordContainerAnimator.enabled = true;
                        wordContainerAnimator.Play("Printing");
                        isPrinted = true;
                        // Add Ending method
                    }
                break;
            }      
        }
    }

    private void LateUpdate()
    { 
        if (wordContainerAnimator.enabled && !wordContainerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Printing"))
        {
            wordContainerAnimator.enabled = false;
        }
    }
}
