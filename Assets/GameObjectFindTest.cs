using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectFindTest : MonoBehaviour
{
    public GameObject gameobject;

    // Start is called before the first frame update
    void Start()
    {
        gameobject = GameObject.Find("/WordContainer/GridParent/GridPlaceHolder/GridBlock (" + "1" + ")/Snap Interactable " + "1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
