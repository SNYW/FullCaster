using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    public Image expBar;
    public TMP_Text levelText;

    private float targetFill;
 
    private void Update()
    {
        levelText.text = GameManager.Instance.level.ToString();
        targetFill = GameManager.Instance.currentExp / GameManager.Instance.expForLevel;
        if (GameManager.Instance.playing)
        {
            if (expBar.fillAmount != targetFill)
                expBar.fillAmount = Mathf.Lerp(expBar.fillAmount, targetFill, 5 * Time.deltaTime);
        }
    }
}
