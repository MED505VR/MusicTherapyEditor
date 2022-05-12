using PlacementSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class DeleteModeButtonController : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField] private PlacementController placementController;
        [SerializeField] private string deleteModeActiveString;
        [SerializeField] private string deleteModeInactiveString;
        [SerializeField] private Color deleteModeActiveColor;
        [SerializeField] private Color deleteModeInactiveColor;
        [SerializeField] private Text text;
        [SerializeField] private Image image;

        #endregion

        #region Public Functions

        public void ToggleDeleteMode()
        {
            placementController.ToggleDeletionMode();

            if (placementController.GetDeleteMode())
            {
                text.text = deleteModeActiveString;
                image.color = deleteModeActiveColor;
            }
            else
            {
                text.text = deleteModeInactiveString;
                image.color = deleteModeInactiveColor;
            }
        }

        #endregion
    }
}