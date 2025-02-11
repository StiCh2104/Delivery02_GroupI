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

        // Inicializamos la posición del jugador al inicio del juego.
        lastPosition = transform.position;
        totalDistance = 0f;
    }

    void Update()
    {
        // Calculamos la distancia entre la posición anterior y la actual.
        float distanceThisFrame = Vector3.Distance(lastPosition, transform.position);

        // Sumamos la distancia total recorrida.
        totalDistance += distanceThisFrame;

        // Actualizamos el texto de la UI con la distancia recorrida.
        distanceText.text = "Distance: " + Mathf.Floor(totalDistance) + " units";

        // Actualizamos la última posición para el siguiente frame.
        lastPosition = transform.position;
    }
}
