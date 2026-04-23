using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DeathScreen : MonoBehaviour
{
    public GameObject deathCanvas;
    public TextMeshProUGUI deathText;
    public float restartDelay = 3f;

    private float timer;
    private bool isDead = false;

    void Start()
    {
        if (deathCanvas != null)
        {
            deathCanvas.SetActive(false);
        }

        if (deathText == null)
        {
            Debug.LogWarning("DeathScreen: deathText is not assigned.");
        }
    }

    void Update()
    {
        if (!isDead)
        {
            return;
        }

        timer -= Time.deltaTime;

        if (deathText != null)
        {
            deathText.text = "You Died\nRestarting in " + Mathf.Ceil(timer).ToString() + "...";
        }

        if (timer <= 0f)
        {
            RestartScene();
        }
    }

    public void TriggerDeath()
    {
        if (isDead)
        {
            return;
        }

        isDead = true;
        timer = restartDelay;

        if (deathCanvas != null)
        {
            deathCanvas.SetActive(true);
        }

        if (deathText != null)
        {
            deathText.text = "You Died\nRestarting in " + Mathf.Ceil(timer).ToString() + "...";
        }
    }

    void RestartScene()
    {
        isDead = false;
        timer = 0f;

        if (deathCanvas != null)
        {
            deathCanvas.SetActive(false);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}