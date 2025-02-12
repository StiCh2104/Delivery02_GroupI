using UnityEngine;
using UnityEngine.UI;

public class DistanceCounter : MonoBehaviour
{
    public Text distanceText;  // Add DistanceHUD
    private Vector3 lastPosition;
    private float totalDistance;

    void Start()
    {
        if (distanceText == null)
        {
            Debug.LogError("El componente Text no está asignado.");
        }

        lastPosition = transform.position;
        totalDistance = 0f;
    }

    void Update()
    {
        float distanceThisFrame = Vector3.Distance(lastPosition, transform.position);

        totalDistance += distanceThisFrame;

        distanceText.text = "Distance: " + Mathf.Floor(totalDistance) + " units";

        lastPosition = transform.position;
    }
}
