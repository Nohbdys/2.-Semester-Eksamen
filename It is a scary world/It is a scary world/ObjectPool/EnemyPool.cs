﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace It_is_a_scary_world
{
    class EnemyPool
    {
        private static Random rnd = new Random();

        private static List<GameObject> inactive = new List<GameObject>();

        private static List<GameObject> active = new List<GameObject>();

        private static Director director1 = new Director(new EnemyBuilder());

        private static Director director2 = new Director(new GhostBuilder());

        public static GameObject Create(Vector2 position, ContentManager content)
        {
            
            if (inactive.Count > 0)
            {
                GameObject enemy = inactive[0];
                active.Add(enemy);
                inactive.RemoveAt(0);

                return enemy;
            }
            else
            {
                if (rnd.Next(0, 2) == 1)
                {
                    GameObject enemy = director1.Construct(position);

                    (enemy.GetComponent("Skeleton") as Skeleton).LoadContent(content);

                    enemy.LoadContent(content);

                    active.Add(enemy);

                    return enemy;
                }
                else
                {
                    GameObject enemy = director2.Construct(position);

                    (enemy.GetComponent("Ghost") as Ghost).LoadContent(content);

                    enemy.LoadContent(content);

                    active.Add(enemy);

                    return enemy;
                }
                
            }
        }

        public static void ReleaseObject(GameObject enemy)
        {
            CleanUp();

            inactive.Add(enemy);

            active.Remove(enemy);
        }

        private static void CleanUp()
        {
            //Reset data, remove references etc.
        }
    }
}
