using RimworldDungeoneering.MapManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace RimworldDungeoneering.GenSteps
{
    public class GenStep_End : GenStep
    {
        public override int SeedPart => 820815231;

        public override void Generate(Map map, GenStepParams parms)
        {
            Map newMap = ((DungeonMapParent)map.info.parent).Map;
            GenSpawn.Spawn(ThingMaker.MakeThing(RDModDefOf.AncientEntryway), newMap.Center, newMap);
        }
    }
}
