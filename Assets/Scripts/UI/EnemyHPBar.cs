using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{
    public Image healthBar;

    public void UpdateHealthBar(float current, float max)
    {
        healthBar.fillAmount = current / max;
    }
}
