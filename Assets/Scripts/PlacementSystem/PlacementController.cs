using System;
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

        public GameObject CurrentSelectedPrefab { get; set; }

        private GameObject _currentSelectedPrefabInstance;
        private XRIDefaultInputActions _inputActions;
        private Vector3 _targetPrefabPlacementLocation;

        #endregion

        #region Event Functions

        private void Awake()
        {
            _inputActions ??= new XRIDefaultInputActions();
            _inputActions.Editor.PlaceCurrentSelectedPrefab.performed += _ => PlaceCurrentSelectedPrefab();
        }

        private void Update()
        {
            _targetPrefabPlacementLocation = GetPlacementGroundPosition();

            if (_currentSelectedPrefabInstance != null) RotateCurrentSelectedPrefab(_currentSelectedPrefabInstance);

            DrawPrefabPlacementVisual();
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

        #region Private Methods

        private void DrawPrefabPlacementVisual()
        {
            if (CurrentSelectedPrefab == null || _targetPrefabPlacementLocation == new Vector3(100, 100, 100)) return;

            if (_currentSelectedPrefabInstance == null)
            {
                _currentSelectedPrefabInstance = Instantiate(CurrentSelectedPrefab, _targetPrefabPlacementLocation,
                    Quaternion.identity);

                CurrentSelectedPrefab.gameObject.layer = 2;

                try
                {
                    _currentSelectedPrefabInstance.GetComponent<Collider>().enabled = false;
                    _currentSelectedPrefabInstance.GetComponent<MeshRenderer>().material =
                        currentSelectedPrefabPlacementMaterial;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            else
            {
                _currentSelectedPrefabInstance.transform.position = _targetPrefabPlacementLocation;
            }
        }

        private Vector3 GetPlacementGroundPosition()
        {
            // Returns the point hit by the raycast if the raycast hit the ground layer
            if (Physics.Raycast(rightControllerTransform.position, rightControllerTransform.forward, out var hit, 10))
                return hit.transform.gameObject.layer != 3 ? new Vector3(100, 100, 100) : hit.point;

            return new Vector3(100, 100, 100);
        }

        private void PlaceCurrentSelectedPrefab()
        {
            if (_targetPrefabPlacementLocation == new Vector3(100, 100, 100)) return;

            if (CurrentSelectedPrefab)
                Instantiate(CurrentSelectedPrefab, _targetPrefabPlacementLocation,
                    _currentSelectedPrefabInstance.transform.rotation);
        }

        private void RotateCurrentSelectedPrefab(GameObject prefab)
        {
            var input = _inputActions.Editor.RotateCurrentSelectedPrefab.ReadValue<Vector2>();

            prefab.transform.Rotate(0, input.x * Time.deltaTime * rotationSpeed, 0);
        }

        #endregion
    }
}