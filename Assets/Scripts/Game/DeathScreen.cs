using UnityEngine;

public class DeathScreen : MonoBehaviour {
    public static float desiredAlpha = 0f;
    private float currentAlpha = 0f;
    public float speed = 2f;
    private CanvasGroup _canvasGroup;
    public GameObject deathScreenUI;

    private void Start() {
        _canvasGroup = deathScreenUI.GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update() {
        if (currentAlpha != desiredAlpha) {
            if (!deathScreenUI.activeSelf) {
                deathScreenUI.SetActive(true);
                PauseMenu.gameIsPaused = true;
            }
            currentAlpha = Mathf.MoveTowards( currentAlpha, desiredAlpha, speed * Time.unscaledDeltaTime);
            _canvasGroup.alpha = currentAlpha;
        }
    }
    
    public void LoadMenu() {
        Time.timeScale = 1f;
        desiredAlpha = 0f;
        PauseMenu.gameIsPaused = false;
        EntitySpawner.Clear();
        LevelLoader.LoadSingle("MainMenu");
    }
    
    public void QuitGame() {
        Application.Quit();
    }
}