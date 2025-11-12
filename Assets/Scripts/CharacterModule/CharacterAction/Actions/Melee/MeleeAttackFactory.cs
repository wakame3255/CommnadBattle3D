using System;
using UnityEngine;

/// <summary>
/// 近接攻撃のファクトリークラス
/// 近接攻撃のMVPコンポーネントを生成
/// </summary>
public class MeleeAttackFactory : ActionFactoryBase
{
    [SerializeField] 
    private MeleeAttackView _view;

    /// <summary>
    /// 近接攻撃アクションを生成
    /// </summary>
    /// <param name="parent">生成先の親Transform</param>
    /// <param name="attackService">攻撃処理サービス</param>
    /// <returns>生成されたアクションのMVPデータ</returns>
    public override ActionMVPData CreateAction(Transform parent, IAttackHandler attackService)
    {
        MeleeAttackView view = Instantiate(_view, parent);

        MeleeAttackModel model = new MeleeAttackModel(attackService);

         new ActionPresenter(model, view).Bind();

        return new ActionMVPData(model, view);
    }
}