using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSpawner : MonoBehaviour
{
    [SerializeField] private float cooldownTime = 0.5f;
    [SerializeField] private float defaultCooldownTime;
    [SerializeField] private bool justSpawned = true;

    public GameObject SpawnObject;

    private void OnTriggerExit(Collider other)
    {
        print("exit");
        if (justSpawned)
        {
            justSpawned = false;
        }
    }

    private void Start()
    {
        defaultCooldownTime = cooldownTime;
    }

    void Update()
    {
        if (!justSpawned)
        {
            cooldownTime -= Time.deltaTime;
            if (cooldownTime <= 0)
            {
                var newObj = Instantiate(SpawnObject, this.transform.position, this.transform.rotation);
                justSpawned = true;
                cooldownTime = defaultCooldownTime;
            }
        }
    }
}
