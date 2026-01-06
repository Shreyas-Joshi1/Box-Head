using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 0.2f;

    private float nextFireTime = 0f;
    private PlayerInputHandler inputHandler;

    private void Awake()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
    }

    private void Update()
    {
        if(Time.time < nextFireTime) return;

        if (inputHandler.InputActions.Player.Attack.triggered)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Shoot()
    {
        GameObject bulletObj = Instantiate(bullet, firePoint.position, Quaternion.identity);
        Bullet bulletInstance = bulletObj.GetComponent<Bullet>();

        Vector2 shootDir = firePoint.right;
        bulletInstance.Fire(shootDir);
    }
}
