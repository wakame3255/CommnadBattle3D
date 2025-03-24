using System;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatusView : MonoBehaviour, IInitialize
{
    [SerializeField]
    private Text _healthText;

    [SerializeField]
    private Text _travelDistanceText;

    //行動コスト

    public void Initialize()
    {
        
    }

    public void SetCharacterState(CharacterState state)
    {
        switch(state)
        {
            case CharacterState.Stay:
                break;
            case CharacterState.Move:
                ShowUI();
                break;
            case CharacterState.End:
                HideUI();
                break;
        }
    }

    public void SetTravelDistance(float distance)
    {
        if (_travelDistanceText == null)
        {
            return;
        }

        _travelDistanceText.text = "残り移動距離 : " + distance.ToString();
    }

    private void ShowUI()
    {
        gameObject.SetActive(true);
    }

    private void HideUI()
    {
        gameObject.SetActive(false);
    }
}
