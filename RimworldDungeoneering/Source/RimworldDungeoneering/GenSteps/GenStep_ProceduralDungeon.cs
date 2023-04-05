using Mono.Cecil.Cil;
using RimWorld;
using RimworldDungeoneering.MapManager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace RimworldDungeoneering.GenSteps
{
    public class GenStep_ProceduralDungeon : GenStep
    {
        public override int SeedPart => 820815231;

        public override void Generate(Map map, GenStepParams parms)
        {
            int size = 18;
            Map newMap = ((DungeonMapParent)map.info.parent).Map;
            int nWidth = (int)Math.Floor(newMap.Size.x / (double)size);
            int nHeight = (int)Math.Floor(newMap.Size.z / (double)size);
            Log.Message("nWidth: " + nWidth + " " + "nHeight: " + nHeight);
            for(int i = 0; i < nWidth; i++)
            {
                for (int j = 0; j < nHeight; j++)
                {
                    IntVec2 intVec2;
                    Sketch monument = new Sketch();
                    int num = Rand.Range(size, size);
                    intVec2 = new IntVec2(num, num);
                    int width = intVec2.x;
                    int height = intVec2.z;
                    bool[,] flagArray = AbstractShapeGenerator.Generate(width, height, false, false, allTruesMustBeConnected: false, allowEnclosedFalses: false, preferOutlines: true);
                    for (int newX = 0; newX < flagArray.GetLength(0); ++newX)
                    {
                        for (int newZ = 0; newZ < flagArray.GetLength(1); ++newZ)
                        {
                            if (flagArray[newX, newZ])
                                monument.AddThing(ThingDefOf.Wall, new IntVec3(newX, 0, newZ), Rot4.North, RDModDefOf.BlocksMarble);
                        }
                    }
                    monument.Spawn(map, new IntVec3(size * i, 0, size * j), (Faction)null, canSpawnThing: ((Func<SketchEntity, IntVec3, bool>)((entity, cell) =>
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
    }
}
