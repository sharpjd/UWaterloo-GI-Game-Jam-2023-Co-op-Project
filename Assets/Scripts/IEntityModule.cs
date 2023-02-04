using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets
{

    /// <summary>
    /// Use this for any scripts that need visibility to and from an <see cref="Entity"/>.
    /// Make sure you add the instance to an Entity's <see cref="Entity.entityModules"/> and 
    /// set this <see cref="ParentEntity"/> to it
    /// </summary>
    internal interface IEntityModule
    {
        Entity ParentEntity { get; set; }
    }

}
