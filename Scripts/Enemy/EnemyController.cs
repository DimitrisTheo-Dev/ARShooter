using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private Vector3 targetOffset;

    private float _distanceToTarget;
    private Vector3 _target;
    private Transform _homeBase;
    private EnemyShoot _enemyShoot;

    void Start()
    {
        //homeBase = GameManager.instance.HomeBase.transform;
        _enemyShoot = GetComponent<EnemyShoot>();
        _homeBase = GameObject.FindGameObjectWithTag("HomeBase").transform;
        _target = _homeBase.position + targetOffset;
        Randomize();
    }

    void Update()
    {
        transform.LookAt(_homeBase);
        _distanceToTarget = Vector3.Distance(transform.position, _target);

        if(_distanceToTarget < 0.5f)
        {
            transform.RotateAround(_target, Vector3.up, 20 * Time.deltaTime);
            
            if(!_enemyShoot.InRange)
                _enemyShoot.InRange = true;
        }
        else
        {
            transform.position += (_target - transform.position) * movementSpeed * Time.deltaTime;
        }
    }

    private void Randomize()
    {
        _target += new Vector3(Random.Range(-0.13f, 0.13f), Random.Range(-0.13f, 0.13f), Random.Range(-0.13f, 0.13f));
        movementSpeed += Random.Range(-0.13f, 0.13f);
    }
}