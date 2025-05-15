using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEnemy : MonoBehaviour
{

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform fireingPoint;
    [Range(0.1f, 4f)]
    [SerializeField] private float fireRate = 0.5f;
    private float fireTimer=0;

    private void FixedUpdate()
    {
        if (fireTimer <= 0f)
        {
            
                fireTimer = fireRate;
                Shoot();
        }
        else
        {
            fireTimer -= Time.deltaTime;
        }
    }
    private void Shoot()
    {
        Instantiate(bulletPrefab, fireingPoint.position, fireingPoint.rotation);
    }
}
