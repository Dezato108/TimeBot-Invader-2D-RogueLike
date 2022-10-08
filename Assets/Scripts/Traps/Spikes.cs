using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] int damageAmount;
    private Collider2D playerToDamage;

    public bool shouldDamagePlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<Animator>().SetBool("Pop Spikes",true);
            playerToDamage = collision;
            shouldDamagePlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<Animator>().SetBool("Pop Spikes", false);
            shouldDamagePlayer = false;            
        }
    }

    public void DamagePlayer()
    {
        if (shouldDamagePlayer)
        {
            playerToDamage.GetComponent<PlayerHealthHandler>().DamagePlayer(damageAmount);
        }
    }
}
