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

    public GameObject projectileToFire;
    public Transform[] projectilePoints;

    public float waitToStartShooting, timeBetweenShots;
    float shootStartCounter, shotCounter;
    int currentShot;
    
    void Start()
    {
        camController = FindFirstObjectByType<CameraController>();

        shootStartCounter = waitToStartShooting;
        
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

            if (shootStartCounter > 0f)
            {
                shootStartCounter -= Time.deltaTime;

                if (shootStartCounter <= 0f)
                {
                    shootStartCounter = timeBetweenShots;
                    FireShot();
                }
            }

            if (shotCounter > 0f)
            {
                shotCounter -= Time.deltaTime;
                if (shotCounter <= 0f)
                {
                    shotCounter = timeBetweenShots;
                    FireShot();
                }
            }
        }
    }

    public void ActivateBattle()
    {
        bossActive = true;
        blockers.SetActive(true);
        camController.enabled = false;
    }

    void FireShot()
    {
        Debug.Log("Fired shot at " + Time.time);
    }
}
