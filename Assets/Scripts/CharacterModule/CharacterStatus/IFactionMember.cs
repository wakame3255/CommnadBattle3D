using System;

public enum Faction
{
    Player,
    Enemy,
    Neutral
}

public interface IFactionMember
{
    Faction Faction { get; }
}
