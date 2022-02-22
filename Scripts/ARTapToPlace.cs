using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTapToPlace : MonoBehaviour
{

    [SerializeField] private GameObject homeBasePrefab;

    public event Action OnHomeBaseInstantiated;

    private GameObject _homeBase;

    private ARRaycastManager _arRaycastManager;

    private List<ARRaycastHit> hits = new();

    private bool _isOverUI;

    private void Start()
    {
        _arRaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    private void Update()
    {
        if (Input.touchCount <= 0) return;
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            Vector2 touchPos = touch.position;

            _isOverUI = IsOverUIObject(touchPos);
        }

        if (!_isOverUI && _arRaycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;

            if (_homeBase == null)
            {
                _homeBase = Instantiate(homeBasePrefab, hitPose.position, hitPose.rotation);
                OnHomeBaseInstantiated?.Invoke();
            }
            else
            {
                _homeBase.transform.position = hitPose.position;
            }
        }
    }


    private bool IsOverUIObject(Vector2 pos)
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return false;

        PointerEventData eventPosition = new PointerEventData(EventSystem.current);
        eventPosition.position = new Vector2(pos.x, pos.y);

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventPosition, results);

        return results.Count > 0;

    }

    public void HomeBaseLocked()
    {
        GameManager.instance.HomeBase = _homeBase;
    }
}
