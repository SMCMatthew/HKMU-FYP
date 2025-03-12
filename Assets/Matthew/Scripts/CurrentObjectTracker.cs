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

    public float duration = 1.5f; // Duration of the lerp
    [SerializeField] private Color initialColor; // Store the initial color
    [SerializeField] private Color targetColor; // Target color with alpha
    public Renderer objectRenderer; // Reference to the object's Renderer
    private bool isLerping = false; // Flag to check if lerping is in progress

    public void isWrongAnswer()
    {
        print("wrong");
        if (!isLerping)
        {
            print("flash");
            StartCoroutine(LerpAlpha(new Color(255, 0, 40)));
        }
    }

    public void MissingInk()
    {
        print("missing ink");
        if (!isLerping)
        {
            print("flash");
            StartCoroutine(LerpAlpha(new Color(10, 255, 0)));
        }
    }

    // Flash the Grid Block when a wrong answer is applied
    private IEnumerator LerpAlpha(Color color)
    {
        isLerping = true;

        // Set the target color to fully opaque
        targetColor = color;
        targetColor.a = 1f; // Set target alpha to 1 (fully opaque)

        // Lerp to fully opaque
        Color startColor = objectRenderer.material.color;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            Color newColor = Color.Lerp(startColor, targetColor, t);
            objectRenderer.material.color = newColor;

            timeElapsed += Time.deltaTime;
            yield return null; // Wait until the next frame
        }

        // Ensure the final color is set to fully opaque
        objectRenderer.material.color = targetColor;

        // Lerp back to the initial color
        timeElapsed = 0f;
        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            Color newColor = Color.Lerp(targetColor, initialColor, t);
            objectRenderer.material.color = newColor;

            timeElapsed += Time.deltaTime;
            yield return null; // Wait until the next frame
        }

        // Ensure the final color is set back to the initial color
        objectRenderer.material.color = initialColor;
        isLerping = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        snapInteractable = this.GetComponent<SnapInteractable>();
        objectRenderer = this.GetComponentInParent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check the cuurent object in the interactable and apply the value to currentObject
        if (snapInteractable.SelectingInteractorViews.Any())
        {
            var interactorData = snapInteractable.SelectingInteractorViews.First().Data as MonoBehaviour;
            //print("AAA: " + interactorData.transform.parent.gameObject.name);
            currentObject = interactorData.transform.parent.gameObject;
        }
        else currentObject = null;

        if (currentObject == null)
        {
            isCorrect = false;
        }


        if (requireObject != null && currentObject != null)
        {
            //print("require: " + requireObject.transform.name + " current: " + currentObject.transform.name + "(Clone)");
            // Change the boolean if the currentObject is same as the requireObject
            if (currentObject.transform.name == requireObject.transform.name || currentObject.transform.name == requireObject.transform.name + "(Clone)")
            {
                isCorrect = true;
                print("correct object");
            }
            else isCorrect = false;
        }
        
        //print("BBB: " + snapInteractable.SelectingInteractorViews.First().Data as MonoBehaviour.gameObject);
        //foreach (IInteractorView interactorView in snapInteractable.SelectingInteractorViews)
        //{
        //    print(interactorView.Identifier.ToString() + "AAA");
        //}
    }
}
