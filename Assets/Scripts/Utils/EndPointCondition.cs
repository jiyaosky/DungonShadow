using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TbsFramework.Cells;
using TbsFramework.Grid;
using TbsFramework.Grid.GameResolvers;
using Unity.VisualScripting;
using UnityEngine;

namespace TbsFramework
{
    public class PositionCondition : GameEndCondition
    {
        public Unit DestinationUnit;
        public int AppliesToPlayerNo;

        public override GameResult CheckCondition(CellGrid cellGrid)
        {
        //     if (DestinationCell.CurrentUnits.Count > 0
        //         && (DestinationCell.CurrentUnits.Exists(u => u.PlayerNumber == AppliesToPlayerNo) || AnyPlayer == true))
        //     {
        //         var winningPlayers = new List<int>() { DestinationUnit.PlayerNumber };
        //         var loosingPlayers = cellGrid.Players.Where(p => p.PlayerNumber != DestinationCell.CurrentUnits[0].PlayerNumber)
        //             .Select(p => p.PlayerNumber)
        //             .ToList();
        //
        //         return new GameResult(true, winningPlayers, loosingPlayers);
        //     }
        //
            return new GameResult(false, null, null);
        }
    }
}

