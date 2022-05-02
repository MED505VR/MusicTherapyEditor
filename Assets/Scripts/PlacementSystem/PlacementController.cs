using System;
using UnityEngine;

namespace PlacementSystem
{
    public class PlacementController : MonoBehaviour
    {
        [SerializeField] private GameObject currentSelectedPrefab;

        private XRIDefaultInputActions _inputActions;

        private void Awake()
        {
            _inputActions.Editor.PlaceCurrentSelectedPrefab.performed += _ => PlaceCurrentSelectedPrefab();
        }

        private void OnEnable()
        {
            _inputActions.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Disable();
        }

        private void PlaceCurrentSelectedPrefab()
        {
            Instantiate(currentSelectedPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
}