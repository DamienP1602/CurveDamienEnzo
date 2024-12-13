using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovementComponent : MonoBehaviour
{
    public event Action<Vector2> onMove = null;
    [SerializeField] float moveSpeed = 2.0f, rotateSpeed = 50.0f, jumpForce = 3.0f;
    [SerializeField] bool sprinting = false;
    [SerializeField] Rigidbody rb = null;
    [SerializeField] LayerMask mask = 0;

    InputsComponent inputRef = null;
    AnimationComponent animRef = null;
    float currentRun = 0.0f, maxTime = 1.0f;
    bool canJump = true;

    float MoveValue => moveSpeed * Time.deltaTime;
    float RotateValue => rotateSpeed * Time.deltaTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inputRef = GetComponent<InputsComponent>();
        animRef = GetComponent<AnimationComponent>();

    }

    private void FixedUpdate()
    {
        Move();
        Rotate();

        CheckJump();
    }

    void Move()
    {
        Vector2 _value = inputRef.Move.ReadValue<Vector2>();

        float _multiplicator = 1.0f;

        if (sprinting && _value.y > 0.0f)
        {
            currentRun += Time.deltaTime;
            currentRun = currentRun >= 1.0f ? 1.0f : currentRun;
            _multiplicator += EaseCubicCurves.EaseInCubic(currentRun);
        }
        else
        {
            currentRun = 0.0f;
            sprinting = false;
        }

        onMove?.Invoke(_value * _multiplicator);

        Vector3 _forward = transform.forward * _value.y * (MoveValue * _multiplicator);
        Vector3 _right = transform.right * _value.x * MoveValue;

        rb.MovePosition(transform.position + _forward + _right);
    }

    void Rotate()
    {
        float _value = inputRef.Rotate.ReadValue<float>();
        Quaternion _rot = Quaternion.Euler(transform.eulerAngles + transform.up * _value * RotateValue);
        rb.MoveRotation(_rot);
    }

    public void ToggleSprint()
    {
        Vector2 _value = inputRef.Move.ReadValue<Vector2>();
        if (_value.y <= 0.0f) return;

        sprinting = !sprinting;
    }

    public void Jump()
    {
        if (!canJump) return;
        jumpAnim();


        rb.AddForce(0.0f, 50.0f * jumpForce, 0.0f);
        Invoke(nameof(jumpAnim), 0.8f);

    }

    private void jumpAnim()
    {
        animRef.UpdateJumpParam();
    }

    void CheckJump()
    {
        Ray _ray = new Ray(transform.position + transform.up * 0.1f, -transform.up);
        bool _hit = Physics.Raycast(_ray,0.2f,mask);
        canJump = _hit;

        //if (_hit)
        //    Debug.Log("GROUND");
        //else
        //    Debug.Log("AIR");

        //Debug.DrawRay(_ray.origin, _ray.direction * 0.2f);
    }
}
