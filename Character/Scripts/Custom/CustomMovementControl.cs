using UnityEngine;


public partial class CustomCharacterControl : MonoBehaviour
{
    //运动模块
    [SerializeField] public Rigidbody RigPlayer;//刚体组件
    [SerializeField] public CapsuleCollider ColPlayer;//胶囊碰撞体

    [SerializeField] public float MoveSpeed = 10;//移动速度
    [SerializeField] public float JumpHeight = 250;//跳跃力度
    [SerializeField] public float MaxFallSpeed = 20;//最大下落速度

    [SerializeField] public bool isAccelerateTimeUnLimited = true;//无限冲刺

    [SerializeField] public float Accelerate_Multiple = 3f;//冲刺倍率
    [SerializeField] public float Accelerate_Time = 1f;//冲刺时间

    private float MoveMultiple = 1f;
    private bool isAccelerate = false;

    /// <summary>
    /// 运动初始化
    /// </summary>
    public void InitMovementController()
    {

    }

    /// <summary>
    /// Update - 运动控制
    /// </summary>
    public void UpdateMovementController()
    {
        if (isAccelerateTimeUnLimited)//无限冲刺
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                Accelerate();
            else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
                SpeedRecover();
        }
        else//限时冲刺
        {
            if (!isAccelerate && (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)))
                AccelerateLimited();
        }

        if (Input.GetKeyDown(KeyCode.Space)) Jump();

        Move();
    }

    /// <summary>
    /// 常规移动
    /// </summary>
    public void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 x = Vector3.Project(RigPlayer.velocity, RotateX.right);
        Vector3 y = Vector3.Project(RigPlayer.velocity, transform.up);
        Vector3 z = Vector3.Project(RigPlayer.velocity, RotateX.forward);

        //移动时
        bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);

        if (isMoving)
        {
            x = h * MoveSpeed * MoveMultiple * RotateX.right / (Mathf.Sqrt(h * h + v * v) + 0.0001f);
            z = v * MoveSpeed * MoveMultiple * RotateX.forward / (Mathf.Sqrt(h * h + v * v) + 0.0001f);
        }
        //没有行动时
        else
        {
            x = Vector3.zero;
            z = Vector3.zero;
        }

        //自由落体
        if (y.magnitude > MaxFallSpeed && Vector3.Dot(y, transform.up) < 0)
            y = -MaxFallSpeed * transform.up;

        RigPlayer.velocity = x + y + z;
    }

    /// <summary>
    /// 冲刺（无限）
    /// </summary>
    public void Accelerate()
    {
        //速度系数变大
        MoveMultiple = Accelerate_Multiple;
        //视野拉远
        TargetFieldofView = Accelerate_Field_of_View;
        TargetFocusSize = Accelerate_FocusSize;
    }

    /// <summary>
    /// 冲刺（限时）
    /// </summary>
    public void AccelerateLimited()
    {
        //速度系数变大
        MoveMultiple = Accelerate_Multiple;
        //视野拉远
        TargetFieldofView = Accelerate_Field_of_View;
        TargetFocusSize = Accelerate_FocusSize;
        //冲刺时间控制
        Invoke("SpeedRecover", Accelerate_Time);
        //标记冲刺状态
        isAccelerate = true;
    }

    /// <summary>
    /// 冲刺复原
    /// </summary>
    public void SpeedRecover()
    {
        //速度系数复原
        MoveMultiple = 1f;
        //视野恢复
        TargetFieldofView = Normal_Field_of_View;
        TargetFocusSize = Normal_FocusSize;
        //退出冲刺状态
        isAccelerate = false;
    }

    /// <summary>
    /// 跳跃
    /// </summary>
    public void Jump()
    {
        RigPlayer.AddForce(transform.up * JumpHeight);
    }
}