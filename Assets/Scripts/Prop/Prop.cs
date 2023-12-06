using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;
using Random2 = System.Random;
public class Prop : MonoBehaviour
{
    private bool _isDropping;
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
        var randomPos = new Vector3(Random.Range(-2,2),0,Random.Range(0,3));
        var rb = PoolingSystem.Instance.InstantiateAPS(GetRandomResourceType().ToString(), transform.position ).GetComponent<Rigidbody>();
        rb.AddForce((Vector3.down + randomPos) * 50);
        Destroy(gameObject);
    }

    private static ResourceTypes GetRandomResourceType()
    {
        var resourceTypes = Enum.GetValues(typeof(ResourceTypes));

        var randomIndex = new Random2().Next(resourceTypes.Length);

        return (ResourceTypes)resourceTypes.GetValue(randomIndex);
    }
}