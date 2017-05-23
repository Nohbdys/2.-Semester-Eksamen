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

        private bool doubleJump = false;
        private int maxJump = 2;
        private int currentJump;

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

            if (keyState.IsKeyDown(Keys.W) && maxJump >= currentJump /*(go.GetComponent("Gravity") as Gravity).grounded == true*/)
            {
                
                (go.GetComponent("Gravity") as Gravity).grounded = false;
                startPos = go.transform.position;
                translation += new Vector2(0, -1);
                (go.GetComponent("Gravity") as Gravity).velocity = new Vector2(0, -500);
                currentJump += 1;
            }
            /*
            if (keyState.IsKeyDown(Keys.W) && firstJump == true && doubleJump == true && (go.GetComponent("Player") as Player).currentJump <= maxJump && (go.GetComponent("Gravity") as Gravity).grounded == false)
            {
                startPos = go.transform.position;
                translation += new Vector2(0, -1);
                (go.GetComponent("Gravity") as Gravity).velocity = new Vector2(0, -500);
                (go.GetComponent("Player") as Player).currentJump += 1;
            }
            */

            if (keyState.IsKeyDown(Keys.A) && (go.GetComponent("Player") as Player).rightWallCollision == false)
            {
                translation += new Vector2(-1, 0);
                currentDirection = Left;
            }
            if (keyState.IsKeyDown(Keys.D) && (go.GetComponent("Player") as Player).leftWallCollision == false)
            {
                translation += new Vector2(1, 0);
                currentDirection = Right;
            }

            transform.Translate(translation * (go.GetComponent("Player") as Player).movementSpeed * GameWorld.Instance.deltaTime);

            animator.PlayAnimation("Walk" + currentDirection);
        }
    }
}
