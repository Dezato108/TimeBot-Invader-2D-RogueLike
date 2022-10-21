using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamController : MonoBehaviour
{
    PlayerController playerToLookAt;
    CinemachineVirtualCamera virtualCamera;
    // Start is called before the first frame update
    void Start()
    {
        playerToLookAt = FindObjectOfType<PlayerController>();
        virtualCamera = GetComponent<CinemachineVirtualCamera>();

        virtualCamera.Follow = playerToLookAt.transform;
    }

    // Update is called once per frame
    void Update()
    {
        while (playerToLookAt == null)
        {
            playerToLookAt = FindObjectOfType<PlayerController>();
            if (virtualCamera)
            {
                virtualCamera.Follow = playerToLookAt.transform;
            }
        }
    }
}
