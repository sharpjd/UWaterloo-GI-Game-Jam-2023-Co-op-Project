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

        int Hitpoints
        {
            get; set;
        }

        public void Die();

        public void OnDamage(int damage)
        {
            Hitpoints -= damage;
            if(Hitpoints < 0)
            {
                Die();
            }
        }

    }
}
