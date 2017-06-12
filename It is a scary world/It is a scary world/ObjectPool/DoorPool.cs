using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace It_is_a_scary_world
{
    class DoorPool
    {
        private static List<GameObject> inactive = new List<GameObject>();

        private static List<GameObject> active = new List<GameObject>();

        private static Director director = new Director(new DoorBuilder());

        public static GameObject Create(Vector2 position, ContentManager content)
        {
            if (inactive.Count > 0)
            {
                GameObject door = inactive[0];
                active.Add(door);
                inactive.RemoveAt(0);

                return door;
            }
            else
            {

                GameObject door = director.Construct(position);

                (door.GetComponent("Door") as Door).LoadContent(content);

                door.LoadContent(content);

                active.Add(door);

                return door;
            }
        }

        public static void ReleaseObject(GameObject door)
        {
            CleanUp();

            inactive.Add(door);

            active.Remove(door);
        }

        private static void CleanUp()
        {
            //Reset data, remove references etc.
        }
    }
}
