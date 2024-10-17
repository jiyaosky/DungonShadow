using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleEntity : Entity
{
    protected override void OnBindEvent()
    {
        On<string>("TestEvent", FromEvent);
    }

    protected override void OnInit()
    {
        // 初始化
        
        Emit("TestEvent", "Hello World");// 触发事件
    }

    protected override void OnRemove()
    {
        //销毁
    }

    protected override void OnTick(float delta)
    {
        // 物理帧
    }

    protected override void OnUpdate(float delta)
    {
        // 渲染帧
    }

    protected override void OnEnable()
    {
        // 启用
    }

    protected override void OnDisable()
    {
        // 禁用
    }

    private void FromEvent(string msg)
    {
        Debug.Log("FromEvent: " + msg);
    }
}
