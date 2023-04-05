using RimworldDungeoneering.MapManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RimworldDungeoneering.GenSteps
{
    public class GenStep_Start : GenStep
    {
        public override int SeedPart => 820815231;

        public override void Generate(Map map, GenStepParams parms)
        {
            DeepProfiler.Start("RebuildAllRegions");
            map.regionAndRoomUpdater.RebuildAllRegionsAndRooms();
            DeepProfiler.End();
            MapGenerator.PlayerStartSpot = ((DungeonMapParent)map.info.parent).Map.Center;
        }
    }
}
