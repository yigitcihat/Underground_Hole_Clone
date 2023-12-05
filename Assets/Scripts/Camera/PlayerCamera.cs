using System;
using Cinemachine;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    private CinemachineVirtualCamera _playerVirtualCamera;
    public CinemachineVirtualCamera PlayerVirtualCamera => _playerVirtualCamera == null ? _playerVirtualCamera = GetComponent<CinemachineVirtualCamera>() : _playerVirtualCamera;


    private void OnEnable()
    {
        EventManager.AddListener(GameEvent.OnTransportTopStage,MoveTopStage);
        EventManager.AddListener(GameEvent.OnTransportBottomStage,MoveBottomStage);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(GameEvent.OnTransportTopStage,MoveTopStage);
        EventManager.RemoveListener(GameEvent.OnTransportBottomStage,MoveBottomStage);
    }

    private void MoveTopStage()
    {
        PlayerVirtualCamera.Priority = 9;
    }

    private void MoveBottomStage()
    {
        PlayerVirtualCamera.Priority = 11;
    }
}
