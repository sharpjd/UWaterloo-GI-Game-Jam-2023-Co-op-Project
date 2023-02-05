namespace Assets
{
    internal interface IHittable
    {

        int Health
        {
            get; set;
        }

        public void Die();

        public void OnDamage(int damage)
        {
            Health -= damage;
            if (Health < 0)
            {
                Die();
            }
        }

    }
}
