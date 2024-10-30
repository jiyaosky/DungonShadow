using TbsFramework.Units;

namespace TbsFramework.Example4
{
    public class AdvWrsWizard : AdvWrsUnit
    {
        public int RangedAttackFactor;

        protected override AttackAction DealDamage(Unit other, int cost)
        {
            int distance = Cell.GetDistance(other.Cell);
            if (distance == 1)
            {
                return new AttackAction(AttackFactor, cost);
            }
            else
            {
                return new AttackAction(RangedAttackFactor, cost);
            }
        }
    }
}

