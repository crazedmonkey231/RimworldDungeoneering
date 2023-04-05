using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace RimworldDungeoneering
{
    public class RDMod : Mod
    {

        private static RDModSettings settings;
        private static Harmony harmony;

        public static RDModSettings Settings => settings;

        public static Harmony Harmony { get => harmony; set => harmony = value; }

        public RDMod(ModContentPack content) : base(content)
        {
            harmony = new Harmony(Content.Name);
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            settings = GetSettings<RDModSettings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return Content.Name;
        }
    }
}
