using UnityEngine;
using static Weapon;

public class SoundManager : MonoBehaviour
{
public static SoundManager Instance {get; set; }

public AudioSource ShootingChannel;

public AudioClip PistolShot;
public AudioClip RifleShot;
public AudioSource reloadingSoundRifle;
public AudioSource reloadingSoundPistol;
public AudioSource emptyMagazineSoundPistol;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlayShootingSound(WeaponModel weapon)
    {
        switch (weapon)
        {
            case WeaponModel.Pistol:
                ShootingChannel.PlayOneShot(PistolShot);
                break;
            case WeaponModel.Rifle:
                ShootingChannel.PlayOneShot(RifleShot);
                break;
        }
    }
    public  void PlayReloadingSound(WeaponModel weapon)
    {
          switch (weapon)
        {
            case WeaponModel.Pistol:
                reloadingSoundPistol.Play();
                break;
            case WeaponModel.Rifle:
                reloadingSoundRifle.Play();
                break;
        }
    }

}
