using System;
using System.Collections;
using UnityEngine;

public class FullText : HcbPanel
{
    private bool _isActive;
    private CanvasGroup _canvasGroup;

    protected override void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        EventManager.AddListener(GameEvent.OnCapacityFull,ActivateFullText);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(GameEvent.OnCapacityFull,ActivateFullText);
    }
    


    private void ActivateFullText()
    {
        if (_isActive)
            return;
        _isActive = true;
        ShowPanel();
        StartCoroutine(AutoDisable());
    }
    private IEnumerator AutoDisable()
    {
        yield return new WaitForSeconds(1f);
        HidePanel();
        _isActive = false;
        
       
    }
}
