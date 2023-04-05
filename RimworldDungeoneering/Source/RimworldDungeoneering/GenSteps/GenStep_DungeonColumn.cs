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
    public class GenStep_DungeonColumn : GenStep
    {
        public override int SeedPart => 820815231;

        public override void Generate(Map map, GenStepParams parms)
        {
            Map newMap = ((DungeonMapParent)map.info.parent).Map;
            foreach (IntVec3 allCell in newMap.AllCells.Where(i => i.Walkable(newMap)))
            {
                if((ThingDefOf.Wall.Equals(newMap.thingGrid.ThingAt<Thing>(allCell + IntVec3.West)?.def) 
                    && ThingDefOf.Wall.Equals(newMap.thingGrid.ThingAt<Thing>(allCell + IntVec3.South)?.def)
                    && ThingDefOf.Wall.Equals(newMap.thingGrid.ThingAt<Thing>(allCell + IntVec3.East)?.def)
                    && newMap.thingGrid.ThingAt<Thing>(allCell + IntVec3.North) == null) 
                    || (ThingDefOf.Wall.Equals(newMap.thingGrid.ThingAt<Thing>(allCell + IntVec3.North)?.def)
                    && ThingDefOf.Wall.Equals(newMap.thingGrid.ThingAt<Thing>(allCell + IntVec3.South)?.def)
                    && ThingDefOf.Wall.Equals(newMap.thingGrid.ThingAt<Thing>(allCell + IntVec3.West)?.def)
                    && newMap.thingGrid.ThingAt<Thing>(allCell + IntVec3.East) == null))
                {
                    GenSpawn.Spawn(ThingMaker.MakeThing(ThingDefOf.Column, RDModDefOf.BlocksMarble), allCell, newMap);
                }
            }
        }
    }
}
