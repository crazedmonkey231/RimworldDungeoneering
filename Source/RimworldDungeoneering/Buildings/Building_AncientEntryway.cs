using RimWorld;
using RimWorld.Planet;
using RimworldDungeoneering.MapManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.AI;
using static UnityEngine.GraphicsBuffer;

namespace RimworldDungeoneering.Buildings
{
    public class Building_AncientEntryway : Building, IArchway
    {
        public bool CanUse => true;

        public int UsingTicks => 500;

        public override IEnumerable<FloatMenuOption> GetFloatMenuOptions(Pawn selPawn)
        {
            yield return new FloatMenuOption(RDModConstants.Enter, () => 
            {
                selPawn.jobs.TryTakeOrderedJob(new Job(RDModDefOf.UseArchway, this)
                {
                    count = 1
                });
            });
        }

        public void Use()
        {
            GenExplosion.DoExplosion(Position, Map, 3, DamageDefOf.EMP, this, damAmount: 0);
            MapParent parent = (MapParent)WorldObjectMaker.MakeWorldObject(DefDatabase<WorldObjectDef>.GetNamed("DungeonMapParent"));
            parent.Tile = Tile;
            Find.WorldObjects.Add(parent);
            Find.World.info.seedString = Rand.Range(0, 2147483646).ToString();
            IntVec3 intVec3 = Find.World.info.initialMapSize;
            MapGenerator.GenerateMap(intVec3, parent, parent.MapGeneratorDef, parent.ExtraGenStepDefs);
        }
    }
}
