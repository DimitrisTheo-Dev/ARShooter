using System;
using UnityEngine;

public class FPSLogic : MonoBehaviour
{

    [SerializeField] GameObject HUDCanvas;
    [SerializeField] GameObject mainCamera;

    private bool _locked;
    private bool _hitAnything;
    private RaycastHit _hit;

    public event Action<GameObject> OnEnemyLocked;
    public event Action OnEnemyUnlocked;

    void FixedUpdate()
    {

        _hitAnything = Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out _hit, 10);

        if (_hitAnything)
        {
            if(_hit.transform.gameObject.CompareTag("Enemy") && !_locked)
            {
                _locked = true;
                OnEnemyLocked?.Invoke(_hit.transform.gameObject);
                Debug.Log("<<<<<<<< LOCKED >>>>>>>>");
            }

            if (!_hit.transform.gameObject.CompareTag("Enemy") && _locked)
            {
                _locked = false;

                OnEnemyUnlocked?.Invoke();
                Debug.Log("<<<<<<<< UNLOCKED >>>>>>>>");
            }
        }

        if(!_hitAnything && _locked)
        {
            _locked = false;
            OnEnemyUnlocked?.Invoke();
            Debug.Log("<<<<<<<< UNLOCKED >>>>>>>>");

        }

    }
}