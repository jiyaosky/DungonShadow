using System.Collections;
using System.Collections.Generic;
using System.Linq;
using QTEPack;
using TbsFramework.Grid;
using TbsFramework.Units.Abilities;
using UnityEngine;

namespace TbsFramework.Units.Abilities
{
    public class DaggerAttack : Ability
    {
        
        public QTE_CircleHit qte;
        
        public override IEnumerator Act(CellGrid cellGrid, bool isNetworkInvoked = false)
        {
            qte.OnSuccess.AddListener(() =>
            {
                // UnitReference.
            });
            qte.ShowQTE(UnitReference.transform.position,1,1);
            qte.RunQTE(1);
            yield return null;
        }

        
        public override void Initialize()
        {
            // AttackAbility = GetComponent<PlayerAttackAbility>();
        }

        public void Activate()
        {
            var cellGrid = FindObjectOfType<CellGrid>();
            if (CanPerform(cellGrid))
            {
                StartCoroutine(HumanExecute(cellGrid));
            }
        }

    }
}