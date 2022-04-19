using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Debug_FPSText : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(SetText());
    }
    private IEnumerator SetText()
    {
        while (true)
        {
            GetComponent<TMP_Text>().text = $"FPS: {(int)(1.0f / Time.deltaTime)}";
            yield return new WaitForSeconds(1f);
        }
    }
}
