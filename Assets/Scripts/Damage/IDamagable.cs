namespace NPLTV.Damage
{
    public interface IDamagable 
    {
        void Damage(float damageAmount);
        void Heal(float healAmount);
    }
}