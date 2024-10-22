using System.Collections.Generic;
using TbsFramework.Cells;
using TbsFramework.Grid;
using TbsFramework.Units;

namespace TbsFramework.Units.Abilities
{
    public class PlayerAttackRangeHighlightAbility : Ability
    {
        List<Unit> inRange;

        public override void OnCellSelected(Cell cell, CellGrid cellGrid)
        {
            var availableDestinations = UnitReference.GetComponent<PlayerMoveAbility>().availableDestinations;
            if (!availableDestinations.Contains(cell))
            {
                return;
            }

            var enemyUnits = cellGrid.GetEnemyUnits(cellGrid.CurrentPlayer);
            inRange = enemyUnits.FindAll(u => ((Player)UnitReference).IsUnitAttackable(u, cell));

            inRange.ForEach(u => u.MarkAsReachableEnemy());
        }

        public override void OnCellDeselected(Cell cell, CellGrid cellGrid)
        {
            inRange?.ForEach(u => u.UnMark());
            var enemyUnits = cellGrid.GetEnemyUnits(cellGrid.CurrentPlayer);
            var inRangeLocal = enemyUnits.FindAll(u => ((Player)UnitReference).IsUnitAttackable(u, UnitReference.Cell));

            inRangeLocal.ForEach(u => u.MarkAsReachableEnemy());
        }

        public override void OnAbilityDeselected(CellGrid cellGrid)
        {
            inRange?.ForEach(u => u.UnMark());
        }

        public override void OnTurnEnd(CellGrid cellGrid)
        {
            inRange = null;
        }
    }
}