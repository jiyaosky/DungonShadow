using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TbsFramework.Cells;
using TbsFramework.Grid;
using TbsFramework.Grid.GameResolvers;
using TbsFramework.Units;
using TbsFramework.Units.Abilities;
using UnityEngine;

namespace TbsFramework
{
    public class PositionCondition : GameEndCondition
    {
        public Unit DestinationUnit;
        public int AppliesToPlayerNo;

        public override GameResult CheckCondition(CellGrid cellGrid)
        {
            var interAbility = DestinationUnit.GetComponent<InteractiveAbility>();
            
            if (interAbility.isInteracted)
            {
                var winningPlayers = new List<int>() { 0 };
                var loosingPlayers = cellGrid.Players.Where(p => p.PlayerNumber != 0)
                    .Select(p => p.PlayerNumber)
                    .ToList();
            
                return new GameResult(true, winningPlayers, loosingPlayers);
            }
            return new GameResult(false, null, null);
        }
    }
}

