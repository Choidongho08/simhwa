﻿using System;
using UnityEngine;

namespace Assets.Code.Core.StatSystem
{
    [Serializable]
    public class StatOverride
    {
        [SerializeField] private StatSO stat;
        [SerializeField] private bool isUseOverride;
        [SerializeField] private float overrideValue;

        public StatOverride(StatSO stat) => this.stat = stat;

        public StatSO CreateStat()
        {
            StatSO newStat = stat.Clone() as StatSO;
            Debug.Assert(newStat != null, $"{stat.statName} clone failed");

            if(isUseOverride)
                newStat.BaseValue = overrideValue;

            return newStat;
        }

    }
}
