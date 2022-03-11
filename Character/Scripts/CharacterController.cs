using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CharacterController : MonoBehaviour
{
    [Header("всдё©И")]
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
