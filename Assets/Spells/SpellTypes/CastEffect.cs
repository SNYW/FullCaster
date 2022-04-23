using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CastEffect : MonoBehaviour
{
    public float killTime;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, killTime);
    }
}
