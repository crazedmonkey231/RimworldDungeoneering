using RimWorld;
using RimworldDungeoneering.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.AI;

namespace RimworldDungeoneering.Jobs
{
    public class JobDriver_UseArchway : JobDriver
    {
        private IArchway Openable => (IArchway)job.targetA.Thing;

        private Thing Target => job.targetA.Thing;

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return pawn.Reserve(job.targetA, job, 1, -1, null, errorOnFailed);
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            Toil toil = ToilMaker.MakeToil("MakeNewToils");
            toil.initAction = delegate
            {
                if (!Openable.CanUse)
                {
                    base.Map.designationManager.DesignationOn(job.targetA.Thing, DesignationDefOf.Open)?.Delete();
                }
            };
            yield return toil.FailOnDespawnedOrNull(TargetIndex.A);
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.InteractionCell).FailOnThingMissingDesignation(TargetIndex.A, DesignationDefOf.Open).FailOnDespawnedOrNull(TargetIndex.A);
            Toil toil2 = Toils_General.Wait(Openable.UsingTicks, TargetIndex.A).WithProgressBarToilDelay(TargetIndex.A).FailOnDespawnedOrNull(TargetIndex.A)
                .FailOnCannotTouch(TargetIndex.A, PathEndMode.InteractionCell);
            if (Target.def.building != null && Target.def.building.openingStartedSound != null)
            {
                toil2.PlaySoundAtStart(Target.def.building.openingStartedSound);
            }
            yield return toil2;
            Toil open = ToilMaker.MakeToil("Open");
            open.initAction = delegate
            {
                Pawn actor = open.actor;
                Thing thing = actor.CurJob.GetTarget(TargetIndex.A).Thing;
                actor.Map.designationManager.DesignationOn(thing, DesignationDefOf.Open)?.Delete();
                IArchway openable = (IArchway)thing;
                if (openable.CanUse)
                {
                    openable.Use();
                }
            };
            open.defaultCompleteMode = ToilCompleteMode.Instant;
            yield return open;
        }
    }
}
