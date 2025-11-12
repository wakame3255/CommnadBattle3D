using System.Linq;
using System.Collections.Generic;

/// <summary>
/// 優先度付きキューの実装
/// A*パスファインディングで使用される優先度順のデータ構造
/// </summary>
/// <typeparam name="TElement">キューに格納する要素の型</typeparam>
/// <typeparam name="TPriority">優先度の型</typeparam>
public class PriorityQueue<TElement, TPriority>
{
    private readonly SortedList<TPriority, Queue<TElement>> _priorities = new ();

    private int _count = default;
    
    /// <summary>
    /// キュー内の要素数
    /// </summary>
    public int Count => _count;

    /// <summary>
    /// 要素を優先度付きでキューに追加
    /// </summary>
    /// <param name="element">追加する要素</param>
    /// <param name="priority">要素の優先度</param>
    public void Enqueue(TElement element, TPriority priority)
    {
        if (!_priorities.ContainsKey(priority))
        {
            _priorities[priority] = new Queue<TElement>();
        }

        _priorities[priority].Enqueue(element);
        _count++;
    }

    /// <summary>
    /// 最も優先度の高い要素をキューから取り出す
    /// </summary>
    /// <returns>取り出した要素</returns>
    public TElement Dequeue()
    {
        if (_priorities.Count == 0)
        {
            DebugUtility.Log("キューなしだよ");
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