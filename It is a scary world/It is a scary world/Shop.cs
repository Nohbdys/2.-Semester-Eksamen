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
    class Shop : Component, IUpdateable, ILoadable
    {
        //Make them to players stats
        private double gold = 1000;

        private int weaponDamageLevel = 1;
        private int weaponAttackSpeedLevel = 1;
        private int playerHealthLevel = 1;
        private int playerSpeedLevel = 1;

        public double weaponDamagePrice = 100;
        public double weaponAttackSpeedPrice = 100;
        public double playerHealthPrice = 100;
        public double playerSpeedPrice = 100;

        public bool weaponDamagePriceUp;
        public bool weaponAttackSpeedPriceUp;
        public bool playerHealthPriceUp;
        public bool playerSpeedPriceUp;

        public bool weaponDamageUpgrade;
        public bool weaponAttackSpeedUpgrade;
        public bool playerHealthUpgrade;
        public bool playerSpeedUpgrade;


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

            Texture2D sprite = content.Load<Texture2D>("Katana");

            animator.CreateAnimation("IdleFront", new Animation(1, 0, 0, 10, 10, 0, Vector2.Zero, sprite));

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
                    weaponDamagePrice += weaponDamagePrice * 1.25;
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
                    weaponAttackSpeedPrice += weaponAttackSpeedPrice * 1.25;
                    weaponAttackSpeedPriceUp = false;
                }
            }

            if (gold >= playerHealthPrice)
            {
                playerHealthUpgrade = true;

                if (playerHealthPriceUp == true)
                {
                    gold -= playerHealthPrice;
                    playerHealthLevel += 1;
                    playerHealthUpgrade = false;
                    playerHealthPrice += playerHealthPrice * 1.25;
                    playerHealthPriceUp = false;
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
                    playerSpeedPrice += playerSpeedPrice * 1.25;
                    playerSpeedPriceUp = false;
                }
            }
        }
    }
}
