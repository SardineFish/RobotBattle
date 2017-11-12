using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Set<T> : List<T>
{
    public new bool Add(T item)
    {
        if (Contains(item))
            return false;
        base.Add(item);
        return true;
    }
}