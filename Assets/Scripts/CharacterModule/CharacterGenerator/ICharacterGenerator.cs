using System;

public interface ICharacterGenerator
{
    /// <summary>
    /// キャラクターを生成する
    /// </summary>
    /// <returns>キャラクター情報</returns>
    public ICharacterStateHandler[] GenerateCharacter();
}