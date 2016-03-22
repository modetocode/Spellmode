using UnityEngine;

/// <summary>
/// Component that is attached to the main camera in the Level Run scene. It can follow a given object.
/// </summary>
[RequireComponent(typeof(Camera))]
public class LevelRunCameraComponent : MonoBehaviour, ITickable {

    private GameObject trackedObject;
    private Camera levelRunCamera;
    private Vector3 lastPositionOfTrackedObject;

    public void Awake() {
        this.levelRunCamera = this.GetComponent<Camera>();
        this.SetInitialCameraPosition();
    }

    private void SetInitialCameraPosition() {
        Vector2 focusPointWorldSpace = Vector2.zero;
        Vector2 centerViewPortPoint = new Vector2(0.5f, 0.5f);
        Vector2 offsetFromCenterViewPort = centerViewPortPoint - Constants.Camera.LevelRun.InitialFocusObjectPosition;
        Vector2 screenWorldSpaceDimensions = this.levelRunCamera.ViewportToWorldPoint(Vector3.one) - this.levelRunCamera.ViewportToWorldPoint(Vector3.zero);
        Vector2 offsetWorldSpace = new Vector2(offsetFromCenterViewPort.x * screenWorldSpaceDimensions.x, offsetFromCenterViewPort.y * screenWorldSpaceDimensions.y);
        Vector2 cameraPositionWorldSpace = focusPointWorldSpace + offsetWorldSpace;
        this.levelRunCamera.transform.position = new Vector3(cameraPositionWorldSpace.x, cameraPositionWorldSpace.y, Constants.Camera.LevelRun.CameraDepth);
    }

    public void TrackObject(GameObject gameObject) {
        //TODO arg check
        this.trackedObject = gameObject;
        this.lastPositionOfTrackedObject = Vector3.zero;
    }

    public void Tick(float deltaTime) {
        if (trackedObject == null) {
            return;
        }

        if (this.lastPositionOfTrackedObject == Vector3.zero) {
            this.lastPositionOfTrackedObject = this.trackedObject.transform.position;
            return;
        }

        float ofsetFromPreviousXPosition = this.trackedObject.transform.position.x - this.lastPositionOfTrackedObject.x;
        this.levelRunCamera.transform.position += new Vector3(ofsetFromPreviousXPosition, 0f, 0f);
        this.lastPositionOfTrackedObject = this.trackedObject.transform.position;
    }

    public void OnTickingPaused(float deltaTime) {
    }
}

