using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;


namespace It_is_a_scary_world
{
    class BulletPool
    {
        
        private static List<GameObject> inactive = new List<GameObject>();

        private static List<GameObject> active = new List<GameObject>();

        private static Director director = new Director(new BulletBuilder());

        public static GameObject Create(Vector2 position, ContentManager content)
        {
            if (inactive.Count > 0)
            {
                GameObject platform = inactive[0];
                active.Add(platform);
                inactive.RemoveAt(0);

                return platform;
            }
            else
            {

                GameObject projectile = director.Construct(position);

                (projectile.GetComponent("Projectiles") as Projectiles).LoadContent(content);

                projectile.LoadContent(content);

                active.Add(projectile);

                return projectile;
            }
        }

        public static void ReleaseObject(GameObject platform)
        {
            CleanUp();

            inactive.Add(platform);

            active.Remove(platform);
        }

        private static void CleanUp()
        {
            
        }
    }
}
