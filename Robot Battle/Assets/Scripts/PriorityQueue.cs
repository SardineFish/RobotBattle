using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum PriorityOrder : int
{
    Ascending = 1,
    Descending = -1
}
public class PriorityQueue<PriorityT,ItemT> where PriorityT: IComparable
{
    public List<PriorityT> Keys { get; private set; }
    public List<ItemT> Values { get; private set; }
    public PriorityOrder PriorityOrder { get; private set; }
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
            for (i = 0; i < Keys.Count; i++)
            {
                if (Keys[i].CompareTo(index) * ((int)this.PriorityOrder) > 0)
                {
                    break;
                }
            }
            Keys.Insert(i, index);
            Values.Insert(i, value);
        }
    }

    public PriorityQueue(PriorityOrder priorityOrder= PriorityOrder.Ascending)
    {
        PriorityOrder = priorityOrder;
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

    public int UpdatePriority(ItemT item,PriorityT newPriority)
    {
        var idx = this.Values.IndexOf(item);
        if (idx < 0)
            return -1;
        this.Values.RemoveAt(idx);
        this.Keys.RemoveAt(idx);
        this[newPriority] = item;
        return this.Values.IndexOf(item);
    }
}