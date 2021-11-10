using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
            Action<EventArgs> actions = events[name];
            Delegate[] actionsList = actions.GetInvocationList();
            if (Array.Exists(actionsList, s => s == (Delegate)action))
            {
                DevUtils.Log("Duplicated Listen");
            }
            else
            {
                actions += action;
            }
        }
        else { 
            events.Add(name, action);
        }
    }

    /// <summary>
    /// 触发名为name的事件。
    /// </summary>
    public void Invoke(string name, EventArgs args) {
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
            Action<EventArgs> actions = events[name];
            Delegate[] actionList = actions.GetInvocationList();
            if (Array.Exists(actionList, s => s == (Delegate)action))
            {
                actions -= action;
                if (actions.GetInvocationList().Length == 0) { 
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
