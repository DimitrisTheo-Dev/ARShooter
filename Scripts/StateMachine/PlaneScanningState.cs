using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaneScanningState : IState
{
    private readonly GameObject _planeScanningCanvas;
    private GameObject _enemyBase;

    private ARPlaneManager _arPlaneManager;
    private List<ARPlane> _foundPlanes;

    private readonly GameObject _enemyBasePrefab;

    public PlaneScanningState(GameObject planeScanningCanvas, GameObject enemyBase)
    {
        this._planeScanningCanvas = planeScanningCanvas;
        this._enemyBasePrefab = enemyBase;
    }

    public void EnterState()
    {
        Debug.Log("<<<<<<<< Enter PlaneScanningState >>>>>>>>");
        _planeScanningCanvas.SetActive(true);
        _arPlaneManager = GameObject.FindObjectOfType<ARPlaneManager>();
        _arPlaneManager.planesChanged += PlanesChanged;
        _foundPlanes = new List<ARPlane>();
    }

    public void ExecuteState()
    {
        Debug.Log("<<<<<<<< Execute PlaneScanningState >>>>>>>>");
    }

    private void PlanesChanged(ARPlanesChangedEventArgs data)
    {
        if(data.added != null && data.added.Count > 0)
        {
            _foundPlanes.AddRange(data.added);
        }

        foreach (ARPlane plane in _foundPlanes.Where(plane => plane.extents.x * plane.extents.y >= 0.2f))
        {
            if (plane.alignment.IsVertical() && _enemyBase == null)
            {
                _enemyBase = GameObject.Instantiate(_enemyBasePrefab);
                _enemyBase.transform.position = plane.center;
                _enemyBase.transform.forward = plane.normal;

                GameManager.instance.EnemyBase = _enemyBase;
            }
        }

    }

    public void ExitState()
    {
        Debug.Log("<<<<<<<< Exit PlaneScanningState >>>>>>>>");

        foreach (var plane in _foundPlanes)
        {
            plane.gameObject.SetActive(false);
        }

        _planeScanningCanvas.SetActive(false);
        _arPlaneManager.planesChanged -= PlanesChanged;
    }
}
