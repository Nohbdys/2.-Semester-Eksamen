using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace It_is_a_scary_world
{
    class Database
    {
        SQLiteConnection dbConn = new SQLiteConnection("Data Source=data.db;Version=3;");

        public void databaseSetup()
        {//insert into hold values (null,  'hold 1', 1, 1, 0, 0, 0, 0, 0);

            using (var dbConn = new SQLiteConnection("Data Source = data.db; Version = 3;"))
            {
                dbConn.Open();
                //string sql = "drop table hold;drop table turneringer; drop table tmp_hold; drop table turn_hold";
                string sql = "create table if not exists upgrades (upgradeid integer primary key, Armor int, Weapondmg int, atkSpeed int, playerSpeed int);create table if not exists player (player_id integer primary key, currentArmor int, currency int, experience int, level int, upgrade_id int, FOREIGN KEY(upgrade_id) REFERENCES upgrades(upgradeid));";

                using (SQLiteCommand command = new SQLiteCommand(sql, dbConn))
                {
                    SQLiteDataReader reader = command.ExecuteReader();
                }
                /*
                // Resetter tmp_hold og kopier hold over i tmp_hold
                sql = "delete from tmp_hold";
                using (SQLiteCommand command = new SQLiteCommand(sql, dbConn))
                {
                    SQLiteDataReader reader = command.ExecuteReader();

                }

                sql = "insert into tmp_hold select * from hold;";
                using (SQLiteCommand command = new SQLiteCommand(sql, dbConn))
                {
                    SQLiteDataReader reader = command.ExecuteReader();

                }
                */

            }


            //dbConn.Close();
        }
        public void databaseTurnSave()
        {
            using (var dbConn = new SQLiteConnection("Data Source = data.db; Version = 3; "))
            {
                dbConn.Open();

            }
            //dbConn.Close();
        }
    }
}
