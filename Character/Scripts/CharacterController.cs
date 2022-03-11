using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CharacterController : MonoBehaviour
{
    [Header("��ģ��")]
    public Transform Body;
    
    void Start()
    {
        InitMotionController();

        InitCameraController();
    }

    void Update()
    {
        UpdateMovementController();

        UpdateMotionController();

        UpdateCameraController();
    }
}
