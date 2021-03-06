﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Harmony;
using UnityEngine;
using Verse;
using Verse.AI;
using RimWorld;

namespace DSFI
{
    public abstract class IdleJobGiverBase
    {
        public abstract float GetWeight(Pawn pawn, Trait traitIndustriousness);
        public abstract Job TryGiveJob(Pawn pawn);
        public abstract void LoadDef(IdleJobGiverDef def);
    }

    public abstract class IdleJobGiver<T> : IdleJobGiverBase where T : IdleJobGiverDef
    {
        public override float GetWeight(Pawn pawn, Trait traitIndustriousness)
        {
            if (this.def != null)
            {
                float bonusMultiplier = 1.0f;
                if (traitIndustriousness != null && this.def.usefulness != 0)
                {
                    bonusMultiplier = (4.0f - Math.Abs(traitIndustriousness.Degree - this.def.usefulness)) / 2.0f;
                }

                return this.def.probabilityWeight * bonusMultiplier;
            }
            else
            {
                return 0f;
            }
        }

        public override void LoadDef(IdleJobGiverDef def)
        {
            this.def = def as T;
        }

        protected T def;
    }

    public abstract class IdleJobGiverDefaultDef : IdleJobGiver<IdleJobGiverDef> { }
}
