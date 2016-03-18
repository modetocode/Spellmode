using System.Collections.Generic;
using UnityEngine;

public class BackgroundGroupResizerComponent : MonoBehaviour {

    [SerializeField]
    private Camera backgroundCamera;
    [SerializeField]
    private GameObject backgroundResizeGroup;
    [SerializeField]
    private GameObject[] backgroundComponents;

    private IList<Material> tiledMaterials;
    private readonly Vector3 topRightViewportPoint = new Vector3(1f, 1f);
    private readonly float defaultMoveSpeed = 0.3f;
    private float moveSpeed;

    public void Start() {
        Vector3 worldPointOffsetFromCenter = backgroundCamera.ViewportToWorldPoint(this.topRightViewportPoint);
        this.backgroundResizeGroup.transform.localScale = worldPointOffsetFromCenter * 2;
        //for (int i = 0; i < tiledMaterials.Length; i++) {
        //    float newWidthRatio = Screen.width / (float)tiledMaterials[i].mainTexture.width;
        //    tiledMaterials[i].mainTextureScale = new Vector2(newWidthRatio, tiledMaterials[i].mainTextureScale.y);
        //}

        this.tiledMaterials = new Material[this.backgroundComponents.Length];
        for (int i = 0; i < this.backgroundComponents.Length; i++) {
            Material sharedMaterial = this.backgroundComponents[i].GetComponent<Renderer>().sharedMaterial;
            float newHeight = Screen.height * this.backgroundComponents[i].transform.localScale.y;
            float textureAspectRatio = sharedMaterial.mainTexture.width / sharedMaterial.mainTexture.height;
            float newWidth = newHeight * textureAspectRatio;
            float newWidthRatio = Screen.width / newWidth;
            sharedMaterial.mainTextureScale = new Vector2(newWidthRatio, sharedMaterial.mainTextureScale.y);
            tiledMaterials[i] = sharedMaterial;
        }

        this.SetMoveSpeed(this.defaultMoveSpeed);
    }

    public void SetMoveSpeed(float moveSpeed) {
        this.moveSpeed = moveSpeed;
    }

    void FixedUpdate() {
        if (this.moveSpeed == 0) {
            return;
        }

        float textureOffset = moveSpeed * Time.fixedDeltaTime;
        for (int i = 0; i < tiledMaterials.Count; i++) {
            float newTextureXOffset = (tiledMaterials[i].mainTextureOffset.x + textureOffset) % 1;
            tiledMaterials[i].mainTextureOffset = new Vector2(newTextureXOffset, tiledMaterials[i].mainTextureOffset.y);
        }
    }
}