using System;
using System.Collections;
using System.Collections.Generic;
using Autotiles3D;
using TbsFramework.Cells;
using TbsFramework.Example1;
using TbsFramework.Grid;
using TbsFramework.Units.Abilities;
using UnityEngine;


namespace TbsFramework.Units
{
    public class AIEnemy : MyUnit
    {
        // 0 ->0
        // 
        // public override void MaskAsAISight()
        // {
        //     // facingDirection = transform.rotation.eulerAngles.y;
        //     Check();
        // }
        // private Animator playerAnimator;
        
        public override void Initialize()
        {
            // playerAnimator = GetComponentInChildren<Animator>();
            SetNewCurrentForward();
            base.Initialize();
        }

        public void SetNewCurrentForward()
        {
            var Directon2D = GridUtils.GetDirectionV3ByAngleY(transform.rotation.eulerAngles.y);
            currentForward = new Vector3(Directon2D.x, 0, Directon2D.z);
        }

        public override void ChangeFoward(Vector3 current, Vector3 target)
        {
            SetNewCurrentForward();
            base.ChangeFoward(current, target);
        }

        public override void MaskAsAISight()
        {
            foreach (var cell in insightCells)
            {
                cell.transform.Find("Inline").gameObject.SetActive(false);
            }
            CheckAISight();
        }

        public void CheckAISight()
        {
            var _cellGrid = FindObjectOfType<CellGrid>();
            var readPlayer = _cellGrid.AIGetEnemyUnits()[0];
            var enemies = _cellGrid.GetAIEnemies();
            
            List<Cell> cells = GetVisibleCells(_cellGrid);
            for (int i = 0; i < cells.Count; i++)
            {
                // (Autotiles3D_BlockBehaviour)cells[i].MaskAsAISight();
                Autotiles3D_BlockBehaviour cell = cells[i].GetComponent<Autotiles3D_BlockBehaviour>();
                cell.MaskAsAISight();
                
                // 发现玩家
                if (cells[i].Equals(readPlayer.Cell))
                {
                    AIState = 2;
                }
                
                // 发现其余友军Unit也处于追击状态则一起追击
                foreach (var enemy in enemies)
                {
                    if (enemy.Equals(this))
                    {
                        continue;
                    }
                    if ((enemy.AIState == 2) && cells[i].Equals(enemy.Cell))
                    {
                        AIState = 2;
                    }
                }
                
            }
            
        }
        
        public float sightRange = 3f;
        public float sightAngle = 45f;
        private List<Cell> insightCells = new List<Cell>();

        // 获取视野范围内的所有单元格
        public List<Cell> GetVisibleCells(CellGrid cellGrid)
        {
            GridUtils.GetCellsInsight(cellGrid, insightCells,
                new Vector2Int(Mathf.RoundToInt(this.Cell.OffsetCoord.x), Mathf.RoundToInt(this.Cell.OffsetCoord.y)),
                sightRange,
                transform.rotation.eulerAngles.y,
                sightAngle, false);
            return insightCells;
        }
        
        // AI的三种状态
        // public int AIState = 0; // 0=未发现玩家的自然状态，1=警戒状态， 2=发现玩家的状态
        
        public Cell AICellStartCell;
        
        public Cell AICellEndCell;
        
        // 0的时候运行
        public override void NatureBehaviour()
        {
            if (AICellStartCell != null && AICellEndCell != null)
            {
                Debug.Log("可以移动了");
                AIPatrol(AICellStartCell, AICellEndCell);
            }
        }
        
        // 巡逻相关
        private Cell AITargetCell;
        public void AIPatrol(Cell startCell, Cell endCell)
        {
            var moveAbility = this.GetComponent<MoveAbility>();
            // 如果当前的cell不是设定好的startCell则先移动到初始地块
            if (!this.Cell.Equals(startCell) && AITargetCell == null)
            {
                Debug.Log("还没到巡逻初始点呢");
                AITargetCell = startCell;
                moveAbility.Destination = CalculatePatrolPath(startCell)[0];
                moveAbility.Act(FindObjectOfType<CellGrid>(), false);
            }

            if (this.Cell.Equals(startCell))
            {
                AITargetCell = endCell;
            }

            if (this.Cell.Equals(endCell))
            {
                AITargetCell = startCell;
            }
            moveAbility.OnAbilitySelected(FindObjectOfType<CellGrid>());
            moveAbility.Destination = CalculatePatrolPath(AITargetCell)[0];
            StartCoroutine(moveAbility.Act(FindObjectOfType<CellGrid>(), false));
        }

        public List<Cell> CalculatePatrolPath(Cell targetCell)
        {
            var path = this.FindPath(FindObjectOfType<CellGrid>().Cells, targetCell);
            List<Cell> selectedPath = new List<Cell>();
            float cost = 0;

            for (int i = path.Count - 1; i >= 0; i--)
            {
                var cell = path[i];
                cost += cell.MovementCost;
                if (cost <= this.MovementPoints)
                {
                    selectedPath.Add(cell);
                }
                else
                {
                    for (int j = selectedPath.Count - 1; j >= 0; j--)
                    {
                        if (!this.IsCellMovableTo(selectedPath[j]))
                        {
                            selectedPath.RemoveAt(j);
                        }
                        else
                        {
                            break;
                        }
                    }
                    break;
                }
            }
            selectedPath.Reverse();
            return selectedPath;
        }

        public Renderer aiRenderer;
        public override void UnitShow()
        {
            aiRenderer.enabled = true;
        }

        public override void UnitHide()
        {
            aiRenderer.enabled = false;
        }

        protected override void DefenceActionPerformed()
        {
            AIState = 2;
            base.DefenceActionPerformed();
        }

        public override void OnTurnStart()
        {
            MaskAsAISight();
            base.OnTurnStart();
        }
    }
}