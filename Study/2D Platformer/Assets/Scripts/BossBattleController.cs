using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleController : MonoBehaviour
{
    bool bossActive;

    public GameObject blockers;
    public Transform camPoint;
    CameraController camController;

    public float cameraMoveSpeed;

    public Transform theBoss;
    public float bossGrowSpeed = 2f;

    public Transform projectileLauncher;
    public float launcherGrowSpeed = 2f;

    public float launcherRotateSpeed = 90f;
    float launcherRotation;
    
    void Start()
    {
        camController = FindFirstObjectByType<CameraController>();

        theBoss.localScale = Vector3.zero;
    }

    void Update()
    {
        if (bossActive == true)
        {
            camController.transform.position = Vector3.MoveTowards(
                camController.transform.position,
                camPoint.position,
                cameraMoveSpeed * Time.deltaTime);

            if (theBoss.localScale != Vector3.one)
            {
                theBoss.localScale = Vector3.MoveTowards(
                    theBoss.localScale,
                    Vector3.one,
                    bossGrowSpeed * Time.deltaTime);
            }

            if (projectileLauncher.transform.localScale != Vector3.one)
            {
                projectileLauncher.localScale = Vector3.MoveTowards(
                    projectileLauncher.localScale,
                    Vector3.one,
                    launcherGrowSpeed * Time.deltaTime);
            }

            launcherRotation += launcherRotateSpeed * Time.deltaTime;

            if (launcherRotation > 360f)
                launcherRotation -= 360f;

            projectileLauncher.transform.localRotation = Quaternion.Euler(0f, 0f,launcherRotation);
        }
    }

    public void ActivateBattle()
    {
        bossActive = true;
        blockers.SetActive(true);
        camController.enabled = false;
    }
}
