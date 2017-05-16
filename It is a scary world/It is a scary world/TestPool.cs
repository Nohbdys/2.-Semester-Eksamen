using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace It_is_a_scary_world
{
    class TestPool
    {

  
 private static List<GameObject> inactive = new List<GameObject>();

    private static List<GameObject> active = new List<GameObject>();

    private static Director director = new Director(new WeaponBuilder());

    public static GameObject Create(Vector2 position, ContentManager content)
    {
        if (inactive.Count > 0)
        {
            GameObject weapon = inactive[0];
            active.Add(weapon);
            inactive.RemoveAt(0);

            return weapon;
        }
        else
        {

            GameObject weapon = director.Construct(position);

            (weapon.GetComponent("Weapons") as Weapons).LoadContent(content);

            weapon.LoadContent(content);

            active.Add(weapon);

            return weapon;
        }
    }

    public static void ReleaseObject(GameObject weapon)
    {
        CleanUp();

        inactive.Add(weapon);

        active.Remove(weapon);
    }

    private static void CleanUp()
    {
        //Reset data, remove references etc.
    }
    }
}
