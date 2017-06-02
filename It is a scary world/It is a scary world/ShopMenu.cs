using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace It_is_a_scary_world
{
    public class ShopMenu
    {
        public static double gold = 1000;

        public static int weaponDamageLevel = 1;
        public static int weaponAttackSpeedLevel = 1;
        public static int playerHealthLevel = 1;
        public static int playerSpeedLevel = 1;

        public static double weaponDamagePrice = 100;
        public static double weaponAttackSpeedPrice = 100;
        public static double playerHealthPrice = 100;
        public static double playerSpeedPrice = 100;

        public static bool weaponDamagePriceUp;
        public static bool weaponAttackSpeedPriceUp;
        public static bool playerHealthPriceUp;
        public static bool playerSpeedPriceUp;

        public static bool weaponDamageUpgrade;
        public static bool weaponAttackSpeedUpgrade;
        public static bool playerHealthUpgrade;
        public static bool playerSpeedUpgrade;

        public static void ShopUpgradeCheck()
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
