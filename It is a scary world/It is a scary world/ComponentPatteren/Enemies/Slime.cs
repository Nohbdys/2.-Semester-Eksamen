using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;
using static It_is_a_scary_world.DIRECTION;

namespace It_is_a_scary_world
{

    class Slime : Component, IUpdateable, ICollisionEnter, ICollisionExit
    {

        private DIRECTION direction;

        private Animator animator;

        bool activeThread = false;

        private GameObject player;

        private IStrategy strategy;

        public Slime(GameObject gameObject) : base(gameObject)
        {
            gameObject.Tag = "Enemy";

        }

        public void LoadContent(ContentManager content)
        {
            player = GameWorld.Instance.FindGameObjectWithTag("Player");

            animator = (Animator)gameObject.GetComponent("Animator");

            Texture2D sprite = content.Load<Texture2D>("SlimeSheet");

            direction = Front;

            //Adds the enemys animations
            animator.CreateAnimation("IdleFront", new Animation(1, 0, 0, 100, 100, 0, Vector2.Zero, sprite));
            animator.CreateAnimation("IdleLeft", new Animation(1, 0, 1, 100, 100, 0, Vector2.Zero, sprite));
            animator.CreateAnimation("IdleRight", new Animation(1, 0, 2, 100, 100, 0, Vector2.Zero, sprite));
            animator.CreateAnimation("IdleBack", new Animation(1, 0, 3, 100, 100, 0, Vector2.Zero, sprite));
            animator.CreateAnimation("WalkFront", new Animation(4, 100, 0, 100, 100, 5, Vector2.Zero, sprite));
            animator.CreateAnimation("WalkBack", new Animation(4, 100, 4, 100, 100, 5, Vector2.Zero, sprite));
            animator.CreateAnimation("WalkLeft", new Animation(4, 200, 0, 100, 100, 5, Vector2.Zero, sprite));
            animator.CreateAnimation("WalkRight", new Animation(4, 200, 4, 100, 100, 5, Vector2.Zero, sprite));
            animator.CreateAnimation("DieBack", new Animation(4, 300, 0, 100, 100, 5, Vector2.Zero, sprite));
            animator.CreateAnimation("DieFront", new Animation(4, 300, 4, 100, 100, 1, Vector2.Zero, sprite));
            animator.CreateAnimation("DieLeft", new Animation(4, 400, 0, 100, 100, 5, Vector2.Zero, sprite));
            animator.CreateAnimation("DieRight", new Animation(4, 400, 4, 100, 100, 5, Vector2.Zero, sprite));

            animator.PlayAnimation("DieFront");

            strategy = new Idle(animator);
        }

        public void Update()
        {
            if (!activeThread)
            {
                Thread t = new Thread(ThreadUpdate);
                t.IsBackground = true;



                t.Start();

                activeThread = true;

            }

        }
        public void ThreadUpdate()
        {
            while (true)
            {
                Thread.Sleep(17);

                if (Vector2.Distance(gameObject.transform.position, player.transform.position) <= 200 && !(strategy is FollowTarget))
                {
                    strategy = new FollowTarget(player.transform, gameObject.transform, animator);
                }
                else if (Vector2.Distance(gameObject.transform.position, player.transform.position) > 200 && !(strategy is Idle))
                {
                    strategy = new Idle(animator);
                }

                strategy.Execute(ref direction);
            }
        }

        public void OnCollisionExit(Collider other)
        {
            if (other.gameObject.Tag == "Player")
            {
                (other.gameObject.GetComponent("SpriteRenderer") as SpriteRenderer).Color = Color.White;
            }
        }

        public void OnCollisionEnter(Collider other)
        {
            if (other.gameObject.Tag == "Player")
            {
                (other.gameObject.GetComponent("SpriteRenderer") as SpriteRenderer).Color = Color.Red;
                GameWorld.Instance.objectsToRemove.Add(gameObject);
            }
            if (other.gameObject.Tag == "Bullet")
            {
                (other.gameObject.GetComponent("SpriteRenderer") as SpriteRenderer).Color = Color.Red;
                GameWorld.Instance.objectsToRemove.Add(gameObject);
            }
        }
    }
}
