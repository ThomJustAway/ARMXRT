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
        const float k_PrefabHalfSize = 0.025f;

        [SerializeField]
        [Tooltip("The prefab to be placed or moved.")]
        GameObject m_PrefabToPlace;

        [SerializeField]
        [Tooltip("The Scriptable Object Asset that contains the ARRaycastHit event.")]
        ArHitEvent m_RaycastHitEvent;

        GameObject m_SpawnedObject;

        /// <summary>
        /// The prefab to be placed or moved.
        /// </summary>
        public GameObject prefabToPlace
        {
            get => m_PrefabToPlace;
            set => m_PrefabToPlace = value;
        }

        /// <summary>
        /// The spawned prefab instance.
        /// </summary>
        public GameObject spawnedObject
        {
            get => m_SpawnedObject;
            set => m_SpawnedObject = value;
        }

        void OnEnable()
        {
            if (m_RaycastHitEvent == null || m_PrefabToPlace == null)
            {
                Debug.LogWarning($"{nameof(spawnedObject)} component on {name} has null inputs and will have no effect in this scene.", this);
                return;
            }

            if (m_RaycastHitEvent != null)
                m_RaycastHitEvent.eventRaised += PlaceObjectAt;
        }

        void OnDisable()
        {
            if (m_RaycastHitEvent != null)
                m_RaycastHitEvent.eventRaised -= PlaceObjectAt;
        }

        void PlaceObjectAt(object sender, ARRaycastHit hitPose)
        {
            if (m_SpawnedObject == null)
            {
                m_SpawnedObject = Instantiate(m_PrefabToPlace, hitPose.trackable.transform.parent);
            }

            var forward = hitPose.pose.rotation * Vector3.up;
            var offset = forward * k_PrefabHalfSize;
            m_SpawnedObject.transform.position = hitPose.pose.position + offset;
            m_SpawnedObject.transform.parent = hitPose.trackable.transform.parent;
        }
    }
}