using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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
            if(Health < 0)
            {
                Die();
            }
        }

    }
}
