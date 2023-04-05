using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RimworldDungeoneering
{
    [DefOf]
    public class RDModDefOf
    {
        //Base game
        public static ThingDef BlocksMarble;
        public static ThingDef AncientLamppost;

        //Mod
        public static ThingDef AncientEntryway;

        public static JobDef UseArchway;

        public static WorldObjectDef DungeonMapParent;

        public static GameConditionDef DungeonAurora;

        public static MapGeneratorDef RuinsDungeonMap;
        public static GenStepDef DungeonStart;
        public static GenStepDef DungeonEnd;
        public static GenStepDef TerrainDeep;
        public static GenStepDef FogDeep;
    }
}
