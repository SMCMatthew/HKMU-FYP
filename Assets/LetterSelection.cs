using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterSelection : MonoBehaviour
{
    public GameObject[] letterModels; // Letter models
    public Transform letterContainer; // Container for arranging selected letters
    public int lettersPerRow = 8; // Maximum number of letters per line
    public int maxLetters = 40; // Maximum number of letters to choose
    public float verticalSpacing = 1.0f; // Vertical spacing
    public float horizontalSpacing = 1.5f; // horizontal spacing
    public int lettersPerColumn = 8; // Maximum number of letters per column
    private List<GameObject> selectedLetters = new List<GameObject>();
    public Vector3 letterScale = new Vector3(1.0f, 1.0f, 1.0f); // letter scaling
    public ColorChanger colorChanger; // Quoting the ColorChanger script

    void Start()
    {
        // Initialize a selected list of letters
        //selectedLetters = new List<GameObject>();
       // colorChanger.SetSelectedLetters(selectedLetters);
    }

    void Update()
    {
        // Check if the player clicked on a letter
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.collider.gameObject;
                Debug.Log("Raycast hit: " + hitObject.name); // Debug message
                if (hitObject.CompareTag("Letter"))
                {
                    if (!selectedLetters.Contains(hitObject))
                    {
                        AddLetter(hitObject);
                        Debug.Log("Selected letter: " + hitObject.name); // Debug message
                       //colorChanger.SetSelectedLetters(selectedLetters);
                    }
                }
            }
        }

        // Check if the player clicked the right mouse button to remove the last letter
        if (Input.GetMouseButtonDown(1))
        {
            RemoveLastLetter();
        }
    }

    void AddLetter(GameObject letter)
    {
        if (selectedLetters.Count >= maxLetters)
        {
            Debug.Log("Maximum number of letters reached."); // Debug message
            return;
        }

        GameObject newLetter = Instantiate(letter, letterContainer);
        selectedLetters.Add(newLetter);

        // Calculate columns and rows
        int column = (selectedLetters.Count - 1) / lettersPerColumn;
        int row = (selectedLetters.Count - 1) % lettersPerColumn;

        // Calculate position
        Vector3 position = new Vector3(column * horizontalSpacing, -row * verticalSpacing, 0);

        // Set the position and rotation of letters
        newLetter.transform.localPosition = position;
        newLetter.transform.localRotation = Quaternion.identity;
        newLetter.transform.localScale = letterScale;

        Debug.Log("Added letter: " + newLetter.name + " at position: " + position); // Debug message
    }

    void RemoveLastLetter()
    {
        // Only the last letter can be removed
        if (selectedLetters.Count > 0)
        {
            GameObject lastLetter = selectedLetters[selectedLetters.Count - 1];
            selectedLetters.RemoveAt(selectedLetters.Count - 1);

            // Destroy letters immediately
            Destroy(lastLetter);

            Debug.Log("Removed letter: " + lastLetter.name); // Debug message
        }
        else
        {
            Debug.Log("No letters to remove."); // Debug message
        }
    }
}