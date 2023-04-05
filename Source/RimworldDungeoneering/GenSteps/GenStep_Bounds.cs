using RimWorld;
using RimworldDungeoneering.MapManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RimworldDungeoneering.GenSteps
{
    public class GenStep_Bounds : GenStep
    {
        public override int SeedPart => 820815231;

        public override void Generate(Map map, GenStepParams parms)
        {
            Map newMap = ((DungeonMapParent)map.info.parent).Map;
            for(int x = 0; x < newMap.Size.x; x++)
            {
                for (int i = 0; i < 10; i++)
                {
                    GenSpawn.Spawn(ThingMaker.MakeThing(ThingDefOf.Wall, ThingDefOf.Steel), new IntVec3(x, 0, i), newMap);
                    GenSpawn.Spawn(ThingMaker.MakeThing(ThingDefOf.Wall, ThingDefOf.Steel), new IntVec3(x, 0, newMap.Size.x -i - 1), newMap);
                }
            }

            for (int y = 0; y < newMap.Size.z; y++)
            {
                for(int i = 0; i < 10; i++)
                {
                    GenSpawn.Spawn(ThingMaker.MakeThing(ThingDefOf.Wall, ThingDefOf.Steel), new IntVec3(i, 0, y), newMap);
                    GenSpawn.Spawn(ThingMaker.MakeThing(ThingDefOf.Wall, ThingDefOf.Steel), new IntVec3(newMap.Size.z - i - 1, 0, y), newMap);
                }
            }
        }
    }
}
