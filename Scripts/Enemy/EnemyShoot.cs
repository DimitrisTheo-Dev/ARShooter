using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnemyShoot : MonoBehaviour
{

    [SerializeField] private float damage;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float shootingInterval;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnpoint;

    private bool _inRange;

    public bool InRange
    { 
        get => _inRange;
        set
        {
            _inRange = value;
            if (_inRange) StartCoroutine(StartShootingRoutine());
        }    
    }

    private IEnumerator StartShootingRoutine()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(shootingInterval);
        }
    }


    private void Shoot()
    {
        GetComponent<AudioSource>().Play();
        GameObject bullet = GameObject.Instantiate(bulletPrefab, bulletSpawnpoint.position, transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
        StartCoroutine(DestroyAfterTimeRoutine(bullet, 1));
    }

    private IEnumerator DestroyAfterTimeRoutine(GameObject objToDestroy, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(objToDestroy);
    }

}