using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace It_is_a_scary_world
{
    interface IBuilder
    {
        GameObject GetResult();

        void BuildGameObject(Vector2 position);
    }
}
