
using UnityEngine;

public class CharacterUpgradePanel : HcbPanel
{
    private void OnEnable()
    {
        EventManager.AddListener(GameEvent.OnCharacterUpgrade,ShowPanel);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(GameEvent.OnCharacterUpgrade,ShowPanel);
    }

}
