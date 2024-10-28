using System.Collections.Generic;
using System.Linq;
using TbsFramework.Grid;
using TbsFramework.Units;


namespace TbsFramework.Players.AI.Evaluators
{
    public class DamageUnitEvaluator : UnitEvaluator
    {
        private float topDamage;

        public override void Precalculate(Unit evaluatingUnit, Player currentPlayer, CellGrid cellGrid)
        {
            var enemyUnits = AIGetEnemyUnits(cellGrid);
            var enemiesInRange = enemyUnits.Where(u => evaluatingUnit.Cell.GetDistance(u.Cell) <= evaluatingUnit.AttackRange);
            topDamage = enemiesInRange.Select(u => evaluatingUnit.DryAttack(u))
                                          .DefaultIfEmpty()
                                          .Max();
        }

        public override float Evaluate(Unit unitToEvaluate, Unit evaluatingUnit, Player currentPlayer, CellGrid cellGrid)
        {
            var score = evaluatingUnit.DryAttack(unitToEvaluate) / topDamage;
            return score;
        }
        
        // 只找玩家就行
        public List<Unit> AIGetEnemyUnits(CellGrid cellGrid)
        {
            // 先发现玩家再返回玩家，
            return cellGrid.Units.FindAll(u => u.PlayerNumber == 0);
        }
    }
}
