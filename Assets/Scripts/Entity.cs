using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    private readonly Dictionary<string, object> _eventMap = new Dictionary<string, object>();

    

    /// <summary>
    /// 在这里绑定事件
    /// </summary>
    protected virtual void OnBindEvent()
    {

    }

    /// <summary>
    /// 初始化
    /// </summary>
    protected virtual void OnInit()
    {

    }

    /// <summary>
    /// 销毁
    /// </summary>
    protected virtual void OnRemove()
    {

    }

    /// <summary>
    /// 物理帧
    /// </summary>
    protected virtual void OnTick()
    {

    }

    /// <summary>
    /// 渲染帧
    /// </summary>
    protected virtual void OnUpdate()
    {

    }

    /// <summary>
    /// 启用
    /// </summary>
    protected virtual void OnEnable()
    {

    }

    /// <summary>
    /// 禁用
    /// </summary>
    protected virtual void OnDisable()
    {

    }

    private void OnDestroy()
    {
        OnRemove();
        _eventMap.Clear();
    }

    private void Awake()
    {
        OnBindEvent();
        OnInit();
    }

    private void FixedUpdate()
    {
        OnTick();
    }

    private void Update()
    {
        OnUpdate();
    }

    

    public void Emit<T>(string @event, T data)
    {
        if (_eventMap.TryGetValue(@event, out var callback))
        {
            ((Action<T>)callback).Invoke(data);
        }
    }

    public void On<T>(string @event, Action<T> callback)
    {
        if (!_eventMap.ContainsKey(@event))
        {
            _eventMap.Add(@event, callback);
        }
        else
        {
            _eventMap[@event] = callback;
        }
    }

    public void Off(string @event)
    {
        if (_eventMap.ContainsKey(@event))
        {
            _eventMap.Remove(@event);
        }
    }


}
