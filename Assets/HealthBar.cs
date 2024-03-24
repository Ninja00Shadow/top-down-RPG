using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public int health = 3;
    public Image[] hearts;
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
}
