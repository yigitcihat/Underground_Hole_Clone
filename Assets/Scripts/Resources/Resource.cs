using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Resource : MonoBehaviour
{
    public ResourceTypes type;

    private Rigidbody _rb;
  
    public Transform toFollow;
    public float duration;
    public float posY;
    public Vector3 offSet;

    public bool isTriggerOpen = true;
    public bool isCollected;

    private Vector3 _startScale;
    private int _collectedIndex = 0;
    
    private void OnEnable()
    {
        isTriggerOpen = true;
        _rb.useGravity = true;
    }

    private void Awake()
    {
        _startScale = transform.localScale;
        _rb = GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        if (!isCollected) return;
        var followPos = new Vector3(toFollow.position.x, posY, toFollow.position.z) + offSet;
        transform.position = Vector3.Lerp(transform.position,followPos,duration);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Ground")) return;
        var moneyObject = PoolingSystem.Instance.InstantiateAPS("MoneyText", transform.position);
        var moneyAnim = moneyObject.GetComponent<MoneyAnimation>();

        if (moneyAnim)
        {
            moneyAnim.EarnMoney(1);
        }
    }

    public void Collect(Transform stackPos,int collectIndex,int listCount)
    {
        toFollow = stackPos;
        transform.DOMove(stackPos.position + new Vector3(0f,posY,0f) + (Vector3.up * 0.3f), 0.3f).OnComplete(() => isCollected = true);
        _collectedIndex = collectIndex;
        duration = 0.22f - listCount * 0.0035f;
    }
    

    public void ReList(int listCount)
    {
        duration = 0.22f - listCount * 0.0035f;
    }
    
    public void UnCollect()
    {
        isCollected = false;
        transform.DOLocalRotate(new Vector3(90f,0f,135f), 1.5f, RotateMode.LocalAxisAdd);
    }

    public void Recycle()
    {
        transform.localScale = _startScale;
    }

    public void DropDown()
    {
        if (this == null) return;
        if (!isCollected) return;
        isCollected = false;
        isTriggerOpen = true;
        transform.DOMove(
            _collectedIndex % 2 == 0
                ? new Vector3(transform.position.x + Random.Range(0.75f, 1f), -3f,
                    transform.position.z + Random.Range(0.5f, 1.5f))
                : new Vector3(transform.position.x - Random.Range(0.75f, 1f), -3f,
                    transform.position.z + Random.Range(0.5f, 1.5f)), 0.5f);
    }
    
   
}
