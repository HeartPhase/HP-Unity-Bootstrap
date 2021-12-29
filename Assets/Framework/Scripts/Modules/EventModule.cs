using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

/// <summary>
/// 事件模块。
/// 游戏内可以不受引用限制地传递事件。
/// </summary>
public class EventModule : IGameModule
{
    public static void Init() {
        ModuleDispatcher.Instance.Register<EventModule>();
        DevUtils.Log("Inited", "EventModule");
    }

    private Dictionary<string, Action<EventArgs>> events = new();

    /// <summary>
    /// 监听名为name的事件。
    /// </summary>
    public void Listen(string name, Action<EventArgs> action)
    {
        if (action == null) {
            DevUtils.Log("Null Listen");
            return;
        }

        if (events.ContainsKey(name))
        {
            Delegate[] actionsList = events[name].GetInvocationList();
            if (Array.Exists(actionsList, s => s == (Delegate)action))
            {
                DevUtils.Log("Duplicated Listen");
            }
            else
            {
                events[name] += action;
            }
        }
        else { 
            events.Add(name, action);
        }
    }

    /// <summary>
    /// 触发名为name的事件。
    /// </summary>
    public void Invoke(string name, EventArgs args = null) {
        if (events.ContainsKey(name)) { 
            events[name].Invoke(args);
        }
    }

    /// <summary>
    /// 移除对名为name事件的所有监听。
    /// </summary>
    /// <param name="name"></param>
    public void Remove(string name) {
        if (events.ContainsKey(name)) { 
            events.Remove(name);
        }
    }

    /// <summary>
    /// 移除对名为name事件的特定监听。
    /// </summary>
    public void Remove(string name, Action<EventArgs> action) {
        if (action == null) {
            DevUtils.Log("Null Removal");
            return;
        }

        if (events.ContainsKey(name))
        {
            Delegate[] actionList = events[name].GetInvocationList();
            if (Array.Exists(actionList, s => s == (Delegate)action))
            {
                events[name] -= action;
                if (events[name] == null) { 
                    events.Remove(name);
                }
            }
            else
            {
                DevUtils.Log("No such listen");
            }
        }
        else {
            DevUtils.Log("No such event");
        }
    }

    /// <summary>
    /// 移除所有监听。
    /// </summary>
    public void RemoveAll() { 
        events.Clear();
    }

    /// <summary>
    /// Debug用，获取当前的监听信息。
    /// 为了安全起见做了一下Clone，掩耳盗铃。
    /// </summary>
    public Dictionary<string, Action<EventArgs>> ReadEventsData() {
        return events.ToDictionary(s => s.Key, s => s.Value);
    }
}

/// <summary>
/// 默认的事件参数。
/// </summary>
public class EventArgs { 
    public object sender = null;
    public object[] args = null;

    /// <summary>
    /// 方便事件触发时临时创建参数。
    /// </summary>
    public EventArgs(object _sender, params object[] _args) { 
        sender = _sender;
        args = _args;
    }

    /// <summary>
    /// 方便Debug，稍微格式化一下输出。
    /// </summary>
    public override string ToString()
    {
        string result = "";
        foreach (object arg in args)
        {
            result += $"{arg.ToString()}, ";
        }
        return sender.ToString() + " - " + result.Remove(result.Length - 2);
    }
}
