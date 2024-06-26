using UnityEngine;

[RequireComponent(typeof(PlayerInputReader))]
public class PlayerAttacks : PowerUpComponent
{
    
    // Variable to store projectile prefab
    [Header("Cannon attack")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float fireCooldown;
    private int nextCannonIndex = 0;
    private float nextTimeToFire;
    [Header("Laser attack")]
    [SerializeField] private float rayLength = 60f;
    [SerializeField] private LayerMask destructiblesLayer;
    [SerializeField] private GameObject laserGFX;
    [SerializeField] private GameObject[] cannonPositions;
    [SerializeField] private bool showRaycast;
    private PlayerInputReader attackInput;
    private bool hasShotLaser;

    
    
    private void Start() {
        attackInput = GetComponent<PlayerInputReader>();
    }

    //FixedUpdate is called every fixed frame-rate frame.
    override protected void Update()
    {
        if (IsFiring() && powerUpEngaged)
        {
            AudioManager.PlaySFX?.Invoke(SFXName.ShootLaser, instanceID);
            hasShotLaser = true;
            FireRayWeapon();
        } else if (IsFiring()) {
            Fire();
        } else if (!IsFiring() && hasShotLaser) {
            AudioManager.StopSFX?.Invoke(SFXName.ShootLaser, instanceID);
            hasShotLaser = false;
        }

        laserGFX.SetActive(IsFiring() && powerUpEngaged);

        base.Update();
    }

    private bool IsFiring() => attackInput.IsFiring;

    // Fire one or multiple projectiles
    void Fire()
    {
        if (Time.unscaledTime > nextTimeToFire)
        {
            //Alternate cannons from which to fire from
            nextCannonIndex++;

            if (nextCannonIndex == cannonPositions.Length)
            {
                nextCannonIndex = 0;
            }
            
            Vector2 nextFirePos = cannonPositions[nextCannonIndex].transform.position;
            
            // Instantiate new projectile
            Instantiate(projectilePrefab, nextFirePos, transform.rotation);
            //Play SFX
            AudioManager.PlaySFX(SFXName.ShootProjectile, instanceID);

            nextTimeToFire = Time.unscaledTime + fireCooldown;
        }  
    }

    private void FireRayWeapon(){

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, rayLength, destructiblesLayer);

        if(hit == true) {
            laserGFX.GetComponent<RayController>().SetUpLinePositions(transform.position, hit.point);      
            hit.transform.gameObject.GetComponent<BaseDestructibleObject>().TakeDamage(2);
        } else {
            laserGFX.GetComponent<RayController>().SetUpLinePositions(transform.position, transform.position + transform.up * rayLength);
        }

    }

#if UNITY_EDITOR
    private void OnDrawGizmos() {
        if (showRaycast)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(transform.position, transform.up * rayLength);
        }
    }  
#endif

}


