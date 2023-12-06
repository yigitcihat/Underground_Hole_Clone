
public class StartPanel : HcbPanel
{
    public void StartGame()
    {
        EventManager.Broadcast(GameEvent.OnGameStart);
        HidePanel();
    }
}
