using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WordContainerPrintTrigger : MonoBehaviour
{
    public Animator wordContainerAnimator;

    TextMeshPro TextMeshPro;

    public bool isPrinted = false;

    private void Start()
    {
        wordContainerAnimator = GameObject.Find("WordContainer").GetComponent<Animator>();
        wordContainerAnimator.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "WordContainer" && !isPrinted)
        {
            wordContainerAnimator.enabled = true;
            print("hit print trigger");
            wordContainerAnimator.Play("Printing");
            isPrinted = true;
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
