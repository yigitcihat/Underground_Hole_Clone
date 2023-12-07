using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UpgradeSection : MonoBehaviour
{
    public UpgradeSectionTypes type;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI costText;

    public int level;
    public int cost;
    public float multiply;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        level = PlayerPrefs.GetInt($"{type}_Level", 1);
        cost = PlayerPrefs.GetInt($"{type}_Cost", cost);
        levelText.text = $"Lvl{level}";
        costText.text = $"${cost}";
    }

    private void Update()
    {
        if (Game.Money >= cost)
        {
            if (_button.interactable) return;
            _button.interactable = true;
        }
        else
        {
            if (!_button.interactable) return;
            _button.interactable = false;
        }
    }

    public void Upgrade()
    {
        level++;
        PlayerPrefs.GetInt($"{type}_Level", level);
        levelText.text = $"Lvl{level}";
        cost = (int)(cost * multiply);
        PlayerPrefs.GetInt($"{type}_Cost", cost);
        costText.text = $"${cost}";

    }
}