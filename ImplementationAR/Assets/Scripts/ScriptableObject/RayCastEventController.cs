using Patterns;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Assets.Scripts.ScriptableObject
{
    /// <summary>
    /// RayCast controller simplify the input recieve from the screen to trigger events
    /// for other scripts to sense. 
    /// </summary>
    public class RayCastEventController : MonoBehaviour
    {
        //Createa a static list to hold hits so that 
        //The raycast does not have to create a new list every single time.
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
        Camera m_Camera;

        [SerializeField]
        [Tooltip("The type of trackable the raycast will hit.")]
        TrackableType m_TrackableType = TrackableType.PlaneWithinPolygon;

        //Sense the ray cast for the UI so that the raycast event controller knows
        //if the player hits a UI component or not.
        [SerializeField]
        GraphicRaycaster m_Raycaster;
        [SerializeField]
        EventSystem m_EventSystem;
        PointerEventData m_PointerEventData;
        List<RaycastResult> UIRayCastResult;

        //able to controll what the raycast can sense
        //- raycast AR hits the AR plane
        //- RayCast scene hits the scene plane.
        public bool canRayCastAR = true;
        public bool canRayCastScene = true;

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
                //subcribe to the screen press
                m_InputActionReferences.ScreenTap.action.performed += ScreenTapped;
            //subscribe to events to toggle different settings when certain events are being played.
            EventManager.Instance.AddListener(EventName.BeginPlacing, BeginPlacing);
            EventManager.Instance.AddListener(EventName.BeginAdjustingARScene, BeginAdjusting);
            EventManager.Instance.AddListener(EventName.StartGame, StartGame);
            EventManager.Instance.AddListener(EventName.RestartGame, BeginPlacing);
            EventManager.Instance.AddListener(EventName.WinGame, StopRayCasting);
            EventManager.Instance.AddListener(EventName.LoseGame, StopRayCasting);
        }

        void OnDisable()
        {
            if (m_InputActionReferences == null)
                return;

            if (m_InputActionReferences.ScreenTap.action != null)
                //unsubcribe to the screen press
                m_InputActionReferences.ScreenTap.action.performed -= ScreenTapped;

            //unsubscribe the event to prevent memory leak.
            EventManager.Instance.RemoveListener(EventName.BeginPlacing, BeginPlacing);
            EventManager.Instance.RemoveListener(EventName.BeginAdjustingARScene, BeginAdjusting);
            EventManager.Instance.RemoveListener(EventName.StartGame, StartGame);
            EventManager.Instance.RemoveListener(EventName.RestartGame, BeginPlacing);
            EventManager.Instance.RemoveListener(EventName.WinGame, StopRayCasting);
            EventManager.Instance.RemoveListener(EventName.LoseGame, StopRayCasting);
        }

        /// <summary>
        /// When player tap the screen, this will be called out. It would check
        /// what can or cannot be raycast and will send events accordingly.
        /// </summary>
        /// <param name="context">The extra information of the tap input.</param>
        void ScreenTapped(InputAction.CallbackContext context)
        {
            if (context.control.device is not Pointer pointer)
            {
                Debug.LogError("Input actions are incorrectly configured. Expected a Pointer binding ScreenTapped.", this);
                return;
            }
            var tapPosition = pointer.position.ReadValue();

            if (CheckIfHitUI(tapPosition)) return; //if it hits a UI component, ignore it.
            RayCastScene(tapPosition);
            RaycastAR(tapPosition);


            bool CheckIfHitUI(Vector2 tapPosition)
            {
                UIRayCastResult = new();
                m_PointerEventData = new PointerEventData(m_EventSystem);
                //Set the Pointer Event Position to that of the mouse position
                m_PointerEventData.position = tapPosition;
                m_Raycaster.Raycast(m_PointerEventData, UIRayCastResult);


                if (UIRayCastResult.Count > 0) return true;
                return false;

            }
            void RaycastAR(Vector2 tapPosition)
            {
                if (!canRayCastAR) return;
                if (m_ARRaycastHitEvent != null &&
                    m_RaycastManager.Raycast(tapPosition, s_Hits, m_TrackableType))
                {//if hits an ar plane, then raise the event to let other listeners know whati is being hit and which position it is.
                    m_ARRaycastHitEvent.Raise(s_Hits[0]);
                }
            }
            void RayCastScene(Vector2 tapPosition)
            {
                if (!canRayCastScene) return;
                Ray raycast = m_Camera.ScreenPointToRay(tapPosition);
                //get a specific raycast from screen to scene.
                RaycastHit raycastHit;
                if (Physics.Raycast(raycast, out raycastHit))
                {
                    //calls a Ihittable component which allow gameobject to interact with raycast and callout the specific
                    //actions that needs to be done.
                    if (raycastHit.collider.gameObject.TryGetComponent<IHittable>(out var component))
                    {
                        component.OnHit();
                    }
                }
            }
        }
        /// <summary>
        /// Event called when the player starts placing the room
        /// </summary>
        void BeginPlacing()
        {
            canRayCastAR = true;
            canRayCastScene = false;
        }
        /// <summary>
        /// Event called when the player rotate and scales the room.
        /// </summary>
        void BeginAdjusting()
        {
            canRayCastAR = false;
            canRayCastScene = false;

        }
        /// <summary>
        /// Event called when the player starts the game.
        /// </summary>
        void StartGame()
        {
            canRayCastAR = false;
            canRayCastScene = true;
        }


        void StopRayCasting()
        {
            canRayCastAR = false;
            canRayCastScene = false;
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