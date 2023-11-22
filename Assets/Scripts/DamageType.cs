public enum DamageType
{
    Physical,
    Magical,
    Pure
}

public class Damage
{
    private DamageType type;
    public DamageType Type { set { type = value; } get { return type; } }

    private float amount;
    public float Amount { set { amount = value; } get { return amount; } }

    public Damage(DamageType type, float amount)
    {
        this.type = type;
        this.amount = amount;
    }
}