using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BGM", menuName = "Config/BGM")]
public class BGMConfig : ScriptableObject
{
    public List<AudioClip> bgms;
}
