using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class AreaController : MonoBehaviour
{
    private int _unlockedBuildingCount;
    private int _level;
    [SerializeField] private List<Transform> levels;
    [SerializeField] private List<CinemachineVirtualCamera> level2Cameras;
    [SerializeField] private List<CinemachineVirtualCamera> level3Cameras;
    [SerializeField] private List<CinemachineVirtualCamera> level4Cameras;

    private void OnEnable()
    {
        EventManager.AddListener(GameEvent.OnNewBuildingUnlock, OpenNewAreas);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(GameEvent.OnNewBuildingUnlock, OpenNewAreas);
    }

    private void Start()
    {
        _unlockedBuildingCount = PlayerPrefs.GetInt(PlayerPrefKeys.UnlockedBuildCount, 0);
        _level = PlayerPrefs.GetInt(PlayerPrefKeys.Level, 0);
        for (var i = 0; i <= _level; i++)
        {
            UnlockNewAreas(i);
        }
    }

    private void OpenNewAreas()
    {
        _unlockedBuildingCount++;
        PlayerPrefs.SetInt(PlayerPrefKeys.UnlockedBuildCount, _unlockedBuildingCount);
        SetLevel();
        switch (_level)
        {
            case 1:
                StartCoroutine(ShowNewAreas(level2Cameras));
                break;
            case 2:
                StartCoroutine(ShowNewAreas(level3Cameras));
                break;
            case 3:
                StartCoroutine(ShowNewAreas(level4Cameras));
                break;
        }
    }

    private void SetLevel()
    {
        Debug.Log(_unlockedBuildingCount);
        switch (_unlockedBuildingCount)
        {
            case  >=6:
                _level = 3;
                PlayerPrefs.SetInt(PlayerPrefKeys.Level, _level);
                UnlockNewAreas(_level);
                break;
            case >=3:
                _level = 2;
                PlayerPrefs.SetInt(PlayerPrefKeys.Level, _level);
                UnlockNewAreas(_level);
                break;
            case >=1:
                _level = 1;
                PlayerPrefs.SetInt(PlayerPrefKeys.Level, _level);
                UnlockNewAreas(_level);
                break;
            default:
                _level = 0;
                PlayerPrefs.SetInt(PlayerPrefKeys.Level, _level);
                UnlockNewAreas(_level);
                break;
                
        }
    }

    private void UnlockNewAreas(int level)
    {
        Debug.Log("UnlockNewArea");
        for (var i = 0; i < levels[level].childCount; i++)
        {
            levels[level].GetChild(i).gameObject.SetActive(true);
        }
    }
    private IEnumerator ShowNewAreas(List<CinemachineVirtualCamera> cameras)
    {
        var priority = 15;
        foreach (var cam in cameras)
        {
            cam.Priority = priority;
            priority++;
            yield return new WaitForSeconds(2f);
        }

        foreach (var cam in cameras)
        {
            cam.Priority = 2;
        }
    }
}