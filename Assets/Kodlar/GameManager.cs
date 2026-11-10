using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

// Ana menu, 60 saniyelik sayac, skor, ESC menusu ve oyun sonu panelini yonetir.
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float gameTime = 60f;
    public int totalBalls = 10;

    public Text timerText;
    public Text scoreText;
    public GameObject hud;          // sure ve gol gostergeleri
    public GameObject startPanel;   // ana menu
    public GameObject howToPanel;   // nasil oynanir
    public GameObject pausePanel;   // ESC menusu
    public GameObject endPanel;
    public Text endText;
    public GameObject quitButton;   // WebGL'de cikis olmadigi icin gizlenir
    public CameraFollow cameraFollow;

    // "Tekrar Oyna" ile sahne yeniden yuklenince ana menu atlansin
    private static bool skipMenu;

    private float timeLeft;
    private int ballsIn;
    private bool playing;
    private bool paused;
    private bool gameOver;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // Kureler cukur ustundeki gorunmez duvardan gecsin, karakter gecemesin
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Ball"), LayerMask.NameToLayer("PlayerBlocker"));

        timeLeft = gameTime;
        UpdateUI();
        endPanel.SetActive(false);
        pausePanel.SetActive(false);
        howToPanel.SetActive(false);
#if UNITY_WEBGL
        quitButton.SetActive(false);
#endif

        if (skipMenu)
        {
            skipMenu = false;
            StartGame();
        }
        else
        {
            startPanel.SetActive(true);
            hud.SetActive(false);
            cameraFollow.orbit = true;
            Time.timeScale = 0f;
        }
    }

    // Ana menudeki "Oyna" butonu
    public void StartGame()
    {
        startPanel.SetActive(false);
        howToPanel.SetActive(false);
        hud.SetActive(true);
        cameraFollow.StopOrbit();
        playing = true;
        Time.timeScale = 1f;
    }

    void Update()
    {
        Keyboard kb = Keyboard.current;
        if (kb != null && kb.escapeKey.wasPressedThisFrame)
        {
            if (howToPanel.activeSelf) ShowHowTo(false);
            else if (playing && !gameOver) SetPaused(!paused);
        }

        if (!playing || paused || gameOver) return;

        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0f)
        {
            timeLeft = 0f;
            EndGame(false);
        }
        UpdateUI();
    }

    void SetPaused(bool value)
    {
        paused = value;
        pausePanel.SetActive(value);
        Time.timeScale = value ? 0f : 1f;
    }

    // ESC menusundeki "Devam Et" butonu
    public void ResumeGame()
    {
        SetPaused(false);
    }

    // Ana menudeki "Nasil Oynanir" ve paneldeki "Tamam" butonlari
    public void ShowHowTo(bool show)
    {
        howToPanel.SetActive(show);
        startPanel.SetActive(!show);
    }

    public void BallScored()
    {
        if (gameOver) return;

        ballsIn++;
        UpdateUI();
        if (ballsIn >= totalBalls)
        {
            EndGame(true);
        }
    }

    void UpdateUI()
    {
        timerText.text = "Süre: " + Mathf.CeilToInt(timeLeft);
        timerText.color = timeLeft <= 10f ? new Color(1f, 0.35f, 0.3f) : Color.white; // son 10 saniye kirmizi
        scoreText.text = "Gol: " + ballsIn + " / " + totalBalls;
    }

    void EndGame(bool won)
    {
        gameOver = true;
        Time.timeScale = 0f;
        endPanel.SetActive(true);

        if (won)
            endText.text = "Mükemmel Antrenman!\nKadroya Girdin!";
        else
            endText.text = "Antrenman Bitti!\n" + ballsIn + " / " + totalBalls + " gol";
    }

    // "Tekrar Oyna" butonlari: menuye ugramadan yeniden baslar
    public void RestartGame()
    {
        skipMenu = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // "Ana Menu" butonlari
    public void GoToMainMenu()
    {
        skipMenu = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Ana menudeki "Cik" butonu (sadece Windows surumunde)
    public void QuitGame()
    {
        Application.Quit();
    }
}
