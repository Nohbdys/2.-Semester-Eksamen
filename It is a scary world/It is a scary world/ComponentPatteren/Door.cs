using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace It_is_a_scary_world
{
    class Door : Component
    {
        private Animator animator;
        private IStrategy strategy;

        public Door(GameObject gameObject) : base(gameObject)
        {
            gameObject.Tag = "Door";
        }

        public void LoadContent(ContentManager content)
        {
            animator = (Animator)gameObject.GetComponent("Animator");

            Texture2D sprite = content.Load<Texture2D>("Door");

            animator.CreateAnimation("IdleFront", new Animation(1, 0, 0, 49, 57, 0, Vector2.Zero, sprite));

            animator.PlayAnimation("IdleFront");

            strategy = new Idle(animator);
        }
    }
}
