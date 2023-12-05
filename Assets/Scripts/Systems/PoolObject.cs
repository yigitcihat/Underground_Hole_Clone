using UnityEngine;


    public class PoolObject : MonoBehaviour
    {
        private void Start()
        {

            EventManager.AddListener(GameEvent.OnLevelFinish,() => transform.SetParent(PoolingSystem.Instance.transform));
        }

        private void OnDestroy()
        {
            EventManager.RemoveListener(GameEvent.OnLevelFinish,() => transform.SetParent(PoolingSystem.Instance.transform));
        }
    }

