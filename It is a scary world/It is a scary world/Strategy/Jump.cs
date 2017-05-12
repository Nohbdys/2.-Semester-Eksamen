using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static It_is_a_scary_world.DIRECTION;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace It_is_a_scary_world
{
    class Jump : IStrategy
    {
        private GameObject go;

        public static Vector2 startPos;

        private Transform transform;

        private Animator animator;

        private bool isFalling;

        private Vector2 velocity;

        public static DIRECTION direction { get; private set; }

        public Jump(Transform transform, Animator animator, GameObject gameObject)
        {
            this.transform = transform;
            this.animator = animator;
            this.go = gameObject;
        }

        public void Execute(ref DIRECTION currentDirection)
        {
            KeyboardState keyState = Keyboard.GetState();

            Vector2 translation = Vector2.Zero;

            isFalling = (go.GetComponent("Gravity") as Gravity).isFalling;

            velocity = (go.GetComponent("Gravity") as Gravity).velocity;

            if (keyState.IsKeyDown(Keys.W) && isFalling == false)
            {
                (go.GetComponent("Gravity") as Gravity).grounded = false;
                startPos = go.transform.position;
                if ((go.GetComponent("Gravity") as Gravity).collidingObject.gameObject.Tag == "Platform")
                {
                    translation += new Vector2(0, -1);
                    (go.GetComponent("Gravity") as Gravity).velocity = new Vector2(0, -500);
                }

                direction = currentDirection;
                transform.Translate(translation * (go.GetComponent("Player") as Player).movementSpeed * GameWorld.Instance.deltaTime);

                animator.PlayAnimation("Walk" + currentDirection);
            }
        }
    }
}
