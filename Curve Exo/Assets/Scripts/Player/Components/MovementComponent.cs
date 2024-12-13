using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovementComponent : MonoBehaviour
{
    public event Action<Vector2> onMove = null;
    [SerializeField] float moveSpeed = 2.0f, rotateSpeed = 50.0f;
    [SerializeField] bool sprinting = false;
    [SerializeField] Rigidbody rb = null;
    InputsComponent inputRef = null;
    float current = 0.0f, maxTime = 1.0f;
    bool jump = false;

    float MoveValue => moveSpeed * Time.deltaTime;
    float RotateValue => rotateSpeed * Time.deltaTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inputRef = GetComponent<InputsComponent>();

    }

    private void FixedUpdate()
    {
        Move();
        Rotate();

        if (jump)
            UpdateTime();
    }

    void Move()
    {
        Vector2 _value = inputRef.Move.ReadValue<Vector2>();

        float _multiplicator = sprinting && _value.y >= 0.0f ? 2.0f : 1.0f;

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

    }

    void UpdateTime()
    {
        current += Time.deltaTime;
        if (current >= maxTime)
        {
            current = 0.0f;
            jump = false;
        }
    }
    public void SetJump(bool _value) => jump = _value;
}
