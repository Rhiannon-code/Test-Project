using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance {get; set; }

    public Weapon hoveredWeapon = null;
    public AmmoBox hoveredAmmoBox = null;

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

    void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            if(Physics.Raycast(ray, out hit))
            {
               GameObject objectHitByRaycast = hit.transform.gameObject;

            //Pickup Weapon    
            if(objectHitByRaycast.GetComponent<Weapon>() && objectHitByRaycast.GetComponent<Weapon>().isActiveWeapon == false)
                {
                    hoveredWeapon = objectHitByRaycast.gameObject.GetComponent<Weapon>();
                    hoveredWeapon.GetComponent<Outline>().enabled = true;
                    
                    if(Input.GetKey(KeyCode.E))
                    {
                       WeaponManager.Instance.PickupWeapon(objectHitByRaycast.gameObject);
                    }
                }
                else
                {
                    if (hoveredWeapon)
                    {
                         hoveredWeapon.GetComponent<Outline>().enabled = false;
                    }
                   
                }
                //AmmoBox interaction
                if(objectHitByRaycast.GetComponent<AmmoBox>())
                {
                    hoveredAmmoBox = objectHitByRaycast.gameObject.GetComponent<AmmoBox>();
                    hoveredAmmoBox.GetComponent<Outline>().enabled = true;
                    
                    if(Input.GetKey(KeyCode.E))
                    {
                       WeaponManager.Instance.PickupAmmo(hoveredAmmoBox);
                       Destroy(objectHitByRaycast.gameObject);
                    }
                }
                else
                {
                    if (hoveredAmmoBox)
                    {
                         hoveredAmmoBox.GetComponent<Outline>().enabled = false;
                    }
                   
                }
            }
        }
   }
}
