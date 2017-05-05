using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace It_is_a_scary_world
{
    class Idle : IStrategy
    {
        private Animator animator;

        public Idle(Animator animator)
        {
            this.animator = animator;
        }

        public void Execute(ref DIRECTION currentDirection)
        {
            animator.PlayAnimation("Idle" + currentDirection);
        }
    }
}
