using System;
using Pathfinding;
using UnityEngine;
using Random = UnityEngine.Random;

public class MovementController : MonoBehaviour {
    private RandomPositionGenerator _randomPositionGenerator;
    private Vector2 _randomDirection;

    private AIPath _aiPath;
    private Seeker _seeker;
    private AIDestinationSetter _destinationSetter;

    private GameObject _randomGameObject;

    public Rigidbody2D rb;
    private bool _isRandomGameObjectNull;

    public void Start() {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        // check if the neccessary A* path finding components are attached
        // if not, then attach them
        // lerp
        _aiPath = GetComponent<AIPath>();
        // if (_aiPath == null)
        //     _aiPath = gameObject.AddComponent<AIPath>();
        // seeker
        _seeker = GetComponent<Seeker>();
        // if (_seeker == null)
        //     _seeker = gameObject.AddComponent<Seeker>();
        // destination setter
        _destinationSetter = GetComponent<AIDestinationSetter>();
        // if (_destinationSetter == null)
        //     _destinationSetter = gameObject.AddComponent<AIDestinationSetter>();

        _randomPositionGenerator = new RandomPositionGenerator();
        // InvokeRepeating(nameof(GenRandomDirection), 0.0f, 4.0f);
        if (_aiPath != null) {
            _randomGameObject = new GameObject("RandomPosition");
            _randomGameObject.transform.position = transform.position;
            InvokeRepeating(nameof(GenRandomPosition), 0.0f, 4.0f);
        }
        else {
            _isRandomGameObjectNull = true;
        }
    }

    // with a given direction will just move the rigid body straight towards that direction
    public void MoveTowards(Vector2 direction, float speed) {
        rb.MovePosition(rb.position + direction.normalized * (speed * Time.fixedDeltaTime));
    }

    public void MoveTo(Transform target, float speed) {
        _destinationSetter.target = target;
        _aiPath.maxSpeed = speed;
    }

    public void MoveTo(Transform target) {
        _destinationSetter.target = target;
    }

    // Moves towards random direction
    public void MoveRandom(float speed, bool negative = false) {
        if (!negative)
            rb.MovePosition(rb.position + _randomDirection * (speed * Time.fixedDeltaTime));
        if (negative)
            rb.MovePosition(rb.position - _randomDirection * (speed * Time.fixedDeltaTime));
    }

    // Using A* Pathfinding
    public void MoveRandom() {
        _destinationSetter.target = _randomGameObject.transform;
    }

    private void GenRandomDirection() {
        _randomDirection = _randomPositionGenerator.GetRandomDirection();
    }

    private void GenRandomPosition() {
        // if (!_isRandomGameObjectNull) {
        // https://arongranberg.com/astar/documentation/dev_4_3_8_84e2f938/old/wander.php
        GraphNode randomNode;
        // For grid graphs
        var grid = AstarPath.active.data.gridGraph;
        randomNode = grid.nodes[Random.Range(0, grid.nodes.Length)];
        // Use the center of the node as the destination for example
        var randomNodePosition = (Vector3) randomNode.position;
        _randomGameObject.transform.position = randomNodePosition;
        // }
        // else {
        //     Debug.Log(transform.root.gameObject.name+": Random Position not generated");
        // }
    }

    private void OnEnable() {
        if (gameObject.scene.name != "Managers") {
            _randomGameObject = new GameObject("RandomPosition");
            _randomGameObject.transform.position = transform.position;
        }
    }

    private void OnDisable() {
        CancelInvoke();
    }
}