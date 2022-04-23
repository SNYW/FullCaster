using UnityEngine;

public class Mage : MonoBehaviour
{
    public int baseHealth;
    public int currentHealth;
    public float range;
    public Enemy target;
    public Transform projectileAnchor;
    public Transform aoeAnchor;

    public void PlayCastAnim()
    {
        GetComponent<Animator>().Play("Attack");
    }
}
