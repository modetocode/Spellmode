using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DestroyerComponent : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collidedObject) {
        if (LayerManager.GetLayerType(collidedObject.gameObject) == Constants.Layers.LayerType.MainCharacter) {
            Debug.Log("End game");
        }
        else {
            Destroy(collidedObject.gameObject);
        }
    }
}
