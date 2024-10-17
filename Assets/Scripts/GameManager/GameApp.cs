using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameApp : Singleton<GameApp>
{
    public static SoundManager SoundManager; // 音频管理器
    public override void Init()
    {
        SoundManager = new SoundManager();
    }
}
