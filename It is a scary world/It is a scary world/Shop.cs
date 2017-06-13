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
        public double gold = 1000;

        public int weaponDamageLevel = 1;
        private int weaponAttackSpeedLevel = 1;
        private int playerArmorLevel = 1;
        private int playerSpeedLevel = 1;

        public double weaponDamagePrice = 100;
        public double weaponAttackSpeedPrice = 100;
        public double playerArmorPrice = 100;
        public double playerSpeedPrice = 100;

        public bool weaponDamagePriceUp;
        public bool weaponAttackSpeedPriceUp;
        public bool playerArmorPriceUp;
        public bool playerSpeedPriceUp;

        public bool weaponDamageUpgrade;
        public bool weaponAttackSpeedUpgrade;
        public bool playerArmorUpgrade;
        public bool playerSpeedUpgrade;

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

            animator.CreateAnimation("IdleFront", new Animation(1, 0, 0, 165, 190, 0, Vector2.Zero, sprite));

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
                }
            }

            if (gold >= weaponAttackSpeedPrice)
            {
                weaponAttackSpeedUpgrade = true;

                if (weaponAttackSpeedPriceUp == true)
                {
                    gold -= weaponAttackSpeedPrice;
                    weaponAttackSpeedLevel += 1;
                    weaponAttackSpeedUpgrade = false;
                    weaponAttackSpeedPrice += (int)Math.Ceiling((weaponAttackSpeedPrice * 1.25));
                    weaponAttackSpeedPriceUp = false;
                }
            }

            if (gold >= playerArmorPrice)
            {
                playerArmorUpgrade = true;

                if (playerArmorPriceUp == true)
                {
                    gold -= playerArmorPrice;
                    playerArmorLevel += 1;
                    playerArmorUpgrade = false;
                    playerArmorPrice += (int)Math.Ceiling((playerArmorPrice * 1.25));
                    playerArmorPriceUp = false;
                }
            }

            if (gold >= playerSpeedPrice)
            {
                playerSpeedUpgrade = true;

                if (playerSpeedPriceUp == true)
                {
                    gold -= playerSpeedPrice;
                    playerSpeedLevel += 1;
                    playerSpeedUpgrade = false;
                    playerSpeedPrice += (int)Math.Ceiling((playerSpeedPrice * 1.25));
                    playerSpeedPriceUp = false;
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
