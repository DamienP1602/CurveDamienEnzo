using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationComponent : MonoBehaviour
{
    [SerializeField] Animator anim = null;
    [SerializeField] float damp = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void UpdateMoveParam(Vector2 _param)
    {
        anim.SetFloat(AnimationParameter.FORWARD,_param.x, damp,Time.deltaTime);
        anim.SetFloat(AnimationParameter.RIGHT, _param.y, damp, Time.deltaTime);
    }

    public void UpdateJumpParam()
    {
        anim.SetTrigger(AnimationParameter.JUMP);
    }
}
