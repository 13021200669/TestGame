using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActionStateInfo
{
    [SerializeField] public ActionState actionState;
    [SerializeField] public AnimationClip anim;
    [SerializeField] public KeyCode key;
    [SerializeField] public string trigger;
}

public enum ActionState
{
    Idle = 0, Idle_Sad, Run, Attack01, Attack02, Jump, Damage, Dead, None
}

public partial class CharacterController : MonoBehaviour
{
    private Animator animator;

    [Header("动画器变量")]
    [SerializeField] private string Key_isRun = "IsRun";
    [SerializeField] private string Key_isAttack01 = "IsAttack01";
    [SerializeField] private string Key_isAttack02 = "IsAttack02";
    [SerializeField] private string Key_isJump = "IsJump";
    [SerializeField] private string Key_isDamage = "IsDamage";
    [SerializeField] private string Key_isDead = "IsDead";

    void InitMotionController()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void UpdateMotionController()
    {
        MotionTrigger(0, Key_isRun, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.W);

        MotionTrigger(1, Key_isAttack01, 0);

        MotionTrigger(1, Key_isAttack02, 1);

        MotionTrigger(1, Key_isJump, KeyCode.Space);
    }
    /// <summary>
    /// 键盘触发
    /// </summary>
    /// <param name="mode">0 - 按住，1 - 按下，2 - 抬起</param>
    /// <param name="trigger">动画器变量名</param>
    /// <param name="key">键盘按键</param>
    public void MotionTrigger(int mode, string trigger, params KeyCode[] key)
    {
        bool isAnyoneTrue = false;
        foreach (KeyCode k in key)
        {
            switch (mode)
            {
                case 1:
                    isAnyoneTrue |= Input.GetKeyDown(k);
                    break;
                case 2:
                    isAnyoneTrue |= Input.GetKeyUp(k);
                    break;
                default:
                    isAnyoneTrue |= Input.GetKey(k);
                    break;
            }
        }

        if (isAnyoneTrue)
        {
            animator.SetBool(trigger, true);
        }
        else
        {
            animator.SetBool(trigger, false);
        }
    }

    /// <summary>
    /// 鼠标触发
    /// </summary>
    /// <param name="mode">0 - 按住，1 - 按下，2 - 抬起</param>
    /// <param name="trigger">动画器变量名</param>
    /// <param name="button">鼠标按键</param>
    public void MotionTrigger(int mode, string trigger, params int[] button)
    {
        bool isAnyoneTrue = false;
        foreach (int b in button)
        {
            switch (mode)
            {
                case 1:
                    isAnyoneTrue |= Input.GetMouseButtonDown(b);
                    break;
                case 2:
                    isAnyoneTrue |= Input.GetMouseButtonUp(b);
                    break;
                default:
                    isAnyoneTrue |= Input.GetMouseButton(b);
                    break;
            }
        }

        if (isAnyoneTrue)
        {
            animator.SetBool(trigger, true);
        }
        else
        {
            animator.SetBool(trigger, false);
        }
    }
}