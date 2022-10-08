using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakables : MonoBehaviour
{
    [SerializeField] GameObject[] brokenParts;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bool PlayerIsDashing = collision.gameObject.GetComponent<PlayerController>().PlayerIsDashing();
            if (PlayerIsDashing)
            {
                GetComponent<Animator>().SetTrigger("Break");

                for (int i=0;i<brokenParts.Length; i++)
                {
                    int randomBrokenPart = Random.Range(0, brokenParts.Length);
                    int randomRotation = Random.Range(0, 4);
                    Instantiate(brokenParts[randomBrokenPart], transform.position, Quaternion.Euler(0f, 0f, 90f * randomRotation));
                }
                if (GetComponent<ItemDropper>() != null)
                {
                    if (GetComponent<ItemDropper>().IsItemDropper())
                    {
                        GetComponent<ItemDropper>().DropItem();
                    }
                }


            }
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
