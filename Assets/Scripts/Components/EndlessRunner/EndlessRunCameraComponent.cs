using UnityEngine;

[RequireComponent(typeof(Camera))]
public class EndlessRunCameraComponent : MonoBehaviour {

    //TODO remove this
    [SerializeField]
    private GameObject objectToFollow;
    private Camera levelCamera;

    public void Awake() {
        this.levelCamera = this.GetComponent<Camera>();
        this.SetCameraInitialPosition();
    }

    private void SetCameraInitialPosition() {
        //set the position so that the coordinate system will start on the bottom lower part of the screen
        //throw new NotImplementedException();
    }

    public void Update() {
        //TODO change this
        Vector3 cameraOffset = new Vector3(6, 4, -10);
        this.levelCamera.transform.position = new Vector3(this.objectToFollow.transform.position.x, 0, 0) + cameraOffset;
    }
}
