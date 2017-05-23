using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace It_is_a_scary_world
{
    class WallPool
    {
        private static List<GameObject> inactive = new List<GameObject>();

        private static List<GameObject> active = new List<GameObject>();

        private static Director director = new Director(new WallBuilder());

        public static GameObject Create(Vector2 position, ContentManager content, int xSize, int ySize)
        {
            if (inactive.Count > 0)
            {
                GameObject wall = inactive[0];
                active.Add(wall);
                inactive.RemoveAt(0);

                return wall;
            }
            else
            {

                GameObject wall = director.Construct(position);

                (wall.GetComponent("Wall") as Wall).LoadContent(content, xSize, ySize);

                wall.LoadContent(content);

                active.Add(wall);

                return wall;
            }
        }

        public static void ReleaseObject(GameObject wall)
        {
            CleanUp();

            inactive.Add(wall);

            active.Remove(wall);
        }

        private static void CleanUp()
        {
            //Reset data, remove references etc.
        }
    }
}
