using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopItem : MonoBehaviour
{
    [SerializeField] Canvas canvasMessage;
    private bool inBuyZone = false;

    enum ItemType { healthRestore, healthUpgrade, weapon}
    [SerializeField] ItemType itemType;

    [SerializeField] int itemCost;

    [SerializeField] WeaponsSystem[] thePotentialWeaponToBuy;
    private WeaponsSystem weaponToBuy;

    [SerializeField] SpriteRenderer weaponSpriteRenderer;
    [SerializeField] TMPro.TextMeshProUGUI priceText;

    private void Start()
    {
        if( itemType== ItemType.weapon)
        {
            int selectedWeapon = Random.Range(0, thePotentialWeaponToBuy.Length);
            weaponToBuy = thePotentialWeaponToBuy[selectedWeapon];

            itemCost = weaponToBuy.GetWeaponPrice();
            weaponSpriteRenderer.sprite = weaponToBuy.GetWeaponShopSprite();

            priceText.text = "Buy" + weaponToBuy.GetWeaponName() + ": " + itemCost + "coins";
        }
    }

    private void Update()
    {
        if (inBuyZone)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (GameManager.instance.GetCurrentBitcoins() >= itemCost)
                {
                    GameManager.instance.SpendBitCoins(itemCost);
                    switch (itemType)
                    {
                        case ItemType.healthRestore:
                            FindObjectOfType<PlayerHealthHandler>().AddHpToPlayer(10);
                            break;
                        case ItemType.healthUpgrade:
                            FindObjectOfType<PlayerHealthHandler>().IncreaseMaxHealth(10);
                            break;
                        case ItemType.weapon:
                            PlayerController playerBuying = FindObjectOfType<PlayerController>();
                            WeaponsSystem weaponToAdd = Instantiate(weaponToBuy, playerBuying.GetWeaponsArm());
                            playerBuying.AddToAvailableWeapons(weaponToAdd);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canvasMessage.gameObject.SetActive(true);
        inBuyZone = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canvasMessage.gameObject.SetActive(false);
        inBuyZone = false;
    }
}
