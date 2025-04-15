using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.LoadScene("M_TimeTravelScene",LoadSceneMode.Single);
    }
}
