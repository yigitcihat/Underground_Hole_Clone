using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;
using Random2 = System.Random;
using System.Linq;

public class Prop : MonoBehaviour
{
    private bool _isDropping;
    private bool _isDestroyed;
    private void Awake()
    {
        PlayerPrefs.GetInt($"{gameObject.name}_isDestroy", 0);
        _isDestroyed = PlayerPrefs.GetInt($"{gameObject.name}_isDestroy", 0) == 1;
        if (_isDestroyed)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isDropping) return;
        _isDropping = true;
        var scale = transform.localScale;
        if (other.CompareTag("Blackhole"))
        {
            transform.DOScale(scale / 1.7f, 0.3f).OnComplete(() =>
            {
                transform.localScale = scale;
                _isDropping = false;
            });
        }
    }

    private void Update()
    {
        if (!(transform.position.y < -1)) return;
        var randomPos = new Vector3(Random.Range(-2, 2), 0, Random.Range(0, 3));
        var rb = PoolingSystem.Instance.InstantiateAPS(GetResourceType().ToString(), transform.position)
            .GetComponent<Rigidbody>();
        rb.AddForce((Vector3.down + randomPos) * 50);
        PlayerPrefs.SetInt($"{gameObject.name}_isDestroy", 1);
        Destroy(gameObject);
    }

    private static ResourceTypes GetResourceType()
    {
        var ironCount = Game.CreatedResources.Count(resource => resource.type == ResourceTypes.Iron);
        var woodCount = Game.CreatedResources.Count(resource => resource.type == ResourceTypes.Wood);
        var plasticCount = Game.CreatedResources.Count(resource => resource.type == ResourceTypes.Plastic);

        var requiredIronCount = Game.RequiredIron - ironCount;
        var requiredWoodCount = Game.RequiredWood - woodCount;
        var requiredPlasticCount = Game.RequiredPlastic - plasticCount;

        var checkFunctions = new List<Func<ResourceTypes>>
        {
            () => requiredIronCount > 0 ? ResourceTypes.Iron : ResourceTypes.None,
            () => requiredWoodCount > 0 ? ResourceTypes.Wood : ResourceTypes.None,
            () => requiredPlasticCount > 0 ? ResourceTypes.Plastic : ResourceTypes.None
        };

        checkFunctions = checkFunctions.OrderBy(f => new Random2().Next()).ToList();

        foreach (var result in checkFunctions.Select(checkFunction => checkFunction()).Where(result => result != ResourceTypes.None))
        {
            return result;
        }

        var resourceTypes = Enum.GetValues(typeof(ResourceTypes));
        var randomIndex = new Random2().Next(resourceTypes.Length);
        var random = (ResourceTypes)resourceTypes.GetValue(randomIndex);
        return random != ResourceTypes.None ? random : ResourceTypes.Iron;




    }

}