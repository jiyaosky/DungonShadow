using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    float dt;

    private void Awake()
    {
        GameApp.Instance.Init();
    }
    // Start is called before the first frame update
    void Start()
    {
        // 播放背景音乐
        GameApp.SoundManager.PlayBGM("bgm");
    }

    // Update is called once per frame
    void Update()
    {
        dt = Time.deltaTime;
        GameApp.Instance.Update(dt);
    }

}
