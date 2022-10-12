using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int currentBitCoins;
    public static GameManager instance;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UIManager.instance.UpdateBitcoinText(currentBitCoins);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GetBitCoins(20);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            SpendBitCoins(10);
        }

        Debug.Log("I have" + currentBitCoins + "bitcoins");
    }

    public void GetBitCoins(int amountToGet)
    {
        currentBitCoins += amountToGet;
        UIManager.instance.UpdateBitcoinText(currentBitCoins);
    }

    public void SpendBitCoins(int amountToSpend)
    {
        currentBitCoins -= amountToSpend;
        if (currentBitCoins <= 0)
        {
            currentBitCoins = 0;
        }
        UIManager.instance.UpdateBitcoinText(currentBitCoins);
    }
}
