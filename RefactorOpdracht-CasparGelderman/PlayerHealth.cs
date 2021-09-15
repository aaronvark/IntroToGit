public class PlayerHealth : Player, IDamageable
{
    private static int health { get; set; } = 1;

    public void TakeDamage(int _amount)
    {
        health -= _amount;
    }

    public void Heal(int _amount)
    {
        health += _amount;
    }
}
