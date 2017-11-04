using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PriorityQueue<PriorityT,ItemT> where PriorityT: IComparable
{
    public List<PriorityT> Keys { get; private set; }
    public List<ItemT> Values { get; private set; }
    public int Count
    {
        get
        {
            return Keys.Count;
        }
    }
    public ItemT this[PriorityT index]
    {
        set
        {
            var i = 0;
            for(i = 0; i < Keys.Count; i++)
            {
                if (Keys[i].CompareTo(index) > 0)
                {
                    break;
                }
            }
            Keys.Insert(i, index);
            Values.Insert(i, value);
        }
    }

    public PriorityQueue()
    {
        Keys = new List<PriorityT>();
        Values = new List<ItemT>();
    }

    public ItemT RemoveAt(int idx)
    {
        var item = Values[idx];
        Keys.RemoveAt(idx);
        Values.RemoveAt(idx);
        return item;
    }

    public void Add(PriorityT key, ItemT value)
    {
        this[key] = value;
    }
}