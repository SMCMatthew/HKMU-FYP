using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class GameManager : MonoBehaviour
{
    public GameObject[] gameObjects; // Array to hold references to the GameObjects
    public GameObject[] requiredGameObjects; // Array to hold the required GameObjects

    void Start()
    {
        // Initialize the GameObjects and Required GameObjects arrays
        gameObjects = new GameObject[1];
        requiredGameObjects = new GameObject[1];

        //// Create and assign GameObjects
        //for (int i = 0; i < gameObjects.Length; i++)
        //{
        //    // Instantiate your GameObjects and assign them
        //    gameObjects[i] = new GameObject("GameObject" + (i + 1)).AddComponent<GameObjectVariable>();
        //    requiredGameObjects[i] = new GameObject("RequiredObject" + (i + 1)); // Create required GameObjects

        //    // Assign the required GameObject to the GameObjectVariable
        //    gameObjects[i].requiredGameObject = requiredGameObjects[i];
        //}
    }

    private void Update()
    {
        print("Array" + EqualityOperator(gameObjects, requiredGameObjects));
    }

    public bool EqualityOperator(GameObject[] firstArray, GameObject[] secondArray)
    {
        return firstArray == secondArray;
    }
}
