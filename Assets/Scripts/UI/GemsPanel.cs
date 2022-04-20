using TMPro;
using UnityEngine;

public class GemsPanel : MonoBehaviour
{
    public TMP_Text gemsAmount;

    private void Update()
    {
        if (PlayerPrefs.HasKey("gems"))
        {
            gemsAmount.text = PlayerPrefs.GetInt("gems").ToString();
        }
        else
        {
            gemsAmount.text = "0";
        }
    }
}
