using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EaseCubicCurves
{
    /// <summary>
    /// EaseInCubic function
    /// Smoothly accelerates the beginning by gradually increasing the interpolated value
    /// </summary>
    /// <param name="_t">Normalized value beetwen 0 and 1 for time progress</param>
    /// <returns>Interpolated value</returns>
    static public float EaseInCubic(float _t)
    {
        return _t * _t * _t;
    }

    /// <summary>
    /// EaseOutCubic function
    /// Smoothly decelerates the end by gradually slowing down the interpolated value.
    /// </summary>
    /// <param name="_t">Normalized value beetwen 0 and 1 for time progress</param>
    /// <returns>Interpolated value</returns>
    static public float EaseOutCubic(float _t)
    {
        return 1 - Mathf.Pow(1 - _t, 3);
    }

    /// <summary>
    /// EaseInOutCubic
    /// Combine EaseInCubic and EaseOutCubic, that starts slowly, accelerates in the middle, and then decelerates toward the end
    /// </summary>
    /// <param name="_t">Normalized value beetwen 0 and 1 for time progress</param>
    /// <returns>Interpolated value</returns>
    static public float EaseInOutCubic(float _t)
    {
        return _t < 0.5f ? 4 * _t * _t * _t : 1 - Mathf.Pow(-2 * _t + 2, 3) / 2;
    }

}
