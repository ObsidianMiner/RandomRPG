using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RandomRPG.Entities;

namespace RandomRPG
{
    public static class Extensions
    {
        public static List<Entity> ToEntities(this List<Hero> heros)
        {
            List<Entity> entities = new List<Entity>();
            for (int i = 0; i < heros.Count; i++)
            {
                entities.Add(heros[i]);
            }
            return entities;
        }
        public static List<Entity> ToEntities(this List<Enemy> enemies)
        {
            List<Entity> entities = new List<Entity>();
            for (int i = 0; i < enemies.Count; i++)
            {
                entities.Add(enemies[i]);
            }
            return entities;
        }
    }

}