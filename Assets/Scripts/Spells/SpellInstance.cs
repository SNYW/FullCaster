public class SpellInstance
{
    public int level;
    public float coolDown;
    public float remainingCooldown;
    public Spell spell;

    public void Init(Spell spell)
    {
        level = 0;
        coolDown = spell.coolDown;
        this.spell = spell;
    }

    public void Cast()
    {
        if (spell.Cast(level))
        {
            remainingCooldown = spell.coolDown;
        }
    }
}
