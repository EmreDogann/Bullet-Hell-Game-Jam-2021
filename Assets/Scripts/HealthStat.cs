using System;
using UnityEngine;

public class HealthStat : MonoBehaviour {
    [SerializeField] public Animator animator;
    [SerializeField] private int health = 8;
    public HealthBar healthBar;
    public bool isInvulnerable = false;

    private void Start() {
        animator = GetComponentInChildren<Animator>();
    }

    public void Update() {
        if (animator != null) {
            animator.SetBool("isDead", IsDead());
        }
    }

    public void InflictDamage(int damage) {
        health -= damage;
        if(healthBar!=null)
            healthBar.SetHealth(health);
    }

    public int GetHealth() {
        return health;
    }

    public void SetHealth(int h) {
        health = h;
        if (healthBar != null) healthBar.SetHealth(h);
    }

    public bool IsDead() {
        return health <= 0;
    }
}