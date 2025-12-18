using System;
using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
//Shooting
public bool isShooting, readyToShoot;
bool allowReset = true;
public float shootingDelay= 2f;
//Burst
public int bulletsPerBurst = 3;
public int burstBulletsLeft;
//Spread
public float spreadIntensity;
//Bullet
public GameObject bulletPrefab;
public Transform bulletSpawn;
public float bulletVelocity = 30;
public float bulletPrefabLifeTime = 3f;

public GameObject muzzleEffect;
private Animator animator;

//Fire Mode Selector
public enum ShootingMode
    {
        Single,
        Burst,
        Auto
    }
    public ShootingMode currentShootingMode;
    private void Awake()
    {
        readyToShoot = true;
        burstBulletsLeft = bulletsPerBurst;
       animator = GetComponent<Animator>(); 
    }
    void Update()
    {
       if (currentShootingMode == ShootingMode.Auto)
        {
            //Hold Left Mouse Button
            isShooting = Input.GetKey(KeyCode.Mouse0);
        }
        else if (currentShootingMode == ShootingMode.Single || currentShootingMode == ShootingMode.Burst)
        {
            //Press Left Mouse Button
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        }
        if (readyToShoot && isShooting)
        {
            burstBulletsLeft = bulletsPerBurst;
            FireWeapon();
        }
    }
    private void FireWeapon()
    {
        //Show Muzzle Effect
        muzzleEffect.GetComponent<ParticleSystem>().Play();
        //Play Recoil Animation
        animator.SetTrigger("RECOIL");
        //Play Shooting Sound
        SoundManager.Instance.shootingSoundPistol.Play();

        readyToShoot = false;
        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;
        //Insantiate the Bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        //Point Bullet Towards Shooting Direction
        bullet.transform.forward = shootingDirection;
        //Shoot Bullet
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);
        //Destroy Bullet After Some Time
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime));
        //Check if we are done shooting
        if (allowReset)
        {
            //Reset Shot
            Invoke("ResetShot", shootingDelay);
            allowReset = false;
        }
        //Check if we are in Burst Mode
        if (currentShootingMode == ShootingMode.Burst && burstBulletsLeft > 1)
        {
            burstBulletsLeft--;
            Invoke("FireWeapon", shootingDelay); 
        }
    }
    private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }
    public Vector3 CalculateDirectionAndSpread()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 targetPoint;  
        if (Physics.Raycast(ray, out hit))
        {
            //Hitting something
            targetPoint = hit.point;
        }
        else
        {
            //Some far away point
            targetPoint = ray.GetPoint(1000); 
        }
        Vector3 direction = targetPoint - bulletSpawn.position;
        float x = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        //Return the shooting direction and spread
        return direction + new Vector3(x, y, 0);
    }
    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}
