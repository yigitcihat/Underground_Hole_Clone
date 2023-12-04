using DG.Tweening;
using UnityEngine;

public class UIScaleAnimation : MonoBehaviour
{
    private void Start()
    {
        transform.DOScale(1.2f, 0.5f)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
