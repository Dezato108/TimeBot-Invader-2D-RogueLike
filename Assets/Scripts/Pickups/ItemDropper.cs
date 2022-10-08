using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropper : MonoBehaviour
{

    [SerializeField] bool dropsItem;
    [SerializeField] GameObject[] itemsToDrop;
    [SerializeField] float itemDropchange = 0.5f;
    
    public void DropItem()
    {
        if (dropsItem)
        {
            if (Random.value < itemDropchange)
            {
                int randomItemNumber = Random.Range(0, itemsToDrop.Length);

                Instantiate(itemsToDrop[randomItemNumber], transform.position, transform.rotation);
            }
        }
    }

    public bool IsItemDropper()
    {
        return dropsItem;
    }
}
