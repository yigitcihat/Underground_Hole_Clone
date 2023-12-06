using System;
using TMPro;
using UnityEngine;

public class MoneyText : MonoBehaviour
{
    private TextMeshProUGUI moneyText;
    private int currentMoney;
    private void Awake()
    {
        moneyText = GetComponent<TextMeshProUGUI>();
        currentMoney = PlayerPrefs.GetInt(PlayerPrefKeys.Money, 45);
        moneyText.text = currentMoney.ToString();
    }

    private void OnEnable()
    {
       EventsSystem.OnCurrencyInteracted.AddListener(ChangeMoneyText);
    }

    private void OnDisable()
    {
        EventsSystem.OnCurrencyInteracted.RemoveListener(ChangeMoneyText);
    }

    private void ChangeMoneyText(int money)
    {
        currentMoney += money;
        PlayerPrefs.SetInt(PlayerPrefKeys.Money,currentMoney);
        moneyText.text = currentMoney.ToString();
    }
    
    
}