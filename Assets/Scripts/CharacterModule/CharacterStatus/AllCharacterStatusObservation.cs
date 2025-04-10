using System.Collections.Generic;

public class AllCharacterStatus
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

    /// <summary>
    /// 自分のキャラクターの状態
    /// </summary>
    private IStatusNotice _myCharacterStatus;
    public IStatusNotice MyCharacterStatus => _myCharacterStatus;

    public AllCharacterStatus(List<IStatusNotice> allyCharacterStatus, List<IStatusNotice> enemyCharacterStatus, IStatusNotice myStatus)
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
    public AllCharacterStatus ReturnOtherStatus(IStatusNotice characterStatus)
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

        return new AllCharacterStatus(_allyCharacterStatus, _enemyCharacterStatus, characterStatus);
    }
}
