using UnityEngine;
using UnityEngine.Events;

public static class EventsSystem 
{
    public static UnityEvent OnPlayerDataChange = new UnityEvent();
    public static CurrencyEvent OnCurrencyInteracted = new CurrencyEvent();

    public static StringEvet OnStatUpdated = new StringEvet();
    public static UnityEvent OnRemoteUpdated = new UnityEvent();
    public static AnalyticEvent OnLogEvent = new AnalyticEvent();

    #region Editor
    public static UnityEvent OnLevelDataChange = new UnityEvent();
    #endregion
}
public class CurrencyEvent : UnityEvent<int> { }
public class StringEvet : UnityEvent<string> { }
public class AnalyticEvent : UnityEvent<string, string, string> { }