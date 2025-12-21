using System;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance {get; set; }
    
    [Header("Ammo")]
    public TextMeshProUGUI magazineAmmoUI;
    public TextMeshProUGUI totalAmmoUI;
    public Image ammoTypeUI;

    [Header("Weapon")]
    public Image activeWeaponUI;
    public Image unActiveWeaponUI;

    [Header("Throwables")]
    public Image lethalUI;
    public TextMeshProUGUI leathalAmountUI;
    public Image tacticalUI;
    public TextMeshProUGUI tacticalAmountUI;

    public Sprite emptySlot;

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
    private void Update()
    {
      Weapon activeWeapon = WeaponManager.Instance.activeWeaponSlot.GetComponentInChildren<Weapon>();
      Weapon unActiveWeapon = GetUnActiveWeapon().GetComponentInChildren<Weapon>();

    if (activeWeapon)
        {
            magazineAmmoUI.text = $"{activeWeapon.bulletsLeft / activeWeapon.bulletsPerBurst}";
            totalAmmoUI.text = WeaponManager.Instance.CheckAmmoLeftFor(activeWeapon.thisWeaponModel).ToString();
            Weapon.WeaponModel model = activeWeapon.thisWeaponModel;
            ammoTypeUI.sprite = GetAmmoTypeSprite(model);
            activeWeaponUI.sprite= GetWeaponSprite(model);

            if(unActiveWeapon)
            {
                unActiveWeaponUI.sprite = GetWeaponSprite(unActiveWeapon.thisWeaponModel);
            }
        }   
    else
            {
              magazineAmmoUI.text = "";
              totalAmmoUI.text = "";
              ammoTypeUI.sprite = emptySlot;
              activeWeaponUI.sprite = emptySlot;
              unActiveWeaponUI.sprite = emptySlot;
            } 
    }

    private Sprite GetWeaponSprite(Weapon.WeaponModel model)
    {
        switch (model)
        {
            case Weapon.WeaponModel.Pistol:
            return (Resources.Load<GameObject>("Pistol_Weapon")).GetComponent<SpriteRenderer>().sprite;

            case Weapon.WeaponModel.Rifle:
            return (Resources.Load<GameObject>("Rifle_Weapon")).GetComponent<SpriteRenderer>().sprite;

            default:
         return null;
        }
    }

    private Sprite GetAmmoTypeSprite(Weapon.WeaponModel model)
    {
        switch (model)
        {
            case Weapon.WeaponModel.Pistol:
            return (Resources.Load<GameObject>("Pistol_Ammo")).GetComponent<SpriteRenderer>().sprite;

            case Weapon.WeaponModel.Rifle:
            return (Resources.Load<GameObject>("Rifle_Ammo")).GetComponent<SpriteRenderer>().sprite;

            default:
            return null;
        }
    }

    private GameObject GetUnActiveWeapon()
    {
        foreach (GameObject weaponSlot in WeaponManager.Instance.weaponSlots)
        {
            if (weaponSlot != WeaponManager.Instance.activeWeaponSlot)
            {
                return weaponSlot;
            }
        }
        return null;
    }
}
