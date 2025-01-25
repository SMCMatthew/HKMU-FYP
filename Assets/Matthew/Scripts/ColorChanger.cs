using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    public Transform grabObject; // Objects that the player can grab
    public float colorChangeSpeed = 0.1f; // Color change speed
    public float raycastDistance = 100f; // Raycast distance
    public Vector3 returnPosition; // The position grabObject returns to after releasing the mouse
    public float returnSpeed = 5f; // Return speed
    public Transform rechargePosition; // Energy replenishment location
    public float rechargeSpeed = 20f; // The speed of replenishing energy
    public Slider energySlider; // UI slider showing energy

    private bool isGrabbing = false;
    private Dictionary<GameObject, float> letterColorProgress = new Dictionary<GameObject, float>(); // Color transfer progress of letters
    private float energy = 100f; // The initial energy is 100%
    private float maxEnergy = 100f; // Maximum energy
    private float minEnergy = 0f; // Minimum energy

    public void ChangeColor(GameObject hitObject)
    {
        print("changing");

        if (!letterColorProgress.ContainsKey(hitObject))
        {
            letterColorProgress[hitObject] = 0f; // Initialize color transfer progress
        }

        float currentProgress = letterColorProgress[hitObject];
        if (currentProgress < 100f && energy > minEnergy)
        {
            Debug.Log("Hit letter: " + hitObject.name); // Debug message
            currentProgress += colorChangeSpeed * Time.deltaTime * 5f; // Increase color transfer progress
            letterColorProgress[hitObject] = Mathf.Min(currentProgress, 100f); // Make sure the progress does not exceed 100%
            energy = Mathf.Max(minEnergy, energy - 0.5f); // Each stroke reduces energy by 1%

            // Update letter color
            Renderer renderer = hitObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                // Get the original color of the material
                Color startColor = renderer.material.color;

                // Define the target color
                Color endColor = new Color32(0, 0, 0, 255);

                // Interpolate between startColor and endColor
                Color color = Color.Lerp(startColor, endColor, currentProgress / 100f);

                // Update the material color
                renderer.material.color = color;
            }

            // Ensure hitObject is not null
            //if (hitObject == null)
            //{
            //    Debug.LogError("hitObject is null. Ensure it is properly assigned.");
            //    return;
            //}

            //// Try to get the Renderer
            //Renderer renderer = hitObject.GetComponent<Renderer>();
            //if (renderer != null)
            //{
            //    Debug.Log("Renderer found! Material color is: " + renderer.material.color);

            //    // Get the original color of the material
            //    Color startColor = renderer.material.color;

            //    // Define the target color
            //    Color endColor = new Color32(0, 0, 0, 255);

            //    // Interpolate between startColor and endColor
            //    Color color = Color.Lerp(startColor, endColor, currentProgress / 100f);

            //    // Update the material color
            //    renderer.material.color = color;

            //    Debug.Log("Updated material color to: " + renderer.material.color);
            //}
            //else
            //{
            //    Debug.LogError("Renderer component not found on the hitObject.");
            //}
        }

        void Update()
        {
            // Update UI slider
            //energySlider.value = energy / maxEnergy;

            // Check if the player clicked on the object
            //if (Input.GetMouseButtonDown(0))
            //{
            //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //    RaycastHit hit;
            //    if (Physics.Raycast(ray, out hit, raycastDistance))
            //    {
            //        if (hit.collider.transform == grabObject)
            //        {
            //            isGrabbing = true;
            //        }
            //    }
            //}

            //if (Input.GetMouseButtonUp(0))
            //{
            //    isGrabbing = false;
            //}

            // If the player is grabbing an object, make the object follow the mouse movement
            //if (isGrabbing)
            //{
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, Vector3.zero); // Define a horizontal plane
            float distance;
            if (plane.Raycast(ray, out distance))
            {
                Vector3 point = ray.GetPoint(distance);
                grabObject.position = point;
            }

            // Check if the raycast hits an object with the specified tag
            RaycastHit hit;
            Vector3 raycastDirection = Vector3.forward; // Towards the direction of the letters
            Debug.DrawRay(grabObject.position, raycastDirection * raycastDistance, Color.red); // Draw visual lines for Raycast
            if (Physics.Raycast(grabObject.position, raycastDirection, out hit, raycastDistance))
            {
                GameObject hitObject = hit.collider.gameObject;
                Debug.Log("Raycast hit: " + hitObject.name); // Debug message

                if (hitObject.CompareTag("letters"))
                {
                    if (!letterColorProgress.ContainsKey(hitObject))
                    {
                        letterColorProgress[hitObject] = 0f; // Initialize color transfer progress
                    }

                    float currentProgress = letterColorProgress[hitObject];
                    if (currentProgress < 100f && energy > minEnergy)
                    {
                        Debug.Log("Hit letter: " + hitObject.name); // Debug message
                        currentProgress += colorChangeSpeed * Time.deltaTime * 5f; // Increase color transfer progress
                        letterColorProgress[hitObject] = Mathf.Min(currentProgress, 100f); // Make sure the progress does not exceed 100%
                        energy = Mathf.Max(minEnergy, energy - 0.5f); // Each stroke reduces energy by 1%

                        // Update letter color
                        Renderer renderer = hitObject.GetComponent<Renderer>();
                        if (renderer != null)
                        {
                            Color startColor = new Color32(229, 181, 223, 255);
                            Color endColor = new Color32(0, 0, 0, 255);
                            Color color = Color.Lerp(renderer.material.color, endColor, currentProgress / 100f); // Change from startColor to endColor based on progress
                            renderer.material.color = color;
                        }
                    }
                    else
                    {
                        Debug.Log("Hit object is not in selected Letters or energy is too low or letter is fully colored: " + hitObject.name); // Debug message
                    }
                }
            }
            else
            {
                Debug.Log("Raycast did not hit any object."); // Debug message
            }
            //}
            //else
            //{
            //// When the player releases the mouse, return the grabObject to the specified position
            //grabObject.position = Vector3.Lerp(grabObject.position, returnPosition, returnSpeed * Time.deltaTime);
            //}

            if (Vector3.Distance(grabObject.position, rechargePosition.position) < 0.5f) // Increase distance threshold
            {
                Debug.Log("Recharging energy..."); // Debug message
                energy = Mathf.Min(maxEnergy, energy + rechargeSpeed * Time.deltaTime);
            }
        }
    }
}