using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordContainerPrintTrigger : MonoBehaviour
{
    public Animator wordContainerAnimator;

    private void Start()
    {
        wordContainerAnimator = GameObject.Find("WordContainer").GetComponent<Animator>();
        wordContainerAnimator.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "WordContainer")
        {
            wordContainerAnimator.enabled = true;
            print("hit print trigger");
            wordContainerAnimator.Play("Printing"); 
        }
    }

    private void Update()
    {
       // if (wordContainerAnimator.enabled && !wordContainerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Printing"))
        //{
        //    wordContainerAnimator.enabled = false;
       // }
    }
}
