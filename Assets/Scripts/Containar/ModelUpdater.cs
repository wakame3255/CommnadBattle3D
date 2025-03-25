using System.Collections.Generic;
using VContainer;
using VContainer.Unity;

public class ModelUpdater: ITickable
{
    private List<IUpdateHandler> updateHandlers = new List<IUpdateHandler>();

    /// <summary>
    /// 更新処理の追加
    /// </summary>
    public void Tick()
    {
       
        for (int i = 0; i < updateHandlers.Count; i++)
        {
            updateHandlers[i].Updateable();
        }
    }

    /// <summary>
    /// 更新処理したいモデルの追加
    /// </summary>
    /// <param name="updateHandler"></param>
    public void AddUpdateHandler(IUpdateHandler updateHandler)
    {
        updateHandlers.Add(updateHandler);
    }
}
