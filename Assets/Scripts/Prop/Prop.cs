using System;
using UnityEngine;
using Random = System.Random;

public class Prop : MonoBehaviour
{
    private void Update()
    {
        if (!(transform.position.y < -1)) return;
        PoolingSystem.Instance.InstantiateAPS(GetRandomResourceType().ToString(), transform.position);
        var moneyObject = PoolingSystem.Instance.InstantiateAPS("MoneyText", transform.position);
        var moneyAnim = moneyObject.GetComponent<MoneyAnimation>();

        if (moneyAnim)
        {
            moneyAnim.EarnMoney(2);
        }
      
        Destroy(gameObject);
    }

    private static ResourceTypes GetRandomResourceType()
    {
        var resourceTypes = Enum.GetValues(typeof(ResourceTypes));

        var randomIndex = new Random().Next(resourceTypes.Length);

        return (ResourceTypes)resourceTypes.GetValue(randomIndex);
    }
}