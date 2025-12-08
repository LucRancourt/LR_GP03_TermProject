using System;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    private float _totalTimePassed = 0.0f;

    [SerializeField] private TextMeshProUGUI timerText;

    public bool IsActive = false;


    private void Update()
    {
        if (IsActive)
            TickTime();
    }

    public void TickTime()
    {
        _totalTimePassed += Time.deltaTime;

        if (timerText != null)
            SetText();
    }

    private void SetText()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(_totalTimePassed);

        if (timeSpan.Hours > 0)
            timerText.text = $"{timeSpan.Hours}:{timeSpan.Minutes:00}:{timeSpan.Seconds:00}";
        else
            timerText.text = $"{timeSpan.Minutes:00}:{timeSpan.Seconds:00}";
    }
}
