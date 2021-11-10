using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 输入模块。
/// 简化输入事件读取的控制和分发。
/// </summary>
public class InputModule : MonoBehaviour, IGameModule
{
    public static void Init()
    {
        ModuleDispatcher.Instance.RegisterMono<InputModule>();
        DevUtils.Log("Inited", "InputModule");
    }

    /// <summary>
    /// 鼠标滚轮
    /// </summary>
    public static event Action<Vector2> Gameplay_MouseScroll = delegate { };
    /// <summary>
    /// WASD 或 方向键
    /// </summary>
    public static event Action<Vector2> Gameplay_Movement = delegate { };
    /// <summary>
    /// 鼠标控制镜头移动
    /// </summary>
    public static event Action<Vector2> PersonPerspective_Camera = delegate { };

    public static event Action Gameplay_LeftClick = delegate { };

    public static event Action Gameplay_RightClick = delegate { };

    public static event Action Gameplay_AnyKey = delegate { };

    private static DefaultInputAction input;

    /// <summary>
    /// 启用框架默认的Input Action
    /// </summary>
    private void Awake()
    {
        input = new();
        input.Gameplay.Enable();

        BindInputActions();
    }

    /// <summary>
    /// 绑定所有的输入事件
    /// </summary>
    private void BindInputActions()
    {
        input.Gameplay.MouseWheel.performed += ctx =>
        {
            Gameplay_MouseScroll(ctx.ReadValue<Vector2>());
        };

        input.Gameplay.Movement.performed += ctx =>
        {
            Gameplay_Movement(ctx.ReadValue<Vector2>());
        };

        input.Gameplay.LeftClick.performed += ctx =>
        {
            Gameplay_LeftClick();
        };

        input.Gameplay.RightClick.performed += ctx =>
        {
            Gameplay_RightClick();
        };

        input.PersonPerspective.Camera.performed += ctx =>
        {
            PersonPerspective_Camera(ctx.ReadValue<Vector2>());
        };

        input.Gameplay.Anykey.performed += ctx =>
        {
            Gameplay_AnyKey();
        };
    }

    /// <summary>
    /// 打开或关闭指定的输入组。
    /// </summary>
    /// <param name="actions">输入组的名称</param>
    /// <param name="on">打开还是关闭</param>
    public void ToggleActions(string actions, bool on)
    {
        InputActionMap map = input.asset.FindActionMap(actions);
        if (map != null)
        {
            if (on)
            { map.Enable(); }
            else
            { map.Disable(); }
        }
    }
}
