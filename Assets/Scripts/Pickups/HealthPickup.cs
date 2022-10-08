using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    [SerializeField] int healthAmount = 10;
    private bool pickedUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !pickedUp && GetComponent<PickupDelayer>().CanBePickedUpMethod())
        {
            pickedUp = true;
            collision.GetComponent<PlayerHealthHandler>().AddHpToPlayer(healthAmount);
            Destroy(gameObject);
        }
    }
}
