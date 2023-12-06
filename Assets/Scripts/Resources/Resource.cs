using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Resource : MonoBehaviour
{
    public ResourceTypes type;

    private void OnEnable()
    {
        isTriggerOpen = true;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Collider>().isTrigger = false;
    }

    public Transform toFollow;
    public float duration;
    public float posY;
    public Vector3 offSet;

    public bool isTriggerOpen = true;
    public bool isCollected;

    private Vector3 _startScale;
    private int _collectedIndex = 0;
    private void Start()
    {
        _startScale = transform.localScale;
    }
    
    private void Update()
    {
        if (!isCollected) return;
        var followPos = new Vector3(toFollow.position.x, posY, toFollow.position.z) + offSet;
        transform.position = Vector3.Lerp(transform.position,followPos,duration);
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
