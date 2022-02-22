using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerShoot : MonoBehaviour
{

    [SerializeField] private LineRenderer laser;
    [SerializeField] private float laserDamage;

    private GameObject _target;
    private GameObject _arCamera;
    private FPSLogic _fpsLogic;

    private void OnEnable()
    {
        _fpsLogic = GetComponent<FPSLogic>();
        _arCamera = laser.GetComponentInParent<Camera>().gameObject;

        _fpsLogic.OnEnemyLocked += EnemyLocked;
        _fpsLogic.OnEnemyUnlocked += EnemyUnlocked;
    }

    private void OnDisable()
    {
        _fpsLogic.OnEnemyLocked -= EnemyLocked;
        _fpsLogic.OnEnemyUnlocked -= EnemyUnlocked;
    }


    public void ShootLaser()
    {
        laser.enabled = true;
        GetComponent<AudioSource>().Play();

        if(_target!= null)
        {
            _target.GetComponentInParent<Health>().ApplyDamage(laserDamage);
        }

        StartCoroutine(DisableLaserRoutine());
    }

    private IEnumerator DisableLaserRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        laser.enabled = false;
    }

    private void EnemyLocked(GameObject enemy)
    {
        _target = enemy;
    }

    private void EnemyUnlocked()
    {
        _target = null;
    }

}