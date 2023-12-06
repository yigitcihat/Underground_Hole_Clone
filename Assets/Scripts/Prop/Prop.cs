using System;
using UnityEngine;
using Random = System.Random;

public class Prop : MonoBehaviour
{
    private void Update()
    {
        if (!(transform.position.y < -1)) return;
        PoolingSystem.Instance.InstantiateAPS(GetRandomResourceType().ToString(), transform.position);
        
        Destroy(gameObject);
    }

    private static ResourceTypes GetRandomResourceType()
    {
        var resourceTypes = Enum.GetValues(typeof(ResourceTypes));

        var randomIndex = new Random().Next(resourceTypes.Length);

        return (ResourceTypes)resourceTypes.GetValue(randomIndex);
    }
}