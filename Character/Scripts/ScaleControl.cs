using UnityEngine;


public partial class CharacterControl : MonoBehaviour
{
    //缩小模块
    [SerializeField] public float ShrinkScale = 0.1f;
    [SerializeField] public float ShrinkTime = 0.5f;

    private float PlayerScale = 1f;

    /// <summary>
    /// 运动初始化
    /// </summary>
    public void InitScaleController()
    {

    }

    /// <summary>
    /// Update - 运动控制
    /// </summary>
    public void UpdateScaleController()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (PlayerScale == 1)
            {
                PlayerScale = ShrinkScale;
                Player.stepOffset *= (ShrinkScale / 1);
            }
            else
            {
                PlayerScale = 1;
                Player.stepOffset /= (ShrinkScale / 1);
            }
        }

        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(PlayerScale, PlayerScale, PlayerScale), Time.deltaTime / ShrinkTime);
    }
}