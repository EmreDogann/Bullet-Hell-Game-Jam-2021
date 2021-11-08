using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Timer;
using UnityEngine;

public class EntityData {
    // the tag that describes this entity
    private string _tag;

    // reference to the layer they are on
    private int _level;

    // reference to the amount of health they have
    private int? _health;

    // transformation data
    private Vector2 _position;
    private Quaternion _rotation;

    // timer data
    [CanBeNull] private Timers _timers;

    // constructer to initalise a new instance of Entitiy Data
    public EntityData(string tag, int level, int? health, Vector2 position, Quaternion rotation, [CanBeNull] Timers timers) {
        _tag = tag;
        _level = level;
        _health = health;
        _position = position;
        _rotation = rotation;
        _timers = timers;
    }

    public string GetTag() {
        return _tag;
    }

    public void SetTag(string tag) {
        _tag = tag;
    }

    public int GetLevel() {
        return _level;
    }

    public void SetLevel(int level) {
        _level = level;
    }

    public int? GetHealth() {
        return _health;
    }

    public void SetHealth(int? health) {
        _health = health;
    }

    public Vector2 GetPosition() {
        return _position;
    }

    public void SetPosition(Vector2 position) {
        _position = position;
    }

    public Quaternion GetRotation() {
        return _rotation;
    }

    public void SetRotation(Quaternion rotation) {
        _rotation = rotation;
    }

    [CanBeNull]
    public Timers GetTimers() {
        return _timers;
    }

    public void SetTimers([CanBeNull] Timers timers) {
        _timers = timers;
    }

    public void SetAllEnemyData(string tag, int level, int health, Vector2 position, Quaternion rotation, Timers timers) {
        _tag = tag;
        _level = level;
        _health = health;
        _position = position;
        _rotation = rotation;
        _timers = timers;
    }
}