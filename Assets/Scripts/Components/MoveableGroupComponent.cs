using UnityEngine;


/// <summary>
/// Component that moves all the specified objects so they follow the object on the x-axis
/// </summary>
public class MoveableGroupComponent : MonoBehaviour {
    [SerializeField]
    private GameObject[] moveableObjects;

    [SerializeField]
    private GameObject objectToFollow;

    private Vector3 previousPosition = Vector3.zero;

    public void Update() {
        if (previousPosition == Vector3.zero) {
            previousPosition = objectToFollow.transform.position;
            return;
        }

        float positionXOffset = this.objectToFollow.transform.position.x - this.previousPosition.x;
        for (int i = 0; i < moveableObjects.Length; i++) {
            moveableObjects[i].transform.position += new Vector3(positionXOffset, 0f, 0f);
        }

        this.previousPosition = this.objectToFollow.transform.position;
    }
}

