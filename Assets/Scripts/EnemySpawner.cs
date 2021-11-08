using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pathfinding;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public int enemyCount = 5;

    // Start is called before the first frame update
    void Start() {
        if (EntitySpawner.IsNull() || EntitySpawner.IsListEmpty(gameObject.scene.buildIndex)) {
            StartCoroutine(SpawnEnemies());
        }
    }

    IEnumerator SpawnEnemies() {
        // Wait x frames.
        for (int x = 0; x < 2; x++) {
            yield return null;
        }
        
        GraphNode[] randomNodes = new GraphNode[enemyCount];
        // For grid graphs
        var grid = AstarPath.active.data.gridGraph;
        // string[] enemyTypes = {"Machine Gunner", "Prowler", "Turret", "Slime"};
        string[] enemyTypes = {"Machine Gunner", "Prowler"};
        int i = 0;
        while (i < enemyCount) {
            GraphNode node = grid.nodes[Random.Range(0, grid.nodes.Length)];
            while (randomNodes.Contains(node) || !node.Walkable)
            {
                
                node = grid.nodes[Random.Range(0, grid.nodes.Length)];
            }
            
            randomNodes[i] = node;

            int randNum = Random.Range(0, enemyTypes.Length);
            BulletManager.Spawn(enemyTypes[randNum], randomNodes[i].RandomPointOnSurface(), Quaternion.identity);
            i++;
        }
    }

    // Update is called once per frame
    void Update() { }
}