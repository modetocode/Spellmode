using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component that resizes and moves various background elements in order to create scrolling background 
/// </summary>
public class BackgroundComponent : MonoBehaviour {

    [SerializeField]
    private Camera backgroundCamera;
    [SerializeField]
    private GameObject backgroundResizeGroup;
    [SerializeField]
    private GameObject backgroundGroup;
    [SerializeField]
    private BackgroundElement[] backgroundElements;

    private IList<Material> tiledMaterials;
    private readonly Vector3 topRightViewportPoint = new Vector3(1f, 1f);
    private readonly float defaultMoveSpeed = 0.2f;
    private float moveSpeed;
    private bool movementPaused;

    public void Awake() {
        Vector3 worldPointOffsetFromCenter = backgroundCamera.ViewportToWorldPoint(this.topRightViewportPoint) - backgroundGroup.transform.position;
        this.backgroundResizeGroup.transform.localScale = worldPointOffsetFromCenter * 2;
        this.tiledMaterials = new Material[this.backgroundElements.Length];
        for (int i = 0; i < this.backgroundElements.Length; i++) {
            Material sharedMaterial = this.backgroundElements[i].GameObject.GetComponent<Renderer>().sharedMaterial;
            float newHeight = Screen.height * this.backgroundElements[i].GameObject.transform.localScale.y;
            float textureAspectRatio = sharedMaterial.mainTexture.width / sharedMaterial.mainTexture.height;
            float newWidth = newHeight * textureAspectRatio;
            float newWidthRatio = Screen.width / newWidth;
            sharedMaterial.mainTextureScale = new Vector2(newWidthRatio, sharedMaterial.mainTextureScale.y);
            tiledMaterials[i] = sharedMaterial;
        }

        this.SetMoveSpeed(this.defaultMoveSpeed);
        this.movementPaused = false;
    }

    public void SetMoveSpeed(float moveSpeed) {
        this.moveSpeed = moveSpeed;
    }

    public void PauseMovement() {
        this.movementPaused = true;
    }

    public void ResumeMovement() {
        this.movementPaused = false;
    }

    public void FixedUpdate() {
        if (this.movementPaused) {
            return;
        }

        if (this.moveSpeed == 0) {
            return;
        }

        float textureOffset = moveSpeed * Time.fixedDeltaTime;
        for (int i = 0; i < tiledMaterials.Count; i++) {
            float reductionRatio = (100f - backgroundElements[i].SpeedReductionPercentage) / 100f;
            float direction = backgroundElements[i].IsMovingForward ? 1f : -1f;
            float newTextureXOffset = (tiledMaterials[i].mainTextureOffset.x + textureOffset * reductionRatio * direction) % 1;
            if (newTextureXOffset < 0) {
                newTextureXOffset = 1f - newTextureXOffset;
            }

            tiledMaterials[i].mainTextureOffset = new Vector2(newTextureXOffset, tiledMaterials[i].mainTextureOffset.y);
        }
    }
}