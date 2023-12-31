using UnityEngine;
using UnityEngine.UI;

public class CharacterUpgradeArea : MonoBehaviour
{
    [SerializeField] private Image radialBar;
    private const float COMPLETE_TIME = 2f;
    private float _timer;
    private PlayerMovement player;
    private bool _isCharacterInUpgradeArea;

    private void OnTriggerEnter(Collider other)
    {
        player = other.GetComponent<PlayerMovement>();
        if (player == null) return;
        _isCharacterInUpgradeArea = true;
    }

    private void OnTriggerExit(Collider other)
    {
        player = other.GetComponent<PlayerMovement>();
        if (player == null) return;
        _isCharacterInUpgradeArea = false;
        _timer = 0;

    }

    private void Update()
    {
        if (_isCharacterInUpgradeArea)
        {
            
            if ((_timer < COMPLETE_TIME))
            {
                _timer += Time.deltaTime;
                var fillAmount = _timer / COMPLETE_TIME;
                radialBar.fillAmount = fillAmount;
            }
            else
            {
                EventManager.Broadcast(GameEvent.OnCharacterUpgrade);
                _isCharacterInUpgradeArea = false;
                radialBar.fillAmount = 0;
            }
            
          

        }
        else
        {
            if (radialBar.fillAmount >0)
            {
                radialBar.fillAmount -= Time.deltaTime *2f;
            }
            else
            {
                radialBar.fillAmount = 0;
            }
                
           
        }
    }
}
