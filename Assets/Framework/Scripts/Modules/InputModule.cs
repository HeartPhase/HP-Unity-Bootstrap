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
}
