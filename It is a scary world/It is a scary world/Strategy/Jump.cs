using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            if (keyState.IsKeyDown(Keys.W) && isFalling == false)
            {
                startPos = go.transform.position;

                transform.Translate(translation * (go.GetComponent("Player") as Player).movementSpeed * GameWorld.Instance.deltaTime);
            }
        }
    }
}
