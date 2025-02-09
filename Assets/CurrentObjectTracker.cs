using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEngine.XR.Interaction.Toolkit.XRGazeAssistance;

public class CurrentObjectTracker : MonoBehaviour
{

    public SnapInteractable snapInteractable;

    // Gameplay element
    public GameObject currentObject;
    public GameObject requireObject;
    public bool isCorrect;

    // Start is called before the first frame update
    void Start()
    {
        snapInteractable = this.GetComponent<SnapInteractable>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check the cuurent object in the interactable and apply the value to currentObject
        if (snapInteractable.SelectingInteractorViews.Any())
        {
            var interactorData = snapInteractable.SelectingInteractorViews.First().Data as MonoBehaviour;
            print("AAA: " + interactorData.transform.parent.gameObject.name);
            currentObject = interactorData.transform.parent.gameObject;
        }

        // Change the boolean if the currentObject is same as the requireObject
        if (currentObject == requireObject)
        {
            isCorrect = true;
            print("correct object");
        }
        else isCorrect = false;
        //print("BBB: " + snapInteractable.SelectingInteractorViews.First().Data as MonoBehaviour.gameObject);
        //foreach (IInteractorView interactorView in snapInteractable.SelectingInteractorViews)
        //{
        //    print(interactorView.Identifier.ToString() + "AAA");
        //}
    }
}
