using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int Health;
    public Text HealthCount;

    private void Start()
    {
        HealthCount.text = $"Жизни: {Health}";
    }

    public void TakeDamage()
    {
        HealthCount.text = $"Жизни: {--Health}";

        if(Health==0)
        {
            FindObjectOfType<GameManager>().GameOver("Жизни не бесконечны!");
        }
    }
}
