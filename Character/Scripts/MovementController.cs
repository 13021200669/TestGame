using UnityEngine;

public partial class CharacterController : MonoBehaviour
{
    [Header("运动参数")]
    [Label("移动速度")] public float MoveSpeed = 10;
    [Label("冲刺倍率")] public float Accelerate_Multiple = 3f;
    [Label("冲刺时间")] public float Accelerate_Time = 1f;

    private float MoveMultiple = 1f;
    private bool isAccelerate = false;

    [Label("跳跃力")] public float JumpForce = 100;

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
        if (!isAccelerate && (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))) Accelerate();

        if (Input.GetKeyDown(KeyCode.Space)) Jump();

        Move();
    }

    /// <summary>
    /// 常规移动
    /// </summary>
    public void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        transform.position += (RotateX.right * x) * Time.deltaTime * (MoveMultiple * MoveSpeed);
        transform.position += (RotateX.forward * z) * Time.deltaTime * (MoveMultiple * MoveSpeed);
    }

    /// <summary>
    /// 冲刺
    /// </summary>
    public void Accelerate()
    {
        //速度系数变大
        MoveMultiple = Accelerate_Multiple;
        //视野拉远
        TargetFieldofView = Accelerate_Field_of_View;
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
        //退出冲刺状态
        isAccelerate = false;
    }

    /// <summary>
    /// 跳跃
    /// </summary>
    public void Jump()
    {
        GetComponent<Rigidbody>().AddForce(RotateX.up * JumpForce);
    }
}
