
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterUpgradePanel : HcbPanel
{
    [SerializeField] private UpgradeSection radius;
    [SerializeField] private UpgradeSection capacity;
    [SerializeField] private UpgradeSection speed;
    private void OnEnable()
    {
        EventManager.AddListener(GameEvent.OnCharacterUpgrade,ShowPanel);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(GameEvent.OnCharacterUpgrade,ShowPanel);
    }

    public void UpgradeMagnetRadius()
    {
        EventsSystem.OnCurrencyInteracted.Invoke(-radius.cost);
        EventManager.Broadcast(GameEvent.OnMagnetUpgrade);
        radius.Upgrade();
        
    }

    public void UpgradeStackCapacity()
    {
        EventsSystem.OnCurrencyInteracted.Invoke(-capacity.cost);
        EventManager.Broadcast(GameEvent.OnCapacityUpgrade);
        capacity.Upgrade();
    }

    public void UpgradeCharacterSpeed()
    {
        EventsSystem.OnCurrencyInteracted.Invoke(-speed.cost);
        EventManager.Broadcast(GameEvent.OnCharacterSpeedUpgrade);
        speed.Upgrade();
    }

}
