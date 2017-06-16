using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using static It_is_a_scary_world.DIRECTION;
using System.Threading;


namespace It_is_a_scary_world
{
    class Ghost : Component, IUpdateable, ICollisionStay
    {
        private DIRECTION direction;

        private bool isDead;

        private bool activeThread;

        private bool firstRun = false;

        private Animator animator;

        private GameObject player;

        private IStrategy strategy;

        private int platformTimer;

        private GameObject go;

        #region Stats

        public float health = 100;
        public float damage = 1;
        public int expDrop = 10;
        public int goldDrop = 10;

        private int dropChance;

        #endregion

        Random rnd = new Random();

        public Ghost(GameObject gameObject) : base(gameObject)
        {
            this.go = gameObject;
            gameObject.Tag = "Enemy";
        }
        public void LoadContent(ContentManager content)
        {
            player = GameWorld.Instance.FindGameObjectWithTag("Player");

            animator = (Animator)gameObject.GetComponent("Animator");

            Texture2D sprite = content.Load<Texture2D>("GhostSheet");

            direction = Front;

            //Adds the enemys animations
            animator.CreateAnimation("IdleFront", new Animation(1, 0, 0, 25, 32, 5, Vector2.Zero, sprite));
            animator.CreateAnimation("IdleBack", new Animation(1, 32, 0, 25, 32, 5, Vector2.Zero, sprite));
            animator.CreateAnimation("IdleLeft", new Animation(1, 0, 0, 25, 32, 5, Vector2.Zero, sprite));
            animator.CreateAnimation("IdleRight", new Animation(1, 32, 0, 25, 32, 5, Vector2.Zero, sprite));
            animator.CreateAnimation("WalkFront", new Animation(1, 0, 0, 25, 32, 5, Vector2.Zero, sprite));
            animator.CreateAnimation("WalkBack", new Animation(1, 32, 0, 25, 32, 5, Vector2.Zero, sprite));
            animator.CreateAnimation("WalkLeft", new Animation(1, 0, 0, 25, 32, 5, Vector2.Zero, sprite));
            animator.CreateAnimation("WalkRight", new Animation(1, 32, 0, 25, 32, 5, Vector2.Zero, sprite));

            animator.CreateAnimation("Pause", new Animation(1, 0, 0, 25, 32, 0, Vector2.Zero, sprite));

            animator.PlayAnimation("WalkLeft");

            strategy = new Idle(animator);
        }

        public void Update()
        {

            if (!activeThread)
            {
                Thread t = new Thread(ThreadUpdate);

                if (health < 0)
                {
                    t.Abort();
                }

                t.IsBackground = true;

                t.Start();

                activeThread = true;

            }
        }

        private void ThreadUpdate()
        {

            while (!isDead)
            {

                if (GameWorld.Instance.currentGameState == GameState.InGame)
                {

                    Thread.Sleep(17);

                    #region Death
                    if (health <= 0 && isDead == false)
                    {
                        isDead = true;
                        go.transform.position = new Vector2(3500, 3500);
                        dropChance = rnd.Next(1, 3);

                        if (dropChance == 1)
                        {
                            foreach (GameObject go in GameWorld.Instance.gameObjects)
                            {
                                if (go.Tag == "Shop")
                                {
                                    (go.GetComponent("Shop") as Shop).gold += rnd.Next(25, 100);
                                    break;
                                }
                            }
                        }

                        foreach (GameObject go in GameWorld.Instance.gameObjects)
                        {
                            if (go.Tag == "Player")
                            {
                                (go.GetComponent("Player") as Player).exp += rnd.Next(25, 50);
                                break;
                            }

                        }
                        activeThread = false;

                        GameWorld.Instance.objectsToRemove.Add(gameObject);
                    }
                    #endregion

                    #region FollowTarget / idle

                    if (Vector2.Distance(gameObject.transform.position, player.transform.position) <= 2000 && !(strategy is GhostWalk))
                    {
                        strategy = new GhostWalk(player.transform, gameObject.transform, animator);
                    }

                    else if (Vector2.Distance(gameObject.transform.position, player.transform.position) <= 200 && !(strategy is Idle))
                    {
                        strategy = new Idle(animator);
                    }

                    strategy.Execute(ref direction);
                    #endregion

                }
                if (GameWorld.Instance.currentGameState == GameState.ShopMenu)
                {
                    Thread.Sleep(17);

                    strategy = new Pause(animator);

                    strategy.Execute(ref direction);
                }
            }
        }

        public void OnCollisionStay(Collider other)
        {
            if (other.gameObject.Tag == "Player")
            {
                if ((other.gameObject.GetComponent("Player") as Player).iFrameGhost == 0)
                {
                    go.transform.position = new Vector2(3500, 3500);

                    if ((other.gameObject.GetComponent("Player") as Player).armor <= 0)
                    {
                        (other.gameObject.GetComponent("Player") as Player).health -= 1;
                    }

                    if ((other.gameObject.GetComponent("Player") as Player).armor > 0)
                    {
                        (other.gameObject.GetComponent("Player") as Player).armor -= 1;
                    }

                    (other.gameObject.GetComponent("Player") as Player).iFrameGhost = 60;
                }
            }
        }
    }
}
