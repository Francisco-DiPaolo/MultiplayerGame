using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Shot : NetworkBehaviour
{
    
    [Header("Shooting")]
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletForce = 20f;

    public void Spawn()
    {
        NetworkObject bullet = Runner.Spawn(bulletPrefab, firePoint.position, firePoint.rotation);
        //GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rbBullet = bullet.GetComponent<Rigidbody>();
        rbBullet.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);

        Destroy(bullet, 20);
    }
}
