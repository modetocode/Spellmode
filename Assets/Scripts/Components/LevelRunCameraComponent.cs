using UnityEngine;

[RequireComponent(typeof(Camera))]
public class LevelRunCameraComponent : MonoBehaviour {

    private Camera levelCamera;

    public void Awake() {
        this.levelCamera = this.GetComponent<Camera>();
        this.SetCameraInitialPosition();
    }

    private void SetCameraInitialPosition() {
        //set the position so that the coordinate system will start on the bottom lower part of the screen
        //throw new NotImplementedException();
    }
}
