using TbsFramework.Units;
using UnityEngine;

namespace TbsFramework
{
    [CreateAssetMenu]
    public class ImmobilizationSkillBuff : Buff
    {
        private int originMovementPoints;
        public override void Apply(Unit unit)
        {
            originMovementPoints = (int)unit.MovementPoints;
            unit.MovementPoints = 0;
        }

        public override void Undo(Unit unit)
        {
            unit.MovementPoints = originMovementPoints;
        }
    }
}
