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
    class Projectiles : Component, IUpdateable, ICollisionStay, ICollisionEnter
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

            Texture2D sprite = content.Load<Texture2D>("BulletTest");

            animator.CreateAnimation("IdleFront", new Animation(1, 0, 0, 25, 25, 0, Vector2.Zero, sprite));

            animator.PlayAnimation("IdleFront");

            strategy = new Idle(animator);
        }

        public void Update()
        {

            gameObject.transform.position += direction * 4; //The bullet moves towards the mouse's current position

        }
        public void OnCollisionStay(Collider other)
        {
            if (other.gameObject.Tag == "Enemy" || other.gameObject.Tag == "Wall" || other.gameObject.Tag == "Platfrom")
            {
                GameWorld.Instance.objectsToRemove.Add(gameObject);              
            }
        }

        public void OnCollisionEnter(Collider other)
        {
            if (other.gameObject.Tag == "Enemy")
            {
                (other.gameObject.GetComponent("Slime") as Slime).health -= (this.gameObject.GetComponent("Player") as Player).damage;
            }

        }
    }
}
