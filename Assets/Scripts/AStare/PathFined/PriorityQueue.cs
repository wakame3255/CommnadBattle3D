using System.Linq;
using System.Collections.Generic;

public class PriorityQueue<TElement, TPriority>
{
    private readonly SortedList<TPriority, Queue<TElement>> _priorities = new ();

    private int _count = default;
    public int Count => _count;

    public void Enqueue(TElement element, TPriority priority)
    {
        if (!_priorities.ContainsKey(priority))
        {
            _priorities[priority] = new Queue<TElement>();
        }

        _priorities[priority].Enqueue(element);
        _count++;
    }

    public TElement Dequeue()
    {
        if (_priorities.Count == 0)
        {
            DebugUtility.Log("ÉLÉÖÅ[Ç»Ç¢ÇÊ");
        }

        KeyValuePair<TPriority, Queue<TElement>> firstPair = _priorities.First();
        TElement element = firstPair.Value.Dequeue();

        if (firstPair.Value.Count == 0)
        {
            _priorities.Remove(firstPair.Key);
        }
      
        _count--;
        return element;
    }
}