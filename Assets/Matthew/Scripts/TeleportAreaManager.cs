using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;

[System.Serializable]
public class TeleportColliderEntry
{
    public GameObject collider; // The teleport collider object
    public bool isEnabled = true; // Control whether this collider should be active
}

public class TeleportAreaManager : MonoBehaviour
{
    [Header("Tag-based Assignment (Optional)")]
    public string teleportTag = "Teleport"; // Assign a tag to auto-find colliders

    [Header("Teleport Colliders List")]
    public List<TeleportColliderEntry> teleportColliders = new List<TeleportColliderEntry>();

    private void Start()
    {
        // Auto-find teleport colliders if none are assigned
        if (teleportColliders.Count == 0 && !string.IsNullOrEmpty(teleportTag))
        {
            FindTeleportCollidersByTag();
        }

        // Apply the initial enabled/disabled states
        UpdateTeleportColliders();
    }

    // Method to Enable All Teleport Colliders
    public void EnableAllTeleportColliders()
    {
        foreach (var entry in teleportColliders)
        {
            entry.isEnabled = true;
            if (entry.collider != null)
                entry.collider.SetActive(true);
        }
        Debug.Log("All teleport colliders enabled.");
    }

    // Method to Disable All Teleport Colliders
    public void DisableAllTeleportColliders()
    {
        foreach (var entry in teleportColliders)
        {
            entry.isEnabled = false;
            if (entry.collider != null)
                entry.collider.SetActive(false);
        }
        Debug.Log("All teleport colliders disabled.");
    }

    // Toggle a specific teleport collider by index
    public void SetTeleportColliderState(int index, bool state)
    {
        if (index >= 0 && index < teleportColliders.Count && teleportColliders[index].collider != null)
        {
            teleportColliders[index].isEnabled = state;
            teleportColliders[index].collider.SetActive(state);
            Debug.Log($"Teleport collider at index {index} set to: {state}");
        }
    }

    // Update all colliders based on their isEnabled state
    public void UpdateTeleportColliders()
    {
        foreach (var entry in teleportColliders)
        {
            if (entry.collider != null)
                entry.collider.SetActive(entry.isEnabled);
        }
    }

    // Find teleport colliders automatically using a tag
    private void FindTeleportCollidersByTag()
    {
        GameObject[] foundColliders = GameObject.FindGameObjectsWithTag(teleportTag);

        // Sort the found colliders by their name (Alphabetical Order)
        System.Array.Sort(foundColliders, (a, b) => a.name.CompareTo(b.name));

        // Clear existing list and add sorted objects
        teleportColliders.Clear();
        foreach (GameObject obj in foundColliders)
        {
            teleportColliders.Add(new TeleportColliderEntry { collider = obj, isEnabled = obj.activeSelf });
        }

        Debug.Log($"Found {foundColliders.Length} teleport colliders using tag: {teleportTag}, now sorted.");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            print("AAA");
            SetTeleportColliderState(0, false);
        }
    }
}
