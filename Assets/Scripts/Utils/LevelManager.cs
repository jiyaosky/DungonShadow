using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> levels;

    public void InstantiateLevel()
    {
        levels[0].SetActive(true);
    }
    
    public void NextLevel()
    {
        
    }
}
