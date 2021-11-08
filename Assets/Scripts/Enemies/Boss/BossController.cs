using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {
    private Transform playerTransform;
    private Transform bossTransform;
    private MovementController _controller;
    public float speed;
    private Animator _animator;
    private static readonly int IsWalking = Animator.StringToHash("isWalking");

    // Start is called before the first frame update
    void Start() {
        _animator = GetComponentInChildren<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _controller = gameObject.AddComponent<MovementController>();
    }

    // Update is called once per frame
    void Update() {
        _controller.MoveTo(playerTransform, speed);
        _animator.SetBool(IsWalking, true);
    }
}