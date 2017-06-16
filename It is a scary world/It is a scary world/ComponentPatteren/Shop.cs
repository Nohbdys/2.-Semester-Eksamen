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
    class Shop : Component, IUpdateable, ILoadable, ICollisionStay, ICollisionExit
    {
        //Make them to players stats
#if DEBUG == true
        public float gold = 10000;
#endif

#if DEBUG == false
        public float gold = 100;
#endif
        public int weaponDamageLevel = 1;
        private int weaponAttackRangeLevel = 1;
        private int playerArmorLevel = 1;
        private int playerSpeedLevel = 1;

        public float weaponDamagePrice = 100;
        public float weaponAttackRangePrice = 100;
        public float playerArmorPrice = 500;
        public float playerSpeedPrice = 100;

        public bool weaponDamagePriceUp;
        public bool weaponAttackRangePriceUp;
        public bool playerArmorPriceUp;
        public bool playerSpeedPriceUp;

        public bool weaponDamageUpgrade;
        public bool weaponAttackRangeUpgrade;
        public bool playerArmorUpgrade;
        public bool playerSpeedUpgrade;

        public int weaponAttackRangeBullet = 40;

        public bool shopActive = false;

        private IStrategy strategy;

        private GameObject go;
        private Transform transform;
        private Animator animator;

        public Shop(GameObject gameObject, Transform transform) : base(gameObject)
        {
            this.go = gameObject;
            this.transform = transform;
            gameObject.Tag = "Shop";
        }

        public void LoadContent(ContentManager content)
        {
            animator = (Animator)gameObject.GetComponent("Animator");

            Texture2D sprite = content.Load<Texture2D>("Shopkeeper");

            animator.CreateAnimation("IdleFront", new Animation(1, 0, 0, 89, 68, 0, Vector2.Zero, sprite));

            animator.PlayAnimation("IdleFront");

            strategy = new Idle(animator);
        }

        public void Update()
        {
            if (gold >= weaponDamagePrice)
            {
                weaponDamageUpgrade = true;

                if (weaponDamagePriceUp == true)
                {
                    gold -= weaponDamagePrice;
                    weaponDamageLevel += 1;
                    weaponDamageUpgrade = false;
                    weaponDamagePrice += (int)Math.Ceiling((weaponDamagePrice * 1.25));
                    weaponDamagePriceUp = false;

                    foreach (GameObject go in GameWorld.Instance.gameObjects)
                    {
                        if (go.Tag == "Player")
                        {
                            (go.GetComponent("Player") as Player).damage = ((weaponDamageLevel * 0.1f) + 1) * (go.GetComponent("Player") as Player).damage;
                            break;
                        }
                    }
                }
            }

            if (gold >= weaponAttackRangePrice)
            {
                weaponAttackRangeUpgrade = true;

                if (weaponAttackRangePriceUp == true && weaponAttackRangeLevel <= 9)
                {
                    gold -= weaponAttackRangePrice;
                    weaponAttackRangeLevel += 1;
                    weaponAttackRangeUpgrade = false;
                    weaponAttackRangePrice += (int)Math.Ceiling((weaponAttackRangePrice * 1.25));
                    weaponAttackRangePriceUp = false;
                    weaponAttackRangeBullet += 5;
                }
            }

            foreach (GameObject go in GameWorld.Instance.gameObjects)
            {
                if (go.Tag == "Player")
                {
                    if (gold >= playerArmorPrice && (go.GetComponent("Player") as Player).armor < (go.GetComponent("Player") as Player).maxArmor)
                    {
                        playerArmorUpgrade = true;

                        if (playerArmorPriceUp == true)
                        {
                            gold -= playerArmorPrice;
                            playerArmorLevel += 1;
                            playerArmorUpgrade = false;
                            playerArmorPriceUp = false;
                            (go.GetComponent("Player") as Player).armor += 1;
                            break;
                        }
                        break;
                    }
                }
            }
            

            if (gold >= playerSpeedPrice)
            {
                playerSpeedUpgrade = true;

                if (playerSpeedPriceUp == true && playerSpeedLevel < 4)
                {
                    gold -= playerSpeedPrice;
                    playerSpeedLevel += 1;
                    playerSpeedUpgrade = false;
                    playerSpeedPrice += (int)Math.Ceiling((playerSpeedPrice * 1.25));
                    playerSpeedPriceUp = false;
                    foreach(GameObject go in GameWorld.Instance.gameObjects)
                    {
                        if (go.Tag == "Player")
                        {
                            (go.GetComponent("Player") as Player).movementSpeed += 5;
                        }
                    }
                }
            }
        }

        public void OnCollisionEnter(Collider other)
        {
            if (other.gameObject.Tag == "Player")
            {
                shopActive = true;
            }
        }

        public void OnCollisionExit(Collider other)
        {
            if (other.gameObject.Tag == "Player")
            {
                shopActive = false;
            }
        }

        public void OnCollisionStay(Collider other)
        {
            if (other.gameObject.Tag == "Player")
            {
                shopActive = true;
            }
        }
    }
}
