using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] int coinAmount = 10;

    [SerializeField] int sfxToPlay;
    private bool pickedUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !pickedUp && GetComponent<PickupDelayer>().CanBePickedUpMethod())
        {
            pickedUp = true;

            GameManager.instance.GetBitCoins(coinAmount);
            AudioManager.instance.PlaySFX(sfxToPlay);

            Destroy(gameObject);
        }
    }
}
