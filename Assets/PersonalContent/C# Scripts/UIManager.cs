using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text gemsText;

    private void OnEnable()
    {
        UpdateGemsUI(0);
        ScoreManager.OnGemCollected += UpdateGemsUI;
    }

    private void OnDisable()
    {
        ScoreManager.OnGemCollected -= UpdateGemsUI;
    }

    private void UpdateGemsUI(int totalGems)
    {
        gemsText.text = "Collected Gems: " + totalGems;
    }
}
