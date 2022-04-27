using UnityEngine;

public class SecondarySpellEffect : MonoBehaviour
{
    public Enemy enemy;
    public GameObject particleEffect;
    public Vector3 anchorPos;
    public ChainSpell.SecondaryEffects chainTarget;

    public void Init(Enemy hitEnemy, Vector3 hitPos)
    {
        enemy = hitEnemy;
        anchorPos = hitPos;
        OnInit();
    }

    protected virtual void OnInit() { SpellManager.Instance.secondaryEffects.Add(this); }

}
