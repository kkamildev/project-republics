

using System;
using Microsoft.Xna.Framework;

namespace project_republics.Utils.Animations;

public class LinearAnimation : Animation
{
    protected readonly float _from, _to;
    public LinearAnimation(float seconds, Action onFinish, float from, float to) : base(seconds, onFinish)
    {
        _from = from;
        _to = to;
    }

    public override float Progress
    {
        get
        {
            return MathHelper.Lerp(_from, _to, base.Progress);
        }   
    }

    
}