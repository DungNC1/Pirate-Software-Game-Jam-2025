using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    CinemachineVirtualCamera m_CinemachineVirtualCamera;

    private void Awake()
    {
        m_CinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        m_CinemachineVirtualCamera.Follow = PlayerInputHandler.Instance.gameObject.transform;
    }
}
