using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace It_is_a_scary_world
{
    interface ILoadable
    {
        void LoadContent(ContentManager content);
    }
}
