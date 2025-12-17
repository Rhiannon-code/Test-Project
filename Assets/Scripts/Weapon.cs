using System;
using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{

public GameObject bulletPrefab;
public Transform bulletSpawn;
public float bulletVelocity = 30;
public float bulletPrefabLifeTime = 3f;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireWeapon();
        }
    }

    private void FireWeapon()
    {
        //Insantiate the Bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        //Shoot Bullet
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward.normalized * bulletVelocity, ForceMode.Impulse);
        //Destroy Bullet After Some Time
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime));


    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}
