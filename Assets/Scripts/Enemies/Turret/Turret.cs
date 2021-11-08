using System.Collections;
using System.Collections.Generic;
using Timer;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public const string TargetingTimerName = "Targeting Timer";
    public const string FireTimerName = "Fire Timer";

    public float speed;
    public float delay;
    public bool isShooting;
    public Transform target;


    public float targetingDuration = 5f;
    public float firingDuration = 3f;

    public float speedReduction;
    [SerializeField] private float defDistanceRay = 100;
    public Transform laserFirePoint;

    public LineRenderer lineRenderer;

    public Transform particles;

    public GameObject endOfRay;


    public HealthStat playerHealth;

    private Timers _timers;
    private float _timers1 = 4;
    private void Awake()
    {
        _timers = gameObject.AddComponent<Timers>();
        _timers.AddTimer(TargetingTimerName, targetingDuration);
        _timers.AddTimer(FireTimerName, firingDuration);
    }

    // Start is called before the first frame update
    private void Start()
    {
        speed = 10;
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
            if (Physics2D.Raycast(transform.position, transform.up))
                if (Physics2D.Raycast(laserFirePoint.position, transform.up))
                {
                    RaycastHit2D hit = Physics2D.Raycast(laserFirePoint.position, laserFirePoint.transform.up);
                    Draw2DRay(laserFirePoint.position, hit.point);
                    particles.position = hit.point;
                    _timers1 += Time.deltaTime;
                    if (hit.collider.tag == "Player" && _timers1 >= 1.5)
                    {
                        playerHealth.InflictDamage(1);
                        _timers1 = 0;
                    }
                    endOfRay.SetActive(true);
                }
                else
                {
                    Draw2DRay(laserFirePoint.position, laserFirePoint.transform.up * defDistanceRay);
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