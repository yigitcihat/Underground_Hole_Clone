using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Building : MonoBehaviour
{

    [SerializeField] private int earningAmount =2;
    private void EarnMoney()
    {
        var moneyObject = PoolingSystem.Instance.InstantiateAPS("MoneyText", transform.position);
        var moneyAnim = moneyObject.GetComponent<MoneyAnimation>();

        if (moneyAnim)
        {
            moneyAnim.EarnMoney(earningAmount);
        }
    }
    
    
    

}
