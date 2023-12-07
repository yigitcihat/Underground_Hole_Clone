using System.Collections;
using UnityEngine;

public class HoleUpgradePanel : HcbPanel
{
    [SerializeField] private UpgradeSection radius;
    [SerializeField] private UpgradeSection timer;
    [SerializeField] private UpgradeSection speed;
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

    public override void HidePanel()
    {
        base.HidePanel();
        Game.IsHole = true;
        
    }
    public void UpgradeHoleRadius()
    {
        EventsSystem.OnCurrencyInteracted.Invoke(-radius.cost);
        EventManager.Broadcast(GameEvent.OnHoleRadiusUpgrade);
        radius.Upgrade();
        
    }

    public void UpgradeTime()
    {
        EventsSystem.OnCurrencyInteracted.Invoke(-timer.cost);
        EventManager.Broadcast(GameEvent.OnHoleTimerUpgrade);
        timer.Upgrade();
    }

    public void UpgradeHoleSpeed()
    {
        EventsSystem.OnCurrencyInteracted.Invoke(-speed.cost);
        EventManager.Broadcast(GameEvent.OnHoleSpeedUpgrade);
        speed.Upgrade();
    }
}
