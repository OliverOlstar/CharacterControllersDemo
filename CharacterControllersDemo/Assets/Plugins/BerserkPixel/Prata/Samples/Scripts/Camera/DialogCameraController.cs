using Project.Scripts.Player;
using UnityEngine;

namespace BerserkPixel.Prata
{
    public class DialogCameraController : MonoBehaviour
    {
        [SerializeField] private float zoomLens = 7;
        [SerializeField] private float zoomDuration = 2f;
        [SerializeField] private float screenY = -.15f;
        
        private Camera mainCamera;
        
        private float startLens;
        private float targetLens;
        
        private float startScreenY;
        private float targetScreenY;
        
        private float startScreenX;
        private float targetScreenX;

        private Transform playerTransform;

        private void Awake()
        {
            mainCamera = Camera.main;
            playerTransform = FindObjectOfType<Movement>().transform;
            startLens = mainCamera.orthographicSize;
            targetLens = startLens;
            
            startScreenX = mainCamera.transform.position.x;
            targetScreenX = startScreenX;
            
            startScreenY = mainCamera.transform.position.y;
            targetScreenY = startScreenY;
        }

        private void Start()
        {
            DialogManager.Instance.OnDialogStart += HandleDialogStart;
            DialogManager.Instance.OnDialogEnds += HandleDialogEnd;
            DialogManager.Instance.OnDialogCancelled += HandleDialogEnd;
        }

        private void OnDisable()
        {
            DialogManager.Instance.OnDialogStart -= HandleDialogStart;
            DialogManager.Instance.OnDialogEnds -= HandleDialogEnd;
            DialogManager.Instance.OnDialogCancelled -= HandleDialogEnd;
        }

        private void Update()
        {
            mainCamera.orthographicSize = Mathf.Lerp(
                mainCamera.orthographicSize, 
                targetLens,
                zoomDuration * Time.deltaTime);

            var cameraPosition = mainCamera.transform.position;
            cameraPosition.y = Mathf.Lerp(
                cameraPosition.y, 
                targetScreenY, 
                zoomDuration * Time.deltaTime);
            
            cameraPosition.x = Mathf.Lerp(
                cameraPosition.x, 
                targetScreenX, 
                zoomDuration * Time.deltaTime);

            mainCamera.transform.position = cameraPosition;
        }

        private void HandleDialogStart()
        {
            targetLens = zoomLens;
            targetScreenY = playerTransform.position.y + screenY;
            targetScreenX = playerTransform.position.x;
        }

        private void HandleDialogEnd()
        {
            targetLens = startLens;
            targetScreenY = startScreenY;
            targetScreenX = startScreenX;
        }
    }
}