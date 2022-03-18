using UnityEngine;


public partial class CharacterControl : MonoBehaviour
{
    //运动模块
    [SerializeField] public CharacterController Player;//角色控制器组件
    [SerializeField] public CapsuleCollider ColPlayer;//胶囊碰撞体

    [SerializeField] public float MoveSpeed = 10;//移动速度
    [SerializeField] public float JumpHeight = 1.5f;//跳跃力度

    [SerializeField] public float GravityValue = 9.81f;//模拟重力
    [SerializeField] public float MaxFallSpeed = 10;//最大下落速度

    [SerializeField] public bool isAccelerateTimeUnLimited = true;//无限冲刺

    [SerializeField] public float Accelerate_Multiple = 3f;//冲刺倍率
    [SerializeField] public float Accelerate_Time = 1f;//冲刺时间

    private Vector3 playerVelocity = Vector3.zero;
    private bool isPlayerGrounded;

    private float moveMultiple = 1f;
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

        Move();
    }

    /// <summary>
    /// 常规移动
    /// </summary>
    public void Move()
    {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 x_Velocity, z_Velocity;
        Vector3 y_Velocity = Vector3.Project(playerVelocity, transform.up);

        ////水平运动 - X, Z
        bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);
        if (isMoving)
        {
            x_Velocity = h * MoveSpeed * PlayerScale * moveMultiple * RotateX.right / (Mathf.Sqrt(h * h + v * v) + 0.0001f);
            z_Velocity = v * MoveSpeed * PlayerScale * moveMultiple * RotateX.forward / (Mathf.Sqrt(h * h + v * v) + 0.0001f);
        }
        else
        {
            x_Velocity = Vector3.zero;
            z_Velocity = Vector3.zero;
        }

        //竖直运动 - Y
        isPlayerGrounded = Player.isGrounded;
        //在地面上
        if (Input.GetKeyDown(KeyCode.Space) && isPlayerGrounded)
        {
            y_Velocity = Mathf.Sqrt(JumpHeight * PlayerScale * 2.0f * GravityValue) * transform.up;
        }
        //在空中
        else
        {
            if (y_Velocity.magnitude > MaxFallSpeed && Vector3.Dot(y_Velocity, transform.up) < 0)
                y_Velocity = MaxFallSpeed * -transform.up;
            else
                y_Velocity += GravityValue * Time.deltaTime * -transform.up;
        }

        playerVelocity = x_Velocity + y_Velocity + z_Velocity;

        Player.Move(playerVelocity * Time.deltaTime);
    }

    /// <summary>
    /// 冲刺（无限）
    /// </summary>
    public void Accelerate()
    {
        //速度系数变大
        moveMultiple = Accelerate_Multiple;
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
        moveMultiple = Accelerate_Multiple;
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
        moveMultiple = 1f;
        //视野恢复
        TargetFieldofView = Normal_Field_of_View;
        TargetFocusSize = Normal_FocusSize;
        //退出冲刺状态
        isAccelerate = false;
    }
}