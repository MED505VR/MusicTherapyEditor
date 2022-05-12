using System.Collections.Generic;
using UnityEngine;

namespace PlacementSystem
{
    public class PlacementController : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField] private Transform leftControllerTransform;
        [SerializeField] private Transform rightControllerTransform;
        [SerializeField] private Material currentSelectedPrefabPlacementMaterial;
        [SerializeField] private float rotationSpeed;

        #endregion

        #region Non-serialized Fields

        private bool _placementModeEnabled;
        private bool _deletionModeEnabled;
        private GameObject _currentSelectedPrefab;
        private GameObject _currentSelectedPrefabInstance;
        private GameObject _oldPrefabInstanceForDeletion;
        private GameObject _prefabInstanceForDeletion;
        private XRIDefaultInputActions _inputActions;
        private Vector3 _targetPrefabPlacementLocation;

        #endregion

        #region Unity Event Functions

        private void Awake()
        {
            _inputActions ??= new XRIDefaultInputActions();
            _inputActions.Editor.PlaceCurrentSelectedPrefab.performed += _ => PlaceCurrentSelectedPrefab();

            _placementModeEnabled = false;
            _deletionModeEnabled = false;
        }

        private void Update()
        {
            _targetPrefabPlacementLocation = GetPlacementGroundPosition();

            if (_currentSelectedPrefabInstance != null) RotateCurrentSelectedPrefab(_currentSelectedPrefabInstance);

            _prefabInstanceForDeletion = GetPlacedObjectForDeletion();
            DrawPrefabPlacementVisual();
            DrawPrefabDeletionVisual();
        }

        private void OnEnable()
        {
            _inputActions.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Disable();
        }

        #endregion

        #region Public Functions

        /// <summary>
        ///     Sets the placement mode to the supplied value.
        /// </summary>
        /// <param name="value"></param>
        public void SetPlacementMode(bool value)
        {
            if (value) _deletionModeEnabled = false;
            _placementModeEnabled = value;
        }

        /// <summary>
        ///     Sets the deletion mode to the supplied value.
        /// </summary>
        public void ToggleDeletionMode()
        {
            _deletionModeEnabled = !_deletionModeEnabled;
        }

        public bool GetDeleteMode()
        {
            return _deletionModeEnabled;
        }

        /// <summary>
        ///     Sets the current selected prefab which will be placed next.
        /// </summary>
        /// <param name="value"></param>
        public void SetCurrentSelectedPrefab(GameObject value)
        {
            SetPlacementMode(true);
            _currentSelectedPrefab = value;
        }

        #endregion

        #region Private Functions

        private void DrawPrefabPlacementVisual()
        {
            if (!_placementModeEnabled || _deletionModeEnabled)
            {
                if (_currentSelectedPrefabInstance) _currentSelectedPrefabInstance = null;

                return;
            }

            if (_currentSelectedPrefab == null) return;

            // If the location is invalid we want to destroy the visual
            if (_targetPrefabPlacementLocation == new Vector3(100, 100, 100))
            {
                Destroy(_currentSelectedPrefabInstance);
                return;
            }

            if (_currentSelectedPrefabInstance == null)
            {
                _currentSelectedPrefabInstance = Instantiate(_currentSelectedPrefab, _targetPrefabPlacementLocation,
                    Quaternion.identity);

                _currentSelectedPrefab.gameObject.layer = 2;

                var coll = _currentSelectedPrefabInstance.GetComponent<Collider>();
                var outline = _currentSelectedPrefabInstance.GetComponent<Outline>();

                var colliderComponents = new List<Collider>();
                if (coll) colliderComponents.Add(coll);
                colliderComponents.AddRange(_currentSelectedPrefabInstance.GetComponentsInChildren<Collider>());

                if (colliderComponents.Count != 0)
                    foreach (var colliderComponent in colliderComponents)
                        colliderComponent.enabled = false;

                if (outline)
                {
                    outline.enabled = true;
                    outline.OutlineWidth = 3.5f;
                    outline.OutlineColor = Color.cyan;
                }
            }

            if (!_currentSelectedPrefabInstance.CompareTag(_currentSelectedPrefab.gameObject.tag))
                Destroy(_currentSelectedPrefabInstance);

            else
                _currentSelectedPrefabInstance.transform.position = _targetPrefabPlacementLocation;
        }

        private void DrawPrefabDeletionVisual()
        {
            if (!_deletionModeEnabled) return;

            if (_oldPrefabInstanceForDeletion != _prefabInstanceForDeletion)
                if (_oldPrefabInstanceForDeletion)
                    _oldPrefabInstanceForDeletion.GetComponent<Outline>().enabled = false;

            if (!_prefabInstanceForDeletion) return;

            var outline = _prefabInstanceForDeletion.GetComponent<Outline>();

            if (!outline) return;

            outline.enabled = true;
            outline.OutlineWidth = 4f;
            outline.OutlineColor = Color.red;

            _oldPrefabInstanceForDeletion = _prefabInstanceForDeletion;
        }

        // Gets the ground position for placing the selected prefab.
        private Vector3 GetPlacementGroundPosition()
        {
            // Returns the point hit by the raycast if the raycast hit the ground layer.
            if (Physics.Raycast(rightControllerTransform.position, rightControllerTransform.forward, out var hit, 10))
                if (hit.transform.gameObject.layer == 3)
                    return hit.point;

            return new Vector3(100, 100, 100);
        }

        // Returns the hovered over game object for deletion.
        private GameObject GetPlacedObjectForDeletion()
        {
            // Return the object if the object is on the placement layer.
            if (Physics.Raycast(rightControllerTransform.position, rightControllerTransform.forward, out var hit, 10))
                if (hit.transform.gameObject.layer == 6)
                    return hit.transform.gameObject;

            return null;
        }

        private void PlaceCurrentSelectedPrefab()
        {
            if (_placementModeEnabled)
            {
                if (_targetPrefabPlacementLocation == new Vector3(100, 100, 100)) return;
                if (!_currentSelectedPrefab) return;

                var prefab = Instantiate(_currentSelectedPrefab, _targetPrefabPlacementLocation,
                    _currentSelectedPrefabInstance.transform.rotation);

                prefab.layer = 6;

                Destroy(_currentSelectedPrefabInstance);

                _placementModeEnabled = false;
            }
            else if (_deletionModeEnabled)
            {
                Destroy(_prefabInstanceForDeletion);
            }
        }

        private void RotateCurrentSelectedPrefab(GameObject prefab)
        {
            if (!_placementModeEnabled) return;

            var input = _inputActions.Editor.RotateCurrentSelectedPrefab.ReadValue<Vector2>();

            prefab.transform.Rotate(0, -input.x * Time.deltaTime * rotationSpeed, 0);
        }

        #endregion
    }
}