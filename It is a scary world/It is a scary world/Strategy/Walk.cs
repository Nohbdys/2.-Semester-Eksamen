﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static It_is_a_scary_world.DIRECTION;

namespace It_is_a_scary_world
{
    class Walk : IStrategy
    {
        private float movementSpeed = 100;

        private Transform transform;

        private Animator animator;

        public Walk(Transform transform, Animator animator)
        {
            this.transform = transform;
            this.animator = animator;
        }

        public void Execute(ref DIRECTION currentDirection)
        {
            KeyboardState keyState = Keyboard.GetState();

            Vector2 translation = Vector2.Zero;

            /*
            if (keyState.IsKeyDown(Keys.W))
            {
                translation += new Vector2(0, -1);
                currentDirection = Back;
            }
            if (keyState.IsKeyDown(Keys.S))
            {
                translation += new Vector2(0, 1);
                currentDirection = Front;
            }
            */

            if (keyState.IsKeyDown(Keys.A))
            {
                translation += new Vector2(-1, 0);
                currentDirection = Left;
            }
            if (keyState.IsKeyDown(Keys.D))
            {
                translation += new Vector2(1, 0);
                currentDirection = Right;
            }

            transform.Translate(translation * movementSpeed * GameWorld.Instance.deltaTime);

            animator.PlayAnimation("Walk" + currentDirection);
        }
    }
}

