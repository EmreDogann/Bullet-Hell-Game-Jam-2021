using System.Collections;
using System.Collections.Generic;
// to use list.Last() and Remove with custom function, it is optimised for C# based on:
// https://stackoverflow.com/questions/1246918/how-can-i-find-the-last-element-in-a-list
using System.Linq;
using Timer;
using UnityEngine;

public class EntitySpawner : MonoBehaviour {
    private static List<EntityData> _allData;

    public static void SpawnEntitiesInLevel(int level) {
        BulletManager.DespawnAll();
        foreach (EntityData data in _allData) {
            // if it is not the same level then go to the next item in the list
            if (data.GetLevel() != level) {
                continue;
            }

            // spawning
            // setting position and rotation
            BulletManager.Spawn(data.GetTag(), data.GetPosition(), data.GetRotation());

            // get the recently spawned object
            // Last uses Linq
            var activeObject = BulletManager.GetActiveObjects(data.GetTag()).Last();

            // set health
            // there is a check as some entities will not have health
            if (activeObject.GetComponent<HealthStat>() != null)
                activeObject.GetComponent<HealthStat>().SetHealth((int) data.GetHealth());
            

            // set timers
            // if the timers component is not null, set the timers to the ones saved in the data
            if (activeObject.GetComponent<Timers>() != null)
                activeObject.GetComponent<Timers>().SetTimersFromSave(data.GetTimers());
        }
    }

    // NOTE: there could be a bunch of issues if the references to the timer
    // is used multiple times, which it shouldn't
    // be but that could cause an issue
    // save level state
    public static void SaveEntitiesInLevel(int level) {
        // need to get rid of the currently saved things to save the new ones in
        if (_allData != null) {
            RemoveLevel(level);
        } else {
            _allData = new List<EntityData>();
        }

        // go through active objects and add them to the all data
        Dictionary<string, List<GameObject>> allActiveObjects = BulletManager.GetAllActiveObjects();
        foreach (var activeObjectGroup in allActiveObjects) {
            string tag = activeObjectGroup.Key;

            // go through the game objects of the particular tag
            foreach (var activeObject in activeObjectGroup.Value) {
                // get health
                int? health = null;
                if (activeObject.GetComponent<HealthStat>() != null) {
                    // so if the game object is dead, then it shall not be added to the scene.
                    // this is just a precaution as dead enemies should not be in activeObjects
                    if (activeObject.GetComponent<HealthStat>().IsDead()) {
                        continue;
                    }
                    health = activeObject.GetComponent<HealthStat>().GetHealth();
                }
                
                // get position
                Vector2 position = activeObject.transform.position;
                // get rotation
                Quaternion rotation = activeObject.transform.rotation;


                Timers? timers = null;
                // get timers
                if (activeObject.GetComponent<Timers>() != null) {
                    timers = activeObject.GetComponent<Timers>();
                }

                var entityData = new EntityData(tag, level, health, position, rotation, timers);
                _allData.Add(entityData);
            }
        }
    }

    // remove all from the all data list the match the given level
    private static void RemoveLevel(int level) {
        // uses Linq command
        // _allData.Remove(_allData.Single(data => data.GetLevel() == level));
        _allData.RemoveAll(data => data.GetLevel() == level);
    }

    public static bool IsListEmpty(int level) {
        return _allData.FindAll(data => data.GetLevel() == level).Count == 0;
    }

    public static bool IsNull() {
        return _allData == null;
    }

    public static void Clear() {
        _allData = null;
    }
}