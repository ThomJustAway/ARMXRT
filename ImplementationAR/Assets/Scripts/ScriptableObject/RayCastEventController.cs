using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Assets.Scripts.ScriptableObject
{
    public class RayCastEventController : MonoBehaviour
    {
        static List<ARRaycastHit> s_Hits = new();
        static Ray s_RaycastRay;

        [SerializeField]
        [Tooltip("The active XR Origin in the scene.")]
        XROrigin m_XROrigin;

        [SerializeField]
        [Tooltip("The active AR Raycast Manager in the scene.")]
        ARRaycastManager m_RaycastManager;

        [SerializeField]
        InputManager m_InputActionReferences;

        [SerializeField]
        [Tooltip("Event to raise if anything was hit by the raycast.")]
        ArHitEvent m_ARRaycastHitEvent;

        [SerializeField]
        [Tooltip("The type of trackable the raycast will hit.")]
        TrackableType m_TrackableType = TrackableType.PlaneWithinPolygon;
        private bool canRecord = true;

        public InputManager inputActions => m_InputActionReferences;

        public ArHitEvent raycastHitEventAsset
        {
            get => m_ARRaycastHitEvent;
            set => m_ARRaycastHitEvent = value;
        }

        public TrackableType trackableType
        {
            get => m_TrackableType;
            set => m_TrackableType = value;
        }

        void OnEnable()
        {
            if (m_RaycastManager == null || m_XROrigin == null || m_InputActionReferences == null)
            {
                Debug.LogWarning($"{nameof(raycastHitEventAsset)} component on {name} has null inputs and will have no effect in this scene.", this);
                return;
            }

            if (m_InputActionReferences.ScreenTap.action != null)
                m_InputActionReferences.ScreenTap.action.performed += ScreenTapped;
        }

        void OnDisable()
        {
            if (m_InputActionReferences == null)
                return;

            if (m_InputActionReferences.ScreenTap.action != null)
                m_InputActionReferences.ScreenTap.action.performed -= ScreenTapped;

        }

        void ScreenTapped(InputAction.CallbackContext context)
        {
            if (!canRecord) return;
            if (context.control.device is not Pointer pointer)
            {
                Debug.LogError("Input actions are incorrectly configured. Expected a Pointer binding ScreenTapped.", this);
                return;
            }

            var tapPosition = pointer.position.ReadValue();

            if (m_ARRaycastHitEvent != null &&
                m_RaycastManager.Raycast(tapPosition, s_Hits, m_TrackableType))
            {
                m_ARRaycastHitEvent.Raise(s_Hits[0]);
            }
        }

        public void ToggleRecord()
        {
            canRecord = !canRecord;
        }

        /// <summary>
        /// Automatically initialize serialized fields when component is first added.
        /// </summary>
        void Reset()
        {
            m_RaycastManager = GetComponent<ARRaycastManager>();
            m_XROrigin = GetComponent<XROrigin>();

            if (m_XROrigin == null)
            {
#if UNITY_2023_1_OR_NEWER
                m_XROrigin = FindAnyObjectByType<XROrigin>();
#else
                m_XROrigin = FindObjectOfType<XROrigin>();
#endif
            }

            if (m_RaycastManager == null)
            {
#if UNITY_2023_1_OR_NEWER
                m_RaycastManager = FindAnyObjectByType<ARRaycastManager>();
#else
                m_RaycastManager = FindObjectOfType<ARRaycastManager>();
#endif
            }
        }
    }
}