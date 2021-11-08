using System.Collections;
using System.Collections.Generic;
using Timer;
using UnityEngine;

public class BossTurret : MonoBehaviour
{
    public const string TargetingTimerName = "BossTargeting Timer";
    public const string FireTimerName = "BossFire Timer";
    public float delay;
    public bool isShooting;
    public Transform target;

    public float targetingDuration = 5f;
    public float firingDuration = 3f;
    [SerializeField] private float defDistanceRay = 100;
    public Transform laserFirePoint;

    public LineRenderer lineRenderer;

    public Transform particles;

    public GameObject endOfRay;

    public HealthStat playerHealth;

    private Timers _timers;
    private void Awake()
    {
        _timers = gameObject.AddComponent<Timers>();
        _timers.AddTimer(TargetingTimerName, targetingDuration);
        _timers.AddTimer(FireTimerName, firingDuration);
    }

    // Start is called before the first frame update
    private void Start()
    {
        isShooting = false;
        var player = GameObject.FindWithTag("Player");
        target = player.transform;
        playerHealth = player.GetComponent<HealthStat>();
    }


    private void Update()
    {
    }

    public void Shoot(bool isActive)
    {
        if (isActive)
        {
            lineRenderer.enabled = true;
            if (Physics2D.Raycast(transform.position, transform.right))
                if (Physics2D.Raycast(laserFirePoint.position, transform.right))
                {
                    RaycastHit2D hit = Physics2D.Raycast(laserFirePoint.position, laserFirePoint.transform.right);
                    Draw2DRay(laserFirePoint.position, hit.point);
                    particles.position = hit.point;
                    if (hit.collider.tag == "Player")
                    {
                        playerHealth.InflictDamage(1);
                    }
                    endOfRay.SetActive(true);
                }
                else
                {
                    Draw2DRay(laserFirePoint.position, laserFirePoint.transform.right * defDistanceRay);
                    endOfRay.SetActive(false);
                }
        }
        else
        {
            lineRenderer.enabled = false;
            endOfRay.SetActive(false);
        }
    }

    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }
}