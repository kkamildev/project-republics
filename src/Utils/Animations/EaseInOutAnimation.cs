

using System;
using Microsoft.Xna.Framework;

namespace project_republics.Utils.Animations;

public class EaseInOutAnimation : LinearAnimation
{
    public EaseInOutAnimation(float seconds, Action onFinish, float from, float to) : base(seconds, onFinish, from, to)
    {
        
    }

    public override float Progress
    {
        get
        {
            return MathHelper.Lerp(_from, _to, BaseProgress * BaseProgress * (3f - 2f * BaseProgress));
        }
    }
}