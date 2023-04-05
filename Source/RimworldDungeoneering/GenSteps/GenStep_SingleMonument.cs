using RimWorld.SketchGen;
using RimWorld;
using RimworldDungeoneering.MapManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using System.Linq.Expressions;

namespace RimworldDungeoneering.GenSteps
{
    public class GenStep_SingleMonument : GenStep
    {
        public override int SeedPart => 820815231;

        public override void Generate(Map map, GenStepParams parms)
        {
            Map newMap = ((DungeonMapParent)map.info.parent).Map;
            int width = 25;
            int height = 25;

            Sketch sketch = SketchGen.Generate(SketchResolverDefOf.Monument, new ResolveParams()
            {
                sketch = new Sketch(),
                monumentSize = new IntVec2?(new IntVec2(width, height))
            });

            TerrainGrid terrainGrid = newMap.terrainGrid;
            Random rand = new Random();
            int xOffset = 3;
            int yOffset = 3;
            int dx = rand.Next(10 + width + xOffset, newMap.Size.x - 10 - width - xOffset);
            int dy = rand.Next(10 + height + yOffset, newMap.Size.z - 10 - height - yOffset);
            IntVec3 location = new IntVec3(dx, 0, dy);

            int ddx = location.x - 3 - width / 2;
            int ddy = location.z - 3 - height / 2;

            int ddx2 = location.x + 3 + width / 2;
            int ddy2 = location.z + 3 + height / 2;

            List<IntVec3> points = newMap.AllCells.Where((i) =>
            {
                return ddx < i.x && i.x < ddx2 && ddy < i.z && i.z < ddy2;
            }).ToList();

            foreach (var p in points)
            {
                IntVec3 cell = p;
                List<Thing> v = newMap.thingGrid.ThingsListAt(cell).ToList();
                foreach (var t in v)
                {
                    Thing r = t;
                    r.Destroy();
                }
                terrainGrid.SetTerrain(cell, TerrainDefOf.TileSandstone);
            }

            sketch.Spawn(newMap, location, (Faction)null, wipeIfCollides: true , clearEdificeWhereFloor: true);
        }
    }
}
