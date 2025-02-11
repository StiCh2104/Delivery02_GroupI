using UnityEngine;
using UnityEngine.UI;

public class TimeDisplay : MonoBehaviour
{
    public Text timeText;

    void Start()
    {
        if (timeText == null)
        {
            Debug.LogError("Error in timeText (is null).");
        }
    }

    void Update()
    {
        float timeInSeconds = Time.time;
        timeText.text = "Time: " + Mathf.Floor(timeInSeconds) + " sec";
    }
}
