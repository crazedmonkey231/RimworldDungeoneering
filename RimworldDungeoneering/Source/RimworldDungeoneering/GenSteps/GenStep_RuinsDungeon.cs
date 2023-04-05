using JetBrains.Annotations;
using RimWorld;
using RimWorld.BaseGen;
using RimWorld.SketchGen;
using RimworldDungeoneering.MapManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using ResolveParams = RimWorld.SketchGen.ResolveParams;

namespace RimworldDungeoneering.GenSteps
{

    public class GenStep_RuinsDungeon : GenStep_Scatterer
    {
        private static readonly SimpleCurve RuinSizeChanceCurve = new SimpleCurve()
    {
      {
        new CurvePoint(6f, 0.0f),
        true
      },
      {
        new CurvePoint(6.001f, 300f),
        true
      },
      {
        new CurvePoint(10f, 50f),
        true
      },
      {
        new CurvePoint(20f, 10f),
        true
      },
      {
        new CurvePoint(30f, 0.0f),
        true
      }
    };
        private int randomSize;

        public override int SeedPart => 1348417666;

        protected override bool TryFindScatterCell(Map map, out IntVec3 result)
        {
            Map newMap = ((DungeonMapParent)map.info.parent).Map;
            this.randomSize = Mathf.RoundToInt(Rand.ByCurve(RuinSizeChanceCurve));
            return base.TryFindScatterCell(newMap, out result);
        }

        protected override bool CanScatterAt(IntVec3 c, Map map) => base.CanScatterAt(c, map) && c.SupportsStructureType(map, TerrainAffordanceDefOf.Heavy) && this.CanPlaceAncientBuildingInRange(new CellRect(c.x, c.z, this.randomSize, this.randomSize).ClipInsideMap(map), map);

        protected bool CanPlaceAncientBuildingInRange(CellRect rect, Map map)
        {
            foreach (IntVec3 cell in rect.Cells)
            {
                if (cell.InBounds(map))
                {
                    TerrainDef terrainDef = map.terrainGrid.TerrainAt(cell);
                    if (terrainDef.HasTag("River") || terrainDef.HasTag("Road") || !GenConstruct.CanBuildOnTerrain((BuildableDef)ThingDefOf.Wall, cell, map, Rot4.North))
                        return false;
                }
            }
            return true;
        }

        protected override void ScatterAt(IntVec3 c, Map map, GenStepParams parms, int stackCount = 1)
        {
            CellRect rect = new CellRect(c.x, c.z, this.randomSize, this.randomSize).ClipInsideMap(map);
            if (!this.CanPlaceAncientBuildingInRange(rect, map))
                return;

            Sketch sketch = SketchGen.Generate(SketchResolverDefOf.Monument, new ResolveParams()
            {
                sketch = new Sketch(),
                monumentSize = new IntVec2?(new IntVec2(rect.Width, rect.Height))
            });

            sketch.Spawn(map, rect.CenterCell, (Faction)null, canSpawnThing: ((Func<SketchEntity, IntVec3, bool>)((entity, cell) =>
            {
                bool flag = false;
                foreach (IntVec3 adjacentCell in entity.OccupiedRect.AdjacentCells)
                {
                    IntVec3 c1 = cell + adjacentCell;
                    if (c1.InBounds(map))
                    {
                        Building edifice = c1.GetEdifice(map);
                        if (edifice == null || !edifice.def.building.isNaturalRock)
                        {
                            flag = true;
                            break;
                        }
                    }
                }
                return flag;
            })));
        }
    }
}
