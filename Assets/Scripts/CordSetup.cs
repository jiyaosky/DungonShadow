using System.Collections;
using System.Collections.Generic;
using TbsFramework.Cells;
using UnityEngine;

public class CordSetup : MonoBehaviour
{
    

    void Awake()
    {
        var allChild = GetComponentsInChildren<Transform>();
        foreach (var child in allChild)
        {
            Cell cell = child.GetComponent<Cell>();
            if(cell != null)
            {
                cell.OffsetCoord = new Vector2(Mathf.RoundToInt(cell.transform.position.x), Mathf.RoundToInt(cell.transform.position.z));
            }
        }
    }

    
    void Update()
    {
        
    }
}
