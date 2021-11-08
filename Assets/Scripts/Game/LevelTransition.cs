using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelTransition : MonoBehaviour {
    public Material shockWaveMaterial;
    public RawImage rawImage;
    public float baseTime = 0.5f;
    private Camera newCamera;
    public AstarPath aStar;

    private Camera camera;
    public GameObject player;
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
    }

    // Update is called once per frame
    void Update() {
        // if (!LevelLoader.isLoading && !LevelLoader.isUnloading && !PauseMenu.gameIsPaused) {
        //     int index = gameObject.scene.buildIndex;
        //     if (index != 3) {
        //         index++;
        //     } else {
        //         // Loop back to level 1.
        //         index = 1;
        //     }
        //
        //     aStar.gameObject.SetActive(false);
        //     LevelLoader.LoadAdditive(index, SceneLoaded);
        // }
        //
        if (shockwaveDone) {
            shockwaveDone = false;
            StopCoroutine(shockwaveEffect);
            LevelLoader.isLoading = false;
        
            BulletManager.MoveParent(new Vector3(0, 0, 0));
        
            LevelLoader.Unload(gameObject.scene.buildIndex);
        }
    }

    public void nextLevel() {
        if (!LevelLoader.isLoading && !LevelLoader.isUnloading && !PauseMenu.gameIsPaused) {
            int index = gameObject.scene.buildIndex;
            if (index != 4) {
                index++;
            } else {
                // Loop back to level 1.
                index = 1;
            }

            aStar.gameObject.SetActive(false);
            LevelLoader.LoadAdditive(index, SceneLoaded);

            
        }
    }

    private void SceneLoaded(Scene scene, LoadSceneMode mode) {
        camera.enabled = false;
        camera.GetComponent<AudioListener>().enabled = false;

        EntitySpawner.SaveEntitiesInLevel(gameObject.scene.buildIndex);

        SceneManager.sceneLoaded -= SceneLoaded;
        SceneManager.SetActiveScene(scene);

        EntitySpawner.SpawnEntitiesInLevel(scene.buildIndex);

        GameObject newSceneRoot = scene.GetRootGameObjects()[0];
        for (int i = 0; i < newSceneRoot.transform.childCount; i++) {
            GameObject child = newSceneRoot.transform.GetChild(i).gameObject;
            if (child.CompareTag("Player")) {
                child.transform.position = player.transform.position;
                child.transform.rotation = player.transform.rotation;
                child.GetComponent<HealthStat>().healthBar = player.GetComponent<HealthStat>().healthBar;
                child.GetComponent<HealthStat>().SetHealth(player.GetComponent<HealthStat>().GetHealth());
                player.SetActive(false);
            } else if (child.CompareTag("MainCamera")) {
                shockWaveMaterial.SetTexture("_SecondaryTex", child.transform.Find("Camera Current Level").GetComponent<Camera>().targetTexture);
            }
        }

        camera.transform.parent.transform.position = new Vector3(0, 50, 0);
        // BulletManager.MoveParent(new Vector3(0, 50, 0));

        // This right here is bullshit.
        // EditorSceneManager.SetSceneCullingMask(scene, newSceneRoot.sceneCullingMask);

        // Vector4 screenPos = camera.ScreenToViewportPoint(Input.mousePosition);
        Vector4 screenPos = camera.WorldToViewportPoint(player.transform.position);
        Vector2 clampedScreenPos = new Vector2(Mathf.Clamp(screenPos.x, 0, 1), Mathf.Clamp(screenPos.y, 0, 1));

        shockWaveMaterial.SetVector("_FocalPoint", clampedScreenPos);
        shockwaveEffect = StartCoroutine(ShockWaveEffect(clampedScreenPos));
    }

    IEnumerator ShockWaveEffect(Vector2 screenPos) {
        rawImage.enabled = true;

        float baseSpeed = 0.70711f / baseTime;
        shockWaveMaterial.SetFloat("_Speed", (baseSpeed * camera.aspect) + ((baseSpeed * camera.aspect) * Vector2.Distance(new Vector2(0.5f, 0.5f), screenPos) / 0.70711f));

        float tParam = 0;
        while (tParam < baseTime) {
            tParam += Time.deltaTime;
            shockWaveMaterial.SetFloat("_TimeScale", tParam);
            yield return null;
        }

        shockWaveMaterial.SetFloat("_TimeScale", 0);
        rawImage.enabled = false;
        shockwaveDone = true;
    }
}