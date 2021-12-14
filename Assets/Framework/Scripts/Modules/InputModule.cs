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
    /// <summary>
    /// 交互/使用/确认
    /// </summary>
    public static event Action Gameplay_Interact = delegate { };
    /// <summary>
    /// 需要第二个交互按钮或其他区分时使用
    /// </summary>
    public static event Action Gameplay_Use = delegate { };

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

        input.Gameplay.Interact.performed += ctx =>
        {
            Gameplay_Interact();
        };

        input.Gameplay.Use.performed += ctx =>
        {
            Gameplay_Use();
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

    /// <summary>
    /// 通过Input Mapping里的路径获取对应的InputAction。
    /// </summary>
    /// <param name="actionHierarchyName">形如"Gameplay/Interact"</param>
    private InputAction FindActionByName(string actionHierarchyName) {
        InputAction action = input.asset.FindAction(actionHierarchyName);
        return action;
    }

    /// <summary>
    /// 获取路径对应InputAction所绑定的操作。
    /// 例如："Gameplay/Interact" -> "[E]"。
    /// </summary>
    public string GetCurrentBinding(string actionHierarchyName) { 
        InputAction actionToRemap = FindActionByName(actionHierarchyName);
        return InputControlPath.ToHumanReadableString(actionToRemap.GetBindingDisplayString());
    }

    /// <summary>
    /// 动态换绑操作对应的按键。
    /// 可自定义绑定成功/取消/发现键位冲突时的Callback。
    /// </summary>
    public void RemapAction(string actionHierarchyName, Action onComplete = null, Action onCancel = null, Action<bool, string> onDuplicate = null) {
        InputAction actionToRemap = FindActionByName(actionHierarchyName);
        if (actionToRemap == null) {
            DevUtils.Log($"No {actionHierarchyName} action to remap", "InputModule");
        }
        actionToRemap.Disable();
        var RebindingOperation = actionToRemap.PerformInteractiveRebinding()
            .WithControlsExcluding("<Mouse>/*")
            .WithCancelingThrough("<Keyboard>/escape")
            .OnCancel(
                (op) => {
                    op.action.Enable();
                    op.Dispose();
                    if (onCancel != null) onCancel.Invoke();
                }
            )
            .OnComplete(
                (op) => { 
                    op.action.Enable();
                    op.Dispose();
                    if (onComplete != null) onComplete.Invoke();
                }
            )
            .OnApplyBinding(
                (op, path) => {
                    if (!HaveDuplicateBinding(path, actionToRemap))
                        actionToRemap.ApplyBindingOverride(path);
                    else if (onDuplicate != null) {
                        onDuplicate(true, InputControlPath.ToHumanReadableString(path));
                    }
                }
            )
            .Start();
    }

    /// <summary>
    /// 检测键位是否出现键位冲突。
    /// </summary>
    private bool HaveDuplicateBinding(string path, InputAction action) {
        foreach (InputBinding binding in input.bindings) {
            if (binding.action == action.bindings[0].action) {
                continue;
            }
            if (binding.effectivePath == path) {
                DevUtils.Log($"Find Duplicate@{binding.action}");
                return true;
            }
        }
        return false;
    }
}
