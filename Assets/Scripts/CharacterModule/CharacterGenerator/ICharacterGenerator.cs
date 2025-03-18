using System;
using System.Collections.Generic;

public interface ICharacterGenerator
{
    /// <summary>
    /// キャラクターを生成する
    /// </summary>
    /// <returns>キャラクター情報</returns>
    public List<ICharacterStateHandler> GenerateCharacter();
}