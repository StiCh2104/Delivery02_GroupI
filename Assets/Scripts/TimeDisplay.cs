using UnityEngine;
using UnityEngine.UI;

public class TimeDisplay : MonoBehaviour
{
    public Text timeText;
    private float timeInSeconds = 0f; 

    void Start()
    {
        if (timeText == null)
        {
            Debug.LogError("El componente Text no está asignado.");
        }
    }

    void Update()
    {
        timeInSeconds += Time.deltaTime; 

        timeText.text = "Time: " + Mathf.Floor(timeInSeconds) + " sec";
    }

    public void ResetTime()
    {
        timeInSeconds = 0f;
    }
}