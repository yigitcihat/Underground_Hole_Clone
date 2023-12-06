using DG.Tweening;
using TMPro;
using UnityEngine;

public class MoneyAnimation : MonoBehaviour
{

    internal void EarnMoney(int money)
    {
        var currentPos = transform.position;
        var currentScale = transform.localScale;
        EventsSystem.OnCurrencyInteracted.Invoke(money);
        var moneyText = GetComponent<TextMeshPro>();
        moneyText.text = $"${money}";
        transform.DOMove(currentPos + new Vector3(0, 1, 0), 0.5f);
        transform.DOScale((currentScale * 1.5f), 0.6f).OnComplete(() =>
        {
            PoolingSystem.Instance.DestroyAPS(gameObject);
        });
    }

    private void OnDisable()
    {
        transform.localScale = Vector3.one;
        
    }
}
