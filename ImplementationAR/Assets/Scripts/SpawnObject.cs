using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Assets.Scripts
{
    [RequireComponent(typeof(ARRaycastManager))]
    public class SpawnObject : MonoBehaviour
    {
        public ARRaycastManager raycastManager;
        private GameObject spawnObject;

        public GameObject placeableObject;

        static List<ARRaycastHit> hits = new List<ARRaycastHit>();
        private Mobileactions touchControl;
        private void Awake()
        {
            raycastManager = GetComponent<ARRaycastManager>();
            touchControl = new Mobileactions();
        }

        private void Start()
        {
            
        }

        bool TryGetTouchPosition(out Vector2 touchpos)
        {
            if (touchControl.Touch.TouchPress.IsPressed())
            {
                touchpos = touchControl.Touch.TouchPosition.ReadValue<Vector2>();
                return true;
            }

            touchpos = default(Vector2);
            return false;
        }

        private void Update()
        {
            print($"Has press {touchControl.Touch.TouchPress.IsPressed()}");
            print($"Has press {touchControl.Touch.TouchPosition.ReadValue<Vector2>()}");
            if (!TryGetTouchPosition(out var touchpos))
            {
                return;
            }

            if(raycastManager.Raycast(touchpos, hits , TrackableType.PlaneWithinPolygon))
            {
                var hitPos = hits[0].pose;
                if(spawnObject == null)
                {
                    spawnObject = Instantiate(placeableObject, hitPos.position, hitPos.rotation);
                }
                else
                {
                    spawnObject.transform.position = hitPos.position;
                    spawnObject.transform.rotation = hitPos.rotation;
                }
            }
        }

        private void OnEnable()
        {
            touchControl.Enable();
        }
        private void OnDisable()
        {
            touchControl.Disable();
        }
    }
}