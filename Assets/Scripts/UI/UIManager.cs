using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] Image imageToFade;

    [SerializeField] Image weaponImage;
    [SerializeField] Text weaponName;

    public Slider healthSlider;
    public TextMeshProUGUI healthText;

    [SerializeField] GameObject deathScreen;

    [SerializeField] TextMeshProUGUI btcText;

    private void Awake()
    {
        instance = this;
    }

    public void FadeImage()
    {
        imageToFade.GetComponent<Animator>().SetTrigger("Start Fade");
    }

    public void ChangeWeaponUI(Sprite gunImage, string gunText)
    {
        weaponImage.sprite = gunImage;
        weaponName.text = gunText;
    }

    public void TurnDeathScreenOn()
    {
        deathScreen.SetActive(true);        
    }

    public void UpdateBitcoinText(int amountOfBTC)
    {
        btcText.text = amountOfBTC.ToString();
    }
}
