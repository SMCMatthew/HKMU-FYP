using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class BroomBehaviour : MonoBehaviour
{
    public ColorChanger colorChanger;

    private void Start()
    {
        colorChanger = GameObject.Find("ColorChanger").GetComponent<ColorChanger>();
    }



    private void OnTriggerEnter(Collider collision)
    {
        //print("Colliding on: " + collision.gameObject.name);
        if (collision.gameObject.tag == "Letter" && collision.transform.childCount > 0)
        {
            colorChanger.ChangeColor(collision.transform.GetChild(0).gameObject);
        }
    }
}
