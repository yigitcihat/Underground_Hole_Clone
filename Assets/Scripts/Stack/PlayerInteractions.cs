using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField]private SphereCollider _sphereCollider;

    private float _radius;

    private void OnEnable()
    {
        EventManager.AddListener(GameEvent.OnMagnetUpgrade,UpgradeMagnetRadius);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(GameEvent.OnMagnetUpgrade,UpgradeMagnetRadius);
    }

    private void Awake()
    {
        _radius = PlayerPrefs.GetFloat(PlayerPrefKeys.MagnetRadius, 1);
        _sphereCollider = GetComponentInChildren<SphereCollider>();
        _sphereCollider.radius = _radius;
    }

    private void UpgradeMagnetRadius()
    {
        _radius *= 1.2f;
        PlayerPrefs.SetFloat(PlayerPrefKeys.MagnetRadius, _radius);
        _sphereCollider.radius = _radius;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (Game.IsHole) return;
        var isCollectable = other.CompareTag("Collectable");

        if (isCollectable)
        {
            StackController.Instance.PickUpItem(other.transform);
            
        }
        
    }
    
}