using System.Collections;
using UnityEngine;

public class HoleUpgradePanel : HcbPanel
{
    private void OnEnable()
    {
        EventManager.AddListener(GameEvent.OnTransportTopStage,()=>StartCoroutine(ShowPanelWithDelay()));
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(GameEvent.OnTransportTopStage,()=>StartCoroutine(ShowPanelWithDelay()));

    }

    private IEnumerator ShowPanelWithDelay()
    {
        yield return new WaitForSeconds(1f);
        ShowPanel();
    }
}
