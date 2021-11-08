using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    public static bool mainMenuLoaded = false;
    public Material shockWaveMaterial;
    public RawImage rawImage;
    public float baseTime = 0.5f;
    private Camera newCamera;

    public GameObject canvas;
    public GameObject cursor;
    private Camera camera;
    private bool shockwaveDone = false;
    private Coroutine shockwaveEffect;

    // Start is called before the first frame update
    void Start() {
        camera = gameObject.GetComponent<Camera>();
        int countLoaded = SceneManager.sceneCount;
        bool managerFound = false;

        for (int i = 0; i < countLoaded; i++) {
            Scene loadedScene = SceneManager.GetSceneAt(i);
            if (loadedScene.name == "Managers" && loadedScene.isLoaded) {
                managerFound = true;
            }
        }
        if (!managerFound) {
            SceneManager.LoadSceneAsync("Managers", LoadSceneMode.Additive);
        }
        
        mainMenuLoaded = true;
    }

    // Update is called once per frame
    void Update() {
        if (shockwaveDone) {
            shockwaveDone = false;
            mainMenuLoaded = false;
            LevelLoader.isLoading = false;
            
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Managers"));
            GameObject.FindWithTag("PlayerHealthBar").transform.GetChild(0).gameObject.SetActive(true);
            GameObject timer = GameObject.FindWithTag("Timer");
            timer.transform.GetChild(0).gameObject.SetActive(true);
            timer.GetComponent<CountdownTimer>().timerIsRunning = true;
            SceneManager.SetActiveScene(currentScene);

            StopCoroutine(shockwaveEffect);
            LevelLoader.Unload(gameObject.scene.buildIndex);
        }
    }

    public void Play() {
        cursor.SetActive(false);
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Managers"));
        AudioManager audioManager = GameObject.Find("Audio Source").GetComponent<AudioManager>();
        SceneManager.SetActiveScene(currentScene);
        audioManager.SwitchTracks();

        LevelLoader.LoadAdditive("Level1-Dungeon", SceneLoaded);
    }
    
    
    
    public void ExitGame() {
        Application.Quit();
    }

    private void SceneLoaded(Scene scene, LoadSceneMode mode) {
        camera.enabled = false;
        camera.GetComponent<AudioListener>().enabled = false;
        SceneManager.SetActiveScene(scene);

        SceneManager.sceneLoaded -= SceneLoaded;

        GameObject newSceneRoot = scene.GetRootGameObjects()[0];
        for (int i = 0; i < newSceneRoot.transform.childCount; i++) {
            GameObject child = newSceneRoot.transform.GetChild(i).gameObject;
            if (child.CompareTag("MainCamera")) {
                shockWaveMaterial.SetTexture("_SecondaryTex", child.transform.Find("Camera Current Level").GetComponent<Camera>().targetTexture);
            }
        }
        
        // camera.transform.parent.transform.position = new Vector3(0, 50, 0);
        
        Vector2 clampedScreenPos = new Vector2(0.5f, 0.5f);
        
        shockWaveMaterial.SetVector("_FocalPoint", clampedScreenPos);
        shockwaveEffect = StartCoroutine(ShockWaveEffect(clampedScreenPos));
    }

    IEnumerator ShockWaveEffect(Vector2 screenPos) {
        rawImage.enabled = true;
        
        float baseSpeed = 0.70711f / baseTime;
        shockWaveMaterial.SetFloat("_Speed", (baseSpeed * camera.aspect) + ((baseSpeed * camera.aspect) * Vector2.Distance(new Vector2(0.5f, 0.5f), screenPos)/0.70711f));
        
        float tParam = 0;
        while (tParam < baseTime) {
            tParam += Time.deltaTime;
            shockWaveMaterial.SetFloat("_TimeScale", tParam);
            yield return null;
        }
        
        shockWaveMaterial.SetFloat("_TimeScale", 0);
        // rawImage.enabled = false;
        canvas.SetActive(false);
        shockwaveDone = true;
    }
}
