using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEngine.XR.Interaction.Toolkit.XRGazeAssistance;

public class CurrentObjectTracker : MonoBehaviour
{

    public SnapInteractable snapInteractable;

    // Start is called before the first frame update
    void Start()
    {
        snapInteractable = this.GetComponent<SnapInteractable>();
    }

    // Update is called once per frame
    void Update()
    {
        var interactorData = snapInteractable.SelectingInteractorViews.First().Data as MonoBehaviour;
        print("AAA: " + interactorData.gameObject.name);
        //print("BBB: " + snapInteractable.SelectingInteractorViews.First().Data as MonoBehaviour.gameObject);
        //foreach (IInteractorView interactorView in snapInteractable.SelectingInteractorViews)
        //{
        //    print(interactorView.Identifier.ToString() + "AAA");
        //}
    }
}
