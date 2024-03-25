using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthMoneyBar : MonoBehaviour
{
    public int health = 3;
    public int coins = 0;
    
    public Image[] hearts;
    public TMP_Text moneyText;
    void Start()
    {
        UpdateHealth();
    }

    // Update is called once per frame
    public void UpdateHealth()
    {
        print(health);
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].color = i < health ? Color.white : Color.black;
        }
    }
    
    public void UpdateMoney(int money)
    {
        coins += money;
        moneyText.text = coins.ToString();
    }
}
