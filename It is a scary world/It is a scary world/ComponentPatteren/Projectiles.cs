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

        private GameObject go;

        private IStrategy strategy;

        Vector2 direction;

        public Projectiles(GameObject gameObject) : base(gameObject)
        {
            MouseState current_mouse = Mouse.GetState();

            int mouseX = current_mouse.X;
            int mouseY = current_mouse.Y;

            foreach (GameObject gol in GameWorld.Instance.gameObjects)
            {
                if (gol.Tag == "Player")
                {
                    direction = new Vector2(mouseX, mouseY) - (gol.GetComponent("Player") as Player).gameObject.transform.position; //The bullet is shot in the direction of the mouse's current position
                    break;
                }
            }
            direction.Normalize();

            gameObject.Tag = "Bullet";
        }

        public void LoadContent(ContentManager content)
        {

            go = GameWorld.Instance.FindGameObjectWithTag("Player");

            animator = (Animator)gameObject.GetComponent("Animator");


            Texture2D sprite = content.Load<Texture2D>("Fireball");

            animator.CreateAnimation("IdleFront", new Animation(4, 0, 0, 18, 11, 4, Vector2.Zero, sprite));

            animator.PlayAnimation("IdleFront");

            strategy = new Idle(animator);
        }

        public void Update()
        {

            gameObject.transform.position += direction * 4; //The bullet moves towards the mouse's current position

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
