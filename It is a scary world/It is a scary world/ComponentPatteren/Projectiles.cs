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

        private DIRECTION direction;

        private float damage;

        Vector2 directions;

        public int bulletTime;
        public int bulletRange = 40;

        public Projectiles(GameObject gameObject) : base(gameObject)
        {
            this.go = gameObject;
            MouseState current_mouse = Mouse.GetState();

            int mouseX = current_mouse.X;
            int mouseY = current_mouse.Y;

            foreach (GameObject gol in GameWorld.Instance.gameObjects)
            {
                if (gol.Tag == "Player")
                {
                    directions = new Vector2(mouseX, mouseY) - (gol.GetComponent("Player") as Player).gameObject.transform.position; //The bullet is shot in the direction of the mouse's current position
                    break;
                }
            }
            directions.Normalize();

            gameObject.Tag = "Bullet";
        }

        public void LoadContent(ContentManager content)
        {

            go = GameWorld.Instance.FindGameObjectWithTag("Player");

            animator = (Animator)gameObject.GetComponent("Animator");

            Texture2D sprite = content.Load<Texture2D>("Fireball");

            animator.CreateAnimation("IdleFront", new Animation(4, 0, 0, 18, 11, 4, Vector2.Zero, sprite));

            animator.CreateAnimation("Pause", new Animation(1, 0, 0, 18, 11, 0, Vector2.Zero, sprite));

            animator.PlayAnimation("IdleFront");

            strategy = new Idle(animator);
        }

        public void Update()
        {
            if (GameWorld.Instance.currentGameState == GameState.InGame)
            {
                strategy = new Idle(animator);

                bulletTime += 1;

                foreach (GameObject go in GameWorld.Instance.gameObjects)
                {
                    if (go.Tag == "Shop")
                    {
                        bulletRange = (go.GetComponent("Shop") as Shop).weaponAttackRangeBullet;
                    }
                }

                if (bulletTime > bulletRange)
                {
                    GameWorld.Instance.objectsToRemove.Add(gameObject);
                }
                foreach (GameObject go in GameWorld.Instance.gameObjects)
                {
                    if (go.Tag == "Player")
                    {
                        damage = (go.GetComponent("Player") as Player).damage;
                    }
                }
                gameObject.transform.position += directions * 4; //The bullet moves towards the mouse's current position

                strategy.Execute(ref direction);
            }
            if (GameWorld.Instance.currentGameState == GameState.ShopMenu)
            {
                strategy = new Pause(animator);

                strategy.Execute(ref direction);
            }
        }
        public void OnCollisionStay(Collider other)
        {
            
            if (other.gameObject.Tag == "Enemy" || other.gameObject.Tag == "Wall" || other.gameObject.Tag == "Platform")
            {
                GameWorld.Instance.objectsToRemove.Add(gameObject);              
            }
            
        }

        public void OnCollisionEnter(Collider other)
        {
            if (other.gameObject.Tag == "Enemy")
            {
                if (other.gameObject.GetComponent("Skeleton") is Skeleton)
                {
                    (other.gameObject.GetComponent("Skeleton") as Skeleton).health -= damage;
                }
                if (other.gameObject.GetComponent("Ghost") is Ghost)
                {
                    (other.gameObject.GetComponent("Ghost") as Ghost).health -= damage;
                }
            }
        }
    }
}
