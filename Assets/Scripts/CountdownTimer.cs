using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    private int _totalTime = 8;
    private TextMeshPro _timerText;
    private TextMeshPro TimerText => _timerText == null ? _timerText = GetComponent<TextMeshPro>() : _timerText;
    private float _currentTime;
    private bool _isTimerStart;

    private void OnEnable()
    {
        EventManager.AddListener(GameEvent.OnUpgradeTimer, UpgradeTimer);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(GameEvent.OnUpgradeTimer, UpgradeTimer);
    }

    private void Start()
    {
        _currentTime = PlayerPrefs.GetInt("Timer", _totalTime);
        _currentTime = _totalTime;
        UpdateTimerDisplay();
    }

    private void Update()
    {
        if (!Game.IsHole) return;
        if (_currentTime > 0)
        {
            _currentTime -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        else
        {
            _currentTime = 0;
            UpdateTimerDisplay();
            StartCoroutine(MoveToBottom());
        }
    }

    private void UpgradeTimer()
    {
        _totalTime += 2;
        PlayerPrefs.SetFloat("Timer", _totalTime);
        UpdateTimerDisplay();
    }

    private IEnumerator MoveToBottom()
    {
        EventManager.Broadcast(GameEvent.OnTransportBottomStage);
        yield return new WaitForSeconds(1f);
        _currentTime = PlayerPrefs.GetFloat("Timer", _totalTime);
        Game.IsHole = false;
    }

    private void UpdateTimerDisplay()
    {
        var minutes = Mathf.FloorToInt(_currentTime / 60);
        var seconds = Mathf.FloorToInt(_currentTime % 60);

        var timerString = $"{minutes:00}:{seconds:00}";
        TimerText.text = timerString;
    }
}