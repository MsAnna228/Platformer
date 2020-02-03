using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    Text _coinText;
    [SerializeField]
    Text _playerLivesText;
    [SerializeField]
    Image _playerLivesImage;

  
    public void UpdateScoreDisplay(int playerCoins)
    {
        _coinText.text = "Coins: " + playerCoins.ToString();
    }

    public void UpdateLives(float playerLives, float maxPlayerLives)
    {
        _playerLivesText.text = "Lives: " + playerLives.ToString();
        _playerLivesImage.fillAmount = playerLives / maxPlayerLives;
    }
}
