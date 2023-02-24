using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputMan : MonoBehaviour
{
    public static Action<InputValue> MoveAction;
    public static Action JumpAction;
    public static Action InteractAction;

    public static bool blockMove;
    public static bool blockJump;
    public static bool blockInteract;

    private void Awake()
    {
        MoveAction = null;
        JumpAction = null;
        InteractAction = null;
    }

    public void OnMove(InputValue value)
    {
        if (!blockMove)
            MoveAction?.Invoke(value);
    }

    public void OnJump()
    {
        if (!blockJump)
            JumpAction?.Invoke();
    }

    public void OnInteract()
    {
        if (!blockInteract)
            InteractAction?.Invoke();
    }
}
