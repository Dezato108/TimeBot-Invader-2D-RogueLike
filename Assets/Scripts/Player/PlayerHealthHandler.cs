using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthHandler : MonoBehaviour
{
    [SerializeField] int currentHealth;
    [SerializeField] int maxHealth;

    [SerializeField] float invicibilityTime = 1f;
    private bool isInvincible;

    [SerializeField] SpriteRenderer playerSprite;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        UIManager.instance.healthSlider.maxValue = maxHealth;

        UpdatePlayerHealthUI();

        isInvincible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DamagePlayer(int amountOfDamage)
    {
        if (!isInvincible)
        {
            currentHealth -= amountOfDamage;
            UpdatePlayerHealthUI();

            MakePlayerInvincible();
            if (currentHealth <= 0)
            {
                UIManager.instance.TurnDeathScreenOn();
                AudioManager.instance.PlayGameOverMusic();
                gameObject.SetActive(false);
            }

            
        }
    }
    public void AddHpToPlayer(int healthAmount)
    {
        currentHealth += healthAmount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        UpdatePlayerHealthUI();
    }

    public void MakePlayerInvincible()
    {
        isInvincible = true;

        StartCoroutine(Flasher());
        StartCoroutine(PlayerNotInvincible());
    }

    public IEnumerator Flasher()
    {
        for (int i = 0; i < 5; i++)
        {
            playerSprite.color = new Color(
                playerSprite.color.r,
                playerSprite.color.g,
                playerSprite.color.b,
                0.1f
            );

            yield return new WaitForSeconds(.1f);

            playerSprite.color = new Color(
                playerSprite.color.r,
                playerSprite.color.g,
                playerSprite.color.b,
                1f
            );

            yield return new WaitForSeconds(.1f);
        }
    }

    IEnumerator PlayerNotInvincible()
    {
        yield return new WaitForSeconds(invicibilityTime);
        isInvincible = false;
    }

    private void UpdatePlayerHealthUI()
    {
        UIManager.instance.healthText.text = currentHealth + "/" + maxHealth;
        UIManager.instance.healthSlider.value = currentHealth;
    }

}
