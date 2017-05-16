using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using static It_is_a_scary_world.DIRECTION;

namespace It_is_a_scary_world
{
    class Movement : IStrategy
    {
        private Transform transform;

        public static Vector2 startPos;

        private Animator animator;

        private GameObject go;

        public static DIRECTION direction { get; private set; }

        public Movement(Transform transform, Animator animator, GameObject gameObject)
        {
            this.transform = transform;
            this.animator = animator;
            this.go = gameObject;
        }

        public void Execute(ref DIRECTION currentDirection)
        {
            KeyboardState keyState = Keyboard.GetState();

            Vector2 translation = Vector2.Zero;

            if (keyState.IsKeyDown(Keys.W) && (go.GetComponent("Gravity") as Gravity).isFalling == false)
            {
                (go.GetComponent("Gravity") as Gravity).grounded = false;
                startPos = go.transform.position;
                if ((go.GetComponent("Gravity") as Gravity).collidingObject.gameObject.Tag == "Platform")
                {
                    translation += new Vector2(0, -1);
                    (go.GetComponent("Gravity") as Gravity).velocity = new Vector2(0, -500);
                }
            }
            /*
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

            transform.Translate(translation * (go.GetComponent("Player") as Player).movementSpeed * GameWorld.Instance.deltaTime);

            animator.PlayAnimation("Walk" + currentDirection);
        }
    }
}
