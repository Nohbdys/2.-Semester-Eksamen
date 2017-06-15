﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace It_is_a_scary_world
{
    class Pause : IStrategy
    {
        private Animator animator;

        public Pause(Animator animator)
        {
            this.animator = animator;
        }

        public void Execute(ref DIRECTION currentDirection)
        {
            animator.PlayAnimation("Pause");
        }
    }
}