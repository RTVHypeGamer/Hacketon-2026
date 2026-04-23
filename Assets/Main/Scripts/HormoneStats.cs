using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HormoneStats : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public GameObject hormoneCanvas;
    public TextMeshProUGUI hormoneText;

    void Update()
    {
        if (hormoneText != null)        {
            hormoneText.text = "Dopamine: " + playerMovement.dopamineLevel.ToString("F1") + "\n" +
                               "Cortisol: " + playerMovement.cortisolLevel.ToString("F1") + "\n" +
                               "Endorphins: " + playerMovement.endorphinLevel.ToString("F1") + "\n" +
                               "Adrenaline: " + playerMovement.adrenalineLevel.ToString("F1") + "\n" +
                               "Testosterone: " + playerMovement.testosteroneLevel.ToString("F1") + "\n" +
                               "Oxytocin: " + playerMovement.oxytocinLevel.ToString("F1");
        }
    }
}
