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

public partial class CharacterControl : MonoBehaviour
{
    private Animator animator;

    [Header("动画器")]
    [Label("状态变量 - 跑")] public string Key_isRun = "IsRun";
    [Label("状态变量 - 跳")] public string Key_isJump = "IsJump";
    [Label("状态变量 - 攻击1")] public string Key_isAttack01 = "IsAttack01";
    [Label("状态变量 - 攻击2")] public string Key_isAttack02 = "IsAttack02";
    [Label("状态变量 - 受击")] public string Key_isDamage = "IsDamage";
    [Label("状态变量 - 死亡")] public string Key_isDead = "IsDead";

    /// <summary>
    /// Start - 动作初始化
    /// </summary>
    void InitMotionController()
    {
        //获取组件
        animator = GetComponentInChildren<Animator>();
    }

    /// <summary>
    /// Update - 动作控制
    /// </summary>
    void UpdateMotionController()
    {
        //旋转模型，指向运动方向
        RotateBody();

        //运动
        MotionTrigger(0, Key_isRun, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.W);
        //跳跃
        MotionTrigger(1, Key_isJump, KeyCode.Space);
        //攻击1
        MotionTrigger(1, Key_isAttack01, 0);
        //攻击2
        MotionTrigger(1, Key_isAttack02, 1);
    }

    /// <summary>
    /// 旋转模型，指向运动方向
    /// </summary>
    public void RotateBody()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (x != 0 || z != 0)
            Body.rotation = Quaternion.LookRotation(x * RotateX.right + z * RotateX.forward);
    }

    /// <summary>
    /// 键盘触发（重载1）
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
    /// 鼠标触发（重载2）
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