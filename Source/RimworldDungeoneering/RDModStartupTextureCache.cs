using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace RimworldDungeoneering
{
    [StaticConstructorOnStartup]
    public static class RDModStartupTextureCache
    {
        private static readonly Texture2D EnterDoor;

        static RDModStartupTextureCache()
        {

            EnterDoor = ContentFinder<Texture2D>.Get("UI/hobbit-door");

        }
    }
}
