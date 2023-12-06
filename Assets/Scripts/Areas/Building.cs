
using DG.Tweening;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private int earningAmount = 2;

    private const float InvokeInterval = 2f;
    
    [Header("Shake")]
    private float shakeDuration = 1f;
    private float shakeStrength = 0.1f;
    private int vibrato = 1;
    private float randomness = 9f;
    [SerializeField] private bool isShakeable;
    private void Start()
    {
        InvokeRepeating(nameof(EarnMoney), 0f, InvokeInterval);
        if (!isShakeable)return;
    }

    private void EarnMoney()
    {
        var moneyObject = PoolingSystem.Instance.InstantiateAPS("MoneyText", transform.position);
        var moneyAnim = moneyObject.GetComponent<MoneyAnimation>();

        if (moneyAnim)
        {
            moneyAnim.EarnMoney(earningAmount);
        }
    }
    private void ShakeBuilding()
    {
        transform.DOShakeScale(shakeDuration, shakeStrength, vibrato, randomness);
    }
}