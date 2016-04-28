using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class ImageCanvasAnimator : MonoBehaviour {

    private Image imageCanvas;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start() {
        this.imageCanvas = this.GetComponent<Image>();
        this.animator = this.GetComponent<Animator>();
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        this.spriteRenderer.enabled = false;
    }

    void Update() {
        if (animator.runtimeAnimatorController) {
            imageCanvas.sprite = spriteRenderer.sprite;
        }
    }
}