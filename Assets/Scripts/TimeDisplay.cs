using UnityEngine;
using UnityEngine.UI;

public class TimeDisplay : MonoBehaviour
{
    public Text timeText;  // Asigna el componente Text desde el Inspector.
    private float timeInSeconds = 0f;  // Ahora es una variable de instancia.

    void Start()
    {
        if (timeText == null)
        {
            Debug.LogError("El componente Text no est� asignado.");
        }
    }

    void Update()
    {
        timeInSeconds += Time.deltaTime;  // Sumar el tiempo desde la �ltima actualizaci�n.

        // Actualizar el texto de la UI con el tiempo transcurrido.
        timeText.text = "Time: " + Mathf.Floor(timeInSeconds) + " sec";
    }

    // Funci�n para reiniciar el tiempo
    public void ResetTime()
    {
        timeInSeconds = 0f;
    }
}