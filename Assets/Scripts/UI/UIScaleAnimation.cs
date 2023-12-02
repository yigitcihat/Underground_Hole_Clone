using DG.Tweening;
using UnityEngine;

public class UIScaleAnimation : MonoBehaviour
{
    private void Start()
    {
        transform.DOScale(1.1f, 1f)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
