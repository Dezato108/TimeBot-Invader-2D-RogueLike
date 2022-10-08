using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupDelayer : MonoBehaviour
{
    [SerializeField] float timeBeforePickup = 0.5f;
    private bool canbePickedUp;

    private void Start()
    {
        canbePickedUp = false;
        StartCoroutine(PickupCooldown());
    }

    IEnumerator PickupCooldown()
    {
        yield return new WaitForSeconds(timeBeforePickup);

        canbePickedUp = true;
    }

    public bool CanBePickedUpMethod() { return canbePickedUp; }
}
