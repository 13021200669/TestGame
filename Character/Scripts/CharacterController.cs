using UnityEngine;

public partial class CharacterController : MonoBehaviour
{
    [Header("��ģ��")]
    [Label("������Ƥ")] public Transform Body;
    [Label("��������")] public Transform Camera_Player;

    void Start()
    {
        InitMovementController();

        InitMotionController();

        InitCameraController();
    }

    void Update()
    {
        //�ӽ�����ʱ��ʼ
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
