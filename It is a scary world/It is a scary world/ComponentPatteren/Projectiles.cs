using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace It_is_a_scary_world
{
    class Projectiles : Component, IUpdateable, ICollisionStay
    {
        private Animator animator;

        private GameObject player;

        private IStrategy strategy;

        Vector2 direction;

        public Projectiles(GameObject gameObject) : base(gameObject)
        {
            MouseState current_mouse = Mouse.GetState();

            int mouseX = current_mouse.X;
            int mouseY = current_mouse.Y;

            direction = new Vector2(mouseX, mouseY) - gameObject.transform.position; //The bullet is shot in the direction of the mouse's current position
            direction.Normalize();

            gameObject.Tag = "Bullet";
        }

        public void LoadContent(ContentManager content)
        {

            player = GameWorld.Instance.FindGameObjectWithTag("Player");

            animator = (Animator)gameObject.GetComponent("Animator");

            Texture2D sprite = content.Load<Texture2D>("BulletTest");

            animator.CreateAnimation("IdleFront", new Animation(1, 0, 0, 25, 25, 0, Vector2.Zero, sprite));

            animator.PlayAnimation("IdleFront");

            strategy = new Idle(animator);
        }

        public void Update()
        {

            gameObject.transform.position += direction * 2; //The bullet moves towards the mouse's current position

        }
        public void OnCollisionStay(Collider other)
        {
            if (other.gameObject.Tag == "Enemy" /*|| other.gameObject.Tag == "Wall"*/)
            {
                GameWorld.Instance.objectsToRemove.Add(gameObject);
            }
        }
    }
}
