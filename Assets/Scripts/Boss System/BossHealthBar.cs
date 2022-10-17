using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] Slider bossHealthSlider;
    // Start is called before the first frame update
    void Start()
    {
        bossHealthSlider.maxValue = FindObjectOfType<BossHealthHandler>().GetBossMaxHealth();
    }

    // Update is called once per frame
    void Update()
    {
        bossHealthSlider.value = FindObjectOfType<BossHealthHandler>().GetBossCurrentHealth();

        if (bossHealthSlider.value <= 0)
        {
            Destroy(gameObject);
        }
    }
}
