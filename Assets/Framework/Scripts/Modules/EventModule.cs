using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// �¼�ģ�顣
/// ��Ϸ�ڿ��Բ����������Ƶش����¼���
/// </summary>
public class EventModule : IGameModule
{
    public static void Init() {
        ModuleDispatcher.Instance.Register<EventModule>();
        DevUtils.Log("Inited", "EventModule");
    }

    private Dictionary<string, Action<EventArgs>> events = new();

    /// <summary>
    /// ������Ϊname���¼���
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
    /// ������Ϊname���¼���
    /// </summary>
    public void Invoke(string name, EventArgs args) {
        if (events.ContainsKey(name)) { 
            events[name].Invoke(args);
        }
    }

    /// <summary>
    /// �Ƴ�����Ϊname�¼������м�����
    /// </summary>
    /// <param name="name"></param>
    public void Remove(string name) {
        if (events.ContainsKey(name)) { 
            events.Remove(name);
        }
    }

    /// <summary>
    /// �Ƴ�����Ϊname�¼����ض�������
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
/// Ĭ�ϵ��¼�������
/// </summary>
public class EventArgs { 
    public object sender = null;
    public object[] args = null;

    /// <summary>
    /// �����¼�����ʱ��ʱ����������
    /// </summary>
    public EventArgs(object _sender, params object[] _args) { 
        sender = _sender;
        args = _args;
    }

    /// <summary>
    /// ����Debug����΢��ʽ��һ�������
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
