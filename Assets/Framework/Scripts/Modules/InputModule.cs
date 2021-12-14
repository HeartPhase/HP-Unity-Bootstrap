using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// ����ģ�顣
/// �������¼���ȡ�Ŀ��ƺͷַ���
/// </summary>
public class InputModule : MonoBehaviour, IGameModule
{
    public static void Init()
    {
        ModuleDispatcher.Instance.RegisterMono<InputModule>();
        DevUtils.Log("Inited", "InputModule");
    }

    /// <summary>
    /// ������
    /// </summary>
    public static event Action<Vector2> Gameplay_MouseScroll = delegate { };
    /// <summary>
    /// WASD �� �����
    /// </summary>
    public static event Action<Vector2> Gameplay_Movement = delegate { };
    /// <summary>
    /// �����ƾ�ͷ�ƶ�
    /// </summary>
    public static event Action<Vector2> PersonPerspective_Camera = delegate { };
    /// <summary>
    /// ����/ʹ��/ȷ��
    /// </summary>
    public static event Action Gameplay_Interact = delegate { };
    /// <summary>
    /// ��Ҫ�ڶ���������ť����������ʱʹ��
    /// </summary>
    public static event Action Gameplay_Use = delegate { };

    public static event Action Gameplay_LeftClick = delegate { };

    public static event Action Gameplay_RightClick = delegate { };

    public static event Action Gameplay_AnyKey = delegate { };

    private static DefaultInputAction input;

    /// <summary>
    /// ���ÿ��Ĭ�ϵ�Input Action
    /// </summary>
    private void Awake()
    {
        input = new();
        input.Gameplay.Enable();

        BindInputActions();
    }

    /// <summary>
    /// �����е������¼�
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
    /// �򿪻�ر�ָ���������顣
    /// </summary>
    /// <param name="actions">�����������</param>
    /// <param name="on">�򿪻��ǹر�</param>
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
    /// ͨ��Input Mapping���·����ȡ��Ӧ��InputAction��
    /// </summary>
    /// <param name="actionHierarchyName">����"Gameplay/Interact"</param>
    private InputAction FindActionByName(string actionHierarchyName) {
        InputAction action = input.asset.FindAction(actionHierarchyName);
        return action;
    }

    /// <summary>
    /// ��ȡ·����ӦInputAction���󶨵Ĳ�����
    /// ���磺"Gameplay/Interact" -> "[E]"��
    /// </summary>
    public string GetCurrentBinding(string actionHierarchyName) { 
        InputAction actionToRemap = FindActionByName(actionHierarchyName);
        return InputControlPath.ToHumanReadableString(actionToRemap.GetBindingDisplayString());
    }

    /// <summary>
    /// ��̬���������Ӧ�İ�����
    /// ���Զ���󶨳ɹ�/ȡ��/���ּ�λ��ͻʱ��Callback��
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
    /// ����λ�Ƿ���ּ�λ��ͻ��
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
