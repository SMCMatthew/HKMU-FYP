using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class BroomBehaviour : MonoBehaviour
{
    public ColorChanger colorChanger;

    private void OnCollisionEnter(Collision collision)
    {
        //print("Colliding on: " + collision.gameObject.name);
        //colorChanger.ChangeColor(collision.transform.GetChild(0).gameObject);
    }
}
