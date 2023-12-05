using System;
using System.Collections.Generic;

public enum GameEvent
{
    OnGameStart,
    OnTransportTopStage,
    OnTransportBottomStage,
    OnLevelFinish,
    OnNavmeshSurfaceUpdate,
    NewZoneActivated,
    OnCharacterUpgrade,
    OnUpgradeTimer,
    OnHoleUpgrade,
    OnMoneyCollect,
    OnLevelDataChange,
    LevelFail,
    OnPlayerDataChange
}

public static class EventManager
{
    private static Dictionary<GameEvent, Action> _events = new();

    public static void AddListener(GameEvent gameEvent, Action action)
    {
        if (!_events.ContainsKey(gameEvent))
            _events[gameEvent] = action;
        else
            _events[gameEvent] += action;
    }

    public static void RemoveListener(GameEvent gameEvent, Action action)
    {
        if (!_events.ContainsKey(gameEvent)) return;
        _events[gameEvent] -= action;
        if (_events[gameEvent] == null)
            _events.Remove(gameEvent);
    }

    public static void Broadcast(GameEvent gameEvent)
    {
        if (!_events.TryGetValue(gameEvent, out var @event)) return;
        if (@event != null)
            @event.Invoke();
    }
}