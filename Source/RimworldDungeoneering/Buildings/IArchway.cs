using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RimworldDungeoneering.Buildings
{
    public interface IArchway
    {
        bool CanUse { get; }

        int UsingTicks { get; }

        void Use();
    }
}
