using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI waveUI;
    [SerializeField] TextMeshProUGUI currencyUI;

    private void OnGUI()
    {
        waveUI.text = LevelManager.main.currentWave.ToString();
        currencyUI.text = LevelManager.main.currency.ToString();
    }
}
