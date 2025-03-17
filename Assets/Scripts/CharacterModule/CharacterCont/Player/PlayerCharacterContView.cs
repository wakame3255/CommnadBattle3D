using R3;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacterContView : MonoBehaviour, IInitialize
{
    //ターン終了ボタン
    [SerializeField]
    private Button _endTurnButton = default;

    //公開ターン終了ボタン
    public Button EndTurnButton { get => _endTurnButton; }

    public void Initialize()
    {
        HideView();
    }

    /// <summary>
    /// モデルの状態に応じてビューを更新する
    /// </summary>
    /// <param name="state">状態</param>
    public void UpdateView(CharacterState state)
    {
        switch (state)
        {
            case CharacterState.Stay:
                break;
            case CharacterState.Move:
                ShowView();
                break;
            case CharacterState.End:
                HideView();
                break;
        }
    }

    /// <summary>
    /// ビューを表示する
    /// </summary>
    private void ShowView()
    {
        this.gameObject.SetActive(true);
        DebugUtility.Log("表示");
    }

    /// <summary>
    /// ビューを非表示にする
    /// </summary>
    private void HideView()
    {
        this.gameObject.SetActive(false);
        DebugUtility.Log("費用時");
    }
}