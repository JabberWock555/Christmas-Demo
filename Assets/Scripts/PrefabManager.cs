using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PrefabManager : MonoBehaviour
{
    [SerializeField] GameObject ChristmasPrfab;
    [SerializeField] Vector3 prefabOffset;
    [SerializeField] UIManager uiManager;


    private ARRaycastManager arRaycastManager;
    private Vector3 initialObjectPosition;
    private Vector2 initialTouchPosition;
    private float initialDistance;
    private Vector3 initialScale;

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private ARTrackedImageManager aRTrackedImageManager;

    void Awake()
    {
        if (aRTrackedImageManager == null)
        {
            aRTrackedImageManager = GetComponent<ARTrackedImageManager>();
        }

        arRaycastManager = GetComponent<ARRaycastManager>();
        if (arRaycastManager == null)
        {
            Debug.LogError("ARRaycastManager is not found in the scene. Please ensure it is added to your AR Session Origin.");
        }
    }

    void OnEnable()
    {
        ChristmasPrfab.SetActive(false);
        aRTrackedImageManager.trackablesChanged.AddListener(OnImageChanged);
    }

    private void OnImageChanged(ARTrackablesChangedEventArgs<ARTrackedImage> arg0)
    {
        foreach (var trackable in arg0.added)
        {
            //if (trackable.referenceImage.name == "test")
            {
                ChristmasPrfab.SetActive(true);
                //ChristmasPrfab.transform.parent = trackable.transform;
                ChristmasPrfab.transform.position += prefabOffset;
                uiManager.OnScanComplete();
                Logger.Instance.Log("Image Changed" + trackable.transform.position);
            }

        }
        foreach (var trackable in arg0.removed)
        {
            //if (trackable.Value.referenceImage.name == "test")
            {
                ChristmasPrfab.SetActive(false);
                Logger.Instance.Log("Image Removed");
            }
        }
    }

    void Update()
    {
        if (ChristmasPrfab != null && ChristmasPrfab.activeSelf)
        {
            TouchControls();
        }
    }

    private void TouchControls()
    {
        // Handle single touch (dragging)
        if (Input.touchCount == 1)
        {
            Logger.Instance.Log("Single Touch");
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                initialTouchPosition = touch.position;
                if (arRaycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinBounds))
                {
                    Pose hitPose = hits[0].pose;
                    initialObjectPosition = ChristmasPrfab.transform.position;
                }
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                if (arRaycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinBounds))
                {
                    Logger.Instance.Log("Single Touch Moved");
                    Pose hitPose = hits[0].pose;
                    Vector3 touchDelta = hitPose.position - ChristmasPrfab.transform.position;
                    ChristmasPrfab.transform.position = initialObjectPosition + touchDelta;
                }
            }
        }

        // Handle multi-touch (pinch-to-scale)
        if (Input.touchCount == 2)
        {
            Logger.Instance.Log("Multi Touch");
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
            {
                initialDistance = Vector2.Distance(touch1.position, touch2.position);
                initialScale = ChristmasPrfab.transform.localScale;
            }
            else if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                Logger.Instance.Log("Multi Touch Scaled");
                float currentDistance = Vector2.Distance(touch1.position, touch2.position);
                float scaleFactor = currentDistance / initialDistance;
                ChristmasPrfab.transform.localScale = initialScale * scaleFactor;
            }
        }
    }
}


