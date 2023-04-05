using RimWorld;
using RimworldDungeoneering.MapManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.Noise;

namespace RimworldDungeoneering.GenSteps
{
    public class GenStep_DungeonTerrain : GenStep
    {
        public override int SeedPart => 820815231;

        public override void Generate(Map map, GenStepParams parms)
        {
            Map newMap = ((DungeonMapParent)map.info.parent).Map;
            TerrainGrid terrainGrid = newMap.terrainGrid;
            foreach (IntVec3 allCell in newMap.AllCells)
            {
                terrainGrid.SetTerrain(allCell, TerrainDefOf.FlagstoneSandstone);
            }
        }
    }
}
