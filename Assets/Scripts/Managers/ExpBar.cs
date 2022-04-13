using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    public Image expBar;

    private float targetFill;
 
    private void Update()
    {
        targetFill = GameManager.Instance.currentExp / GameManager.Instance.expForLevel;
        if (GameManager.Instance.playing)
        {
            if (expBar.fillAmount != targetFill)
                expBar.fillAmount = Mathf.Lerp(expBar.fillAmount, targetFill, 5 * Time.deltaTime);
        }
    }
}
