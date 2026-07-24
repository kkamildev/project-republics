

using System;
using Microsoft.Xna.Framework;

namespace project_republics.Utils.Animations;


public class EaseOutAnimation : LinearAnimation
{
    public EaseOutAnimation(float seconds, Action onFinish, float from, float to) : base(seconds, onFinish, from, to)
    {
    }

    public override float Progress
    {
        get
        {
            return MathHelper.Lerp(_from, _to, 1f - (1f - BaseProgress) * (1f - BaseProgress));
        }
    }
}