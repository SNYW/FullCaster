using System.Collections;
using UnityEngine;

public class Mage : MonoBehaviour
{
    public int baseHealth;
    public int currentHealth;
    public float range;
    public Enemy target;
    public Transform projectileAnchor;

    public void PlayCastAnim()
    {
        GetComponent<Animator>().Play("Attack");
    }
}
