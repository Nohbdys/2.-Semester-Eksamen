﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace It_is_a_scary_world
{
    interface IStrategy
    {
        void Execute(ref DIRECTION currentDirection);
    }
}
