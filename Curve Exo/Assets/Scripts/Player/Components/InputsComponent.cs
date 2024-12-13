using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputsComponent : MonoBehaviour
{
    IAA_Player controls = null;

    InputAction move = null;
    InputAction rotate = null;
    InputAction jump = null;
    InputAction sprint = null;

    public InputAction Move => move;
    public InputAction Rotate => rotate;
    public InputAction Jump => jump;
    public InputAction Sprint => sprint;


    private void Awake()
    {
        controls = new IAA_Player();
    }

    private void OnEnable()
    {
        move = controls.Player.Move;
        rotate = controls.Player.Rotate;
        jump = controls.Player.Jump;
        sprint = controls.Player.Sprint;

        move.Enable();
        rotate.Enable();
        jump.Enable();
        sprint.Enable();
    }
}
