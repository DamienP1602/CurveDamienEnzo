using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputsComponent)), RequireComponent(typeof(MovementComponent)), RequireComponent(typeof(AnimationComponent))]
public class Player : MonoBehaviour
{
    [SerializeField] MovementComponent movement = null;
    [SerializeField] InputsComponent inputs = null;
    [SerializeField] AnimationComponent anims = null;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Init()
    {
        movement = GetComponent<MovementComponent>();
        inputs = GetComponent<InputsComponent>();
        anims = GetComponent<AnimationComponent>();

        movement.onMove += anims.UpdateMoveParam;
        inputs.Sprint.performed += (e) => movement.ToggleSprint();
        inputs.Jump.performed += (e) => movement.SetJump(true);
    }
}
