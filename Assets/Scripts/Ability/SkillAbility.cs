using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TbsFramework.Cells;
using TbsFramework.Grid;
using TbsFramework.Grid.GridStates;
using UnityEngine;

namespace TbsFramework.Units.Abilities
{
    // 定义抽象基类 Skill，继承自 Ability
    public abstract class SkillAbility : Ability
    {
        public string SkillName;
        public string Description;
        
        public bool IsActive = false;

        // 可以添加特定于 Skill 的属性和方法
        public int Range = 0;

        public int APCost  = 0;
        
        public int CoolDown = 0;
        
        public int Price = 0;
        
        public abstract void Activate();
    }
}