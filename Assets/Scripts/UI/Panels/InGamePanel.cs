using System;
using UnityEngine;
using UnityEngine.Serialization;

public class InGamePanel : HcbPanel
{
    [SerializeField] private FullText fullText;

    private void OnEnable()
    {
        EventManager.AddListener(GameEvent.OnGameStart, ShowPanel);
        EventManager.AddListener(GameEvent.OnCapacityFull,ActivateFullText);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(GameEvent.OnGameStart, HidePanel);
        EventManager.RemoveListener(GameEvent.OnCapacityFull,ActivateFullText);

    }

    private void ActivateFullText()
    {
        if (fullText.isActiveAndEnabled) return;
        fullText.gameObject.SetActive(true);
    }
}