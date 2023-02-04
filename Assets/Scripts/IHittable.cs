using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets
{
    internal interface IHittable
    {

        float Hitpoints
        {
            get; set;
        }

        void OnDeath()
        {

        }

        public void OnHit(float damage)
        {
            Hitpoints -= damage;
            if(Hitpoints < 0)
            {
                OnDeath();
            }
        }

    }
}
