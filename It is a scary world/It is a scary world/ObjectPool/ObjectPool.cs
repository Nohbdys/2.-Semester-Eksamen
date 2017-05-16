using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace It_is_a_scary_world
{
    class ObjectPool
    {
        //test
        private static List<GameObject> inactive = new List<GameObject>();

        private static List<GameObject> active = new List<GameObject>();

        private static Director director = new Director(new PlatformBuilder());

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

                GameObject platform = director.Construct(position);

                (platform.GetComponent("Platform") as Platform).LoadContent(content);

                platform.LoadContent(content);

                active.Add(platform);

                return platform;
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
            //Reset data, remove references etc.
        }
    }
}
