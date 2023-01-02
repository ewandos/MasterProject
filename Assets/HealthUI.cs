using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public TextMeshProUGUI healthUI;

    public void Initialize(PlayerHealth playerHealth)
    {
        healthUI.text = playerHealth._health.ToString();
        playerHealth.updatedHealthEvent += i => healthUI.text = i.ToString();
    }
}
