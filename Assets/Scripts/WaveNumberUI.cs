using TMPro;
using UnityEngine;

public class WaveNumberUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI waveText;

    public void SetWaveNumber(int waveNumber)
    {
        waveText.text = "Wave: " + waveNumber;
    }
}
