using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class ActivePool {
    /// <summary>
    /// Unique Identifier of this pool. Pass this string in your BulletManager.Spawn() to specify which type of bullet you want to spawn.
    /// </summary>
    [Tooltip("Unique Identifier of this pool. Pass this string in your BulletManager.Spawn() to specify which type of bullet you want to spawn.")]
    public string tag;

    /// <summary>
    /// The prefab to pool and spawn in the world.
    /// </summary>
    [Tooltip("The prefab to pool and spawn in the world.")]
    public GameObject prefab;

    /// <summary>
    /// The number of these bullets you want in the pool. NOTE: Pools do not grow and instead recycle the oldest bullets so set this wisely!
    /// </summary>
    [Tooltip("The number of these bullets you want in the pool. NOTE: Pools do not grow and instead recycle the oldest bullets so set this wisely!")]
    public int poolSize;

    [System.NonSerialized] public List<GameObject> activeObjects;

    public ActivePool() {
        activeObjects = new List<GameObject>();
    }

    public void Add(GameObject _gameObject) {
        activeObjects.Add(_gameObject);
    }

    public void Remove(GameObject _gameObject) {
        activeObjects.Remove(_gameObject);
    }

    public bool isFull() {
        return activeObjects.Count == poolSize;
    }
}

public class BulletManager : MonoBehaviour {
    public ActivePool[] pool;
    private static Transform parent;
    private static Dictionary<string, ActivePool> activePools;

    // Start is called before the first frame update
    void Start() {
        activePools = new Dictionary<string, ActivePool>();
        Scene currentActiveScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(gameObject.scene);
        parent = GameObject.Find("[Entities]").transform;

        foreach (ActivePool activePool in pool) {
            SimplePool.Preload(activePool.prefab, activePool.poolSize, parent);
            activePools[activePool.tag] = activePool;
        }

        SceneManager.SetActiveScene(currentActiveScene);
    }

    public static void Spawn(string tag, Vector3 pos, Quaternion rot) {
        if (activePools[tag].isFull()) {
            Despawn(tag, activePools[tag].activeObjects[0]);
        }

        activePools[tag].Add(SimplePool.Spawn(activePools[tag].prefab, pos, rot));
    }

    public static void Despawn(string tag, GameObject objectToRemove) {
        activePools[tag].Remove(objectToRemove);
        SimplePool.Despawn(objectToRemove);
    }

    public static void DespawnAll() {
        foreach (KeyValuePair<string, ActivePool> pool in activePools) {
            foreach (GameObject activeObject in pool.Value.activeObjects) {
                SimplePool.Despawn(activeObject);
            }

            pool.Value.activeObjects.Clear();
        }
    }

    public static void MoveParent(Vector3 pos) {
        parent.position = pos;
    }

    public static Dictionary<string, List<GameObject>> GetAllActiveObjects() {
        Dictionary<string, List<GameObject>> allActiveObjects = new Dictionary<string, List<GameObject>>();

        foreach (KeyValuePair<string, ActivePool> pool in activePools) {
            allActiveObjects[pool.Key] = pool.Value.activeObjects;
        }

        return allActiveObjects;
    }

    public static List<GameObject> GetActiveObjects(string tag) {
        return activePools[tag].activeObjects;
    }
}