using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsPickup : MonoBehaviour
{
    [SerializeField] WeaponsSystem weapon;
    private bool pickedUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.CompareTag("Player") && !pickedUp)
        {
            pickedUp = true;
            bool gunOnPlayer = false;

            foreach (WeaponsSystem weaponToCheck in collision.GetComponent<PlayerController>().GetAvailableWeaponsOnPlayer())
            {
                if (weapon.GetWeaponName() == weaponToCheck.GetWeaponName())
                {
                    gunOnPlayer = true;
                }
            }

            if (!gunOnPlayer)
            {
                WeaponsSystem weaponToAdd = Instantiate(weapon, collision.GetComponent<PlayerController>().GetWeaponsArm());
                collision.GetComponent<PlayerController>().AddToAvailableWeapons(weaponToAdd);
            }

            Destroy(gameObject);
        }
    }
}
