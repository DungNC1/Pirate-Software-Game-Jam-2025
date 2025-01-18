using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 mousePosition;
    [SerializeField] private GameObject bullet;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private GameObject firePoint;
    private bool canFire;
    private float timer;

    private void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePosition - transform.position;
        float zRotation = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, zRotation);

        if(!canFire)
        {
            timer += Time.deltaTime;

            if(timer > playerStats.shootCooldown)
            {
                canFire = true;
                timer = 0;
            }
        }

        if(Input.GetMouseButtonDown(0))
        {
            canFire = false;
            Instantiate(bullet, firePoint.transform.position, Quaternion.identity);
        }
    }
}
