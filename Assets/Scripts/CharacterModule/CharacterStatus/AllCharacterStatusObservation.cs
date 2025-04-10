using System.Collections.Generic;

public class OtherCharacterStatus
{
    /// <summary>
    /// 味方キャラクターの状態
    /// </summary>
    private List<IStatusNotice> _allyCharacterStatus = new List<IStatusNotice>();
    public List<IStatusNotice> AllyCharacterStatus => _allyCharacterStatus;

    /// <summary>
    /// 敵キャラクターの状態
    /// </summary>
    private List<IStatusNotice> _enemyCharacterStatus = new List<IStatusNotice>();
    public List<IStatusNotice> EnemyCharacterStatus => _enemyCharacterStatus;

    public OtherCharacterStatus(List<IStatusNotice> allyCharacterStatus, List<IStatusNotice> enemyCharacterStatus)
    {
        _allyCharacterStatus = allyCharacterStatus;
        _enemyCharacterStatus = enemyCharacterStatus;
    }
}

public class AllCharacterStatusObservation
{
    private List<IStatusNotice> _allCharacterStatus = new List<IStatusNotice>();

    public void AddCharacterStatus(IStatusNotice characterStatus)
    {
        _allCharacterStatus.Add(characterStatus);
    }

    /// <summary>
    /// 自分以外のキャラクターの状態を取得
    /// </summary>
    /// <returns></returns>
    public OtherCharacterStatus ReturnOtherStatus(IStatusNotice characterStatus)
    {
        List<IStatusNotice> _allyCharacterStatus = new List<IStatusNotice>();
        List<IStatusNotice> _enemyCharacterStatus = new List<IStatusNotice>();

        Faction myFaction = characterStatus.Faction;

        for (int i = 0; i < _allCharacterStatus.Count; i++)
        {
            if (_allCharacterStatus[i].Faction == myFaction)
            {
                _allyCharacterStatus.Add(_allCharacterStatus[i]);
            }
            else
            {
                _enemyCharacterStatus.Add(_allCharacterStatus[i]);
            }
        }

        return new OtherCharacterStatus(_allyCharacterStatus, _enemyCharacterStatus);
    }
}
