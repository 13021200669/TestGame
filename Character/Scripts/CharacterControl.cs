using UnityEngine;

public partial class CharacterControl : MonoBehaviour
{
    //子模块
    [SerializeField] public Transform Body;
    [SerializeField] public Transform Camera_Player;

    void Start()
    {
        InitMovementController();

        InitMotionController();

        InitCameraController();
    }

    void Update()
    {
        //视角锁定时开始
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            UpdateMovementController();

            UpdateMotionController();

            UpdateCameraController();

            if (Input.GetKeyDown(KeyCode.Escape))
                Cursor.lockState = CursorLockMode.None;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

    }

    private void FixedUpdate()
    {

    }
}
