using Assets.Code.Core.StatSystem;
using Assets.Code.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Test
{
    public class StatTester : MonoBehaviour
    {
        [SerializeField] private EntityStat statCompo;

        [SerializeField] private StatSO targetStat;
        [SerializeField] private float modifyValue;

        [ContextMenu("Test Execute")]
        private void TestExecute()
        {
            statCompo.GetStat(targetStat).AddModifier(this, modifyValue);
        }
        [ContextMenu("Test Rollback")]
        private void TextRollback()
        {
            statCompo.GetStat(targetStat).RemoveModifier(this);
        }

    }
}
