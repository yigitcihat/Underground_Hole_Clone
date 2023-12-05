
public class InGamePanel : HcbPanel
{
    private void OnEnable()
    {
        EventManager.AddListener(GameEvent.OnGameStart,ShowPanel);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(GameEvent.OnGameStart,HidePanel);
    }
}
