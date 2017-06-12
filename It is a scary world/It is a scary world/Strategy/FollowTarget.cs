using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static It_is_a_scary_world.DIRECTION;

namespace It_is_a_scary_world
{
    class FollowTarget : IStrategy
    {
        private Transform target;
        private Transform transform;
        private float movementSpeed = 50;
        private Animator animator;

        public FollowTarget(Transform target, Transform transform, Animator animator)
        {
            this.target = target;
            this.transform = transform;
            this.animator = animator;
        }

        public void Execute(ref DIRECTION currentDirection)
        {
            Vector2 translation = Vector2.Zero;

            //Enemy followtarget Y-axis
            /*
            if (target.position.Y <= transform.position.Y)
            {
                translation += new Vector2(0, -1);
                currentDirection = Back;
            }

            if (target.position.Y >= transform.position.Y)
            {
                translation += new Vector2(0, 1);
                currentDirection = Front;
            }
            */

            //Enemy followtarget x-axis
            if (target.position.X <= transform.position.X)
            {
                translation += new Vector2(-1.5f, 0);
                currentDirection = Left;
            }

            if (target.position.X >= transform.position.X)
            {
                translation += new Vector2(1.5f, 0);
                currentDirection = Right;
            }

            transform.Translate(translation * movementSpeed * GameWorld.Instance.deltaTime);

            animator.PlayAnimation("Walk" + currentDirection);
        }
    }
}
