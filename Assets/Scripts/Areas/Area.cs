using System.Collections;
using System.Security.Cryptography;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Area : MonoBehaviour
{
    [SerializeField] private GameObject activationArea;
    [SerializeField] private GameObject areaLineSquare;
    [SerializeField] private Image radialBar;

    [SerializeField] private GameObject building;

    [SerializeField] private int requiredIron;
    [SerializeField] private int requiredWood;
    [SerializeField] private int requiredPlastic;

    [SerializeField] private TextMeshProUGUI ironTMP;
    [SerializeField] private TextMeshProUGUI woodTMP;
    [SerializeField] private TextMeshProUGUI plasticTMP;

    private int _currentIron;
    private int _currentWood;
    private int _currentPlastic;

    private int _totalMaterial;

    private bool _isUnlocked;
    private bool _isCoroutineRunning = false;

    private void Awake()
    {
        _isUnlocked = PlayerPrefs.GetInt($"{name}_isUnlocked", 0) == 1;
        if (_isUnlocked)
        {
            UnlockArea();
           
        }
        else
        {
            SetRequirementTexts();
            _currentIron = requiredIron;
            _currentWood = requiredWood;
            _currentPlastic = requiredPlastic;

            _totalMaterial = requiredIron + requiredWood + requiredPlastic;
        }

        SetRequirements();
        
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<PlayerMovement>();
        if (player == null || player.isMoving) return;

        var stackController = player.GetComponent<StackController>();

        if (_totalMaterial > 0 && !_isCoroutineRunning)
        {
            StartCoroutine(ActivateAvailable(stackController));
        }
    }

    private IEnumerator ActivateAvailable(StackController stackController)
    {
        _isCoroutineRunning = true;

        // Handle iron materials
        for (var i = 0; i < _currentIron && stackController.ironList.Count > 0; i++)
        {
            var material = stackController.ironList[0];
            stackController.stackList.Remove(material.transform);
            stackController.ironList.RemoveAt(0);
            material.transform.SetParent(null, true);

            if (ironTMP == null) break;

            material.transform.DOMove(ironTMP.transform.position, 0.1f);
            yield return new WaitForSeconds(0.15f);

            PoolingSystem.Instance.DestroyAPS(material.gameObject);

            if (_currentIron <= 0) break;
            AddIron(1);
            stackController.ironCount--;
        }

        for (var i = 0; i < _currentWood && stackController.woodList.Count > 0; i++)
        {
            var material = stackController.woodList[0];
            stackController.stackList.Remove(material.transform);
            stackController.woodList.RemoveAt(0);
            material.transform.SetParent(null, true);

            if (woodTMP == null) break;

            material.transform.DOMove(woodTMP.transform.position, 0.1f);
            yield return new WaitForSeconds(0.15f);

            PoolingSystem.Instance.DestroyAPS(material.gameObject);

            if (_currentWood <= 0) break;
            AddWood(1);
            stackController.woodCount--;
        }

        for (var i = 0; i < _currentPlastic && stackController.plasticList.Count > 0; i++)
        {
            var material = stackController.plasticList[0];
            stackController.stackList.Remove(material.transform);
            stackController.plasticList.RemoveAt(0);
            material.transform.SetParent(null, true);

            if (plasticTMP == null) break;

            material.transform.DOMove(plasticTMP.transform.position, 0.1f);
            yield return new WaitForSeconds(0.15f);

            PoolingSystem.Instance.DestroyAPS(material.gameObject);

            if (_currentPlastic <= 0) break;
            AddPlastic(1);
            stackController.plasticCount--;
        }

        if (_totalMaterial <= 0)
        {
            UnlockArea();
            EventManager.Broadcast(GameEvent.OnNewBuildingUnlock);
            PoolingSystem.Instance.InstantiateAPS("SmokeExp", building.transform.position);
        }

        _isCoroutineRunning = false;
    }

    private void SetRequirementTexts()
    {
        requiredIron = PlayerPrefs.GetInt($"{name}_requiredIron", requiredIron);
        requiredWood = PlayerPrefs.GetInt($"{name}_requiredWood", requiredWood);
        requiredPlastic = PlayerPrefs.GetInt($"{name}_requiredPlastic", requiredPlastic);

        if (ironTMP is not null)
        {
            ironTMP.text = requiredIron.ToString();
            if (requiredIron == 0) Destroy(ironTMP.transform.parent.gameObject);
        }

        if (woodTMP is not null)
        {
            woodTMP.text = requiredWood.ToString();
            if (requiredWood == 0) Destroy(woodTMP.transform.parent.gameObject);
        }

        if (plasticTMP is not null)
        {
            plasticTMP.text = requiredPlastic.ToString();
            if (requiredPlastic == 0) Destroy(plasticTMP.transform.parent.gameObject);
        }
    }

    private void UnlockArea()
    {
        _isUnlocked = true;
        PlayerPrefs.SetInt($"{name}_isUnlocked", 1);
        radialBar.transform.parent.gameObject.SetActive(false);
        areaLineSquare.gameObject.SetActive(false);
        building.SetActive(true);
        
    }

    private void SetRequirements()
    {
        Game.RequiredIron += requiredIron;
        Game.RequiredPlastic += requiredPlastic;
        Game.RequiredWood += requiredWood;
    }

    private void AddIron(int amount)
    {
        _currentIron -= amount;
        Game.RequiredIron -= amount;
        PlayerPrefs.SetInt($"{name}_requiredIron", _currentIron);
        ironTMP.text = _currentIron.ToString();
        _totalMaterial -= amount;
        if (_totalMaterial > 0)
        {
            radialBar.fillAmount += (1 / _totalMaterial);
        }

        if (_currentIron <= 0)
        {
            Destroy(ironTMP.transform.parent.gameObject);
        }
    }

    private void AddWood(int amount)
    {
        _currentWood -= amount;
        Game.RequiredWood -= amount;
        PlayerPrefs.SetInt($"{name}_requiredWood", _currentWood);
        woodTMP.text = _currentWood.ToString();
        _totalMaterial -= amount;
        if (_totalMaterial > 0)
        {
            radialBar.fillAmount += (1 / _totalMaterial);
        }

        if (_currentWood <= 0)
        {
            Destroy(woodTMP.transform.parent.gameObject);
        }
    }

    private void AddPlastic(int amount)
    {
        _currentPlastic -= amount;
        Game.RequiredPlastic -= amount;
        PlayerPrefs.SetInt($"{name}_requiredPlastic", _currentPlastic);
        plasticTMP.text = _currentPlastic.ToString();
        _totalMaterial -= amount;
        if (_totalMaterial > 0)
        {
            radialBar.fillAmount += (1 / _totalMaterial);
        }

        if (_currentPlastic <= 0)
        {
            Destroy(plasticTMP.transform.parent.gameObject);
        }
    }
}