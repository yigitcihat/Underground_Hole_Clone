using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractions : MonoBehaviour
{
    // [SerializeField] private Transform moneyText;
    // [SerializeField] private Transform canvas;
    // private Animator Animator => GetComponent<Animator>();
    // private float _timer;
    // private float _interval = 0.1f;

    public void OnTriggerEnter(Collider other)
    {
        if (Game.IsHole) return;
        var isCollectable = other.CompareTag("Collectable");

        if (isCollectable)
        {
            StackController.Instance.PickUpItem(other.transform);
            
        }
        
    }

 
    private void OnTriggerStay(Collider other)
    {
        //     var exchange = other.GetComponent<ExchangeZone>();
        //
        //     if (exchange)
        //     {
        //         if (StackController.Instance.stackList.Count > 0)
        //         {
        //             _timer += Time.deltaTime;
        //             if (!(_timer >= _interval)) return;
        //
        //             var lastTokenIndex = StackController.Instance.stackList.Count;
        //             var token = StackController.Instance.stackList[lastTokenIndex - 1].GetComponent<Resource>();
        //             StackController.Instance.stackList.Remove(token.transform);
        //             var money = PlayerPrefs.GetInt(PlayerPrefKeys.Money, 200);
        //             // money += token.coinValue;
        //
        //             var targetPosition = moneyText.position;
        //             var moneyUI = PoolingSystem.Instance.InstantiateAPS("MoneyUI", Vector3.zero);
        //             moneyUI.transform.SetParent(canvas);
        //             moneyUI.transform.localPosition = Vector3.zero;
        //             PoolingSystem.Instance.DestroyAPS(token.gameObject);
        //             moneyUI.transform.DOMove(targetPosition, 0.1f).OnComplete(() =>
        //             {
        //                 PoolingSystem.Instance.InstantiateAPS("SaleToken");
        //                 moneyText.DOShakeScale(0.1f, 0.2f, 2, 20);
        //                 PoolingSystem.Instance.DestroyAPS(moneyUI);
        //             });
        //             _timer = 0;
        //         }
        //     }
        //   
    }
}