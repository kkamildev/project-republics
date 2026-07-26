

using System;
using Microsoft.Xna.Framework;

namespace project_republics.Utils.Animations;

public class SinAnimation : Animation
{
    private float _maxValue;
    public SinAnimation(float seconds, float maxValue, Action onFinish) : base(seconds, onFinish)
    {
        _maxValue = maxValue;
    }

    public override float Progress
    {
        get
        {
            return (float)Math.Sin(MathHelper.ToRadians(360 * base.Progress)) * _maxValue;
        }
    }
}