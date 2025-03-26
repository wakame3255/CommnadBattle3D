using System;
using System.ComponentModel.DataAnnotations;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatusView : MonoBehaviour, IInitialize
{
    [SerializeField, Required]
    private Text _healthText;

    [SerializeField, Required]
    private Text _travelDistanceText;

    [SerializeField, Required]
    private Text _actionCost;

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

    public void SetActionCost(int actionCost)
    {
        if (_actionCost == null)
        {
            return;
        }
        _actionCost.text = "行動コスト : " + actionCost.ToString();
    }

    public void SetHealth(int health)
    {
        if (_healthText == null)
        {
            return;
        }
        _healthText.text = "HP : " + health.ToString();
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
