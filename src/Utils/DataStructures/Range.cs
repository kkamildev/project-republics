

namespace project_republics.Utils.DataStructures;

public struct Range<T> where T : struct, System.Numerics.IComparisonOperators<T, T, bool>
{
    public T Start{get;set;}
    public T End{get;set;}

    private bool _startClosed, _endClosed;

    public Range(T start, T end)
    {
        Start = start;
        End = end;
    }

    public Range(T start, T end, bool startClosed, bool endClosed) : this(start, end)
    {
        _startClosed = startClosed;
        _endClosed = endClosed;
    }

    public readonly bool Intersects(T value)
    {
        bool startCondition, endCondition;

        if(_startClosed)
        {
            startCondition = value >= Start;
        } else
        {
            startCondition = value > Start;
        }

        if(_endClosed)
        {
            endCondition = value <= End;
        } else
        {
            endCondition = value < End;
        }

        return startCondition & endCondition; 
    }

    public bool StartClosed
    {
        get
        {
            return _startClosed;
        }
        set
        {
            _startClosed = value;
        }
    }
    public bool EndClosed
    {
        get
        {
            return _endClosed;
        }
        set
        {
            _endClosed = value;
        }
    }
}