using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    
    [Header("Shooting")]
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject [] bulletPrefab;
    [SerializeField] float bulletForce = 20f;
    [SerializeField] float timeSpawn;
    [SerializeField] float repeatSpawnRate;


    void Start()
    {
        InvokeRepeating ( "Spawn" , timeSpawn , repeatSpawnRate );
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Spawn()
    {
     
        GameObject bullet = Instantiate(bulletPrefab[Random.Range(0, bulletPrefab.Length)], firePoint.position, firePoint.rotation);
        Rigidbody rbBullet = bullet.GetComponent<Rigidbody>();
        rbBullet.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);

        Destroy(bullet, 10);
    }

    /*public IEnumerator timeCD()
    {
        
        canShot = false;
        yield return new WaitForSeconds(delay);
        canShot = true;
        Spawn();
    
    }*/
}
