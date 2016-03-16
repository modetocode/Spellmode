using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class MainCharacterComponent : MonoBehaviour {

    private Animator animator;
    private Rigidbody2D characterRigidBody;
    private CircleCollider2D groundCollider;
    private bool isGrounded;

    public void Awake() {
        this.animator = this.GetComponent<Animator>();
        this.characterRigidBody = this.GetComponent<Rigidbody2D>();
        this.groundCollider = this.GetComponent<CircleCollider2D>();
    }

    public void Start() {
        //this.SetMoveAnimation();
    }

    public void Update() {
        int groundLayerMask = LayerManager.GetLayerMask(Constants.Layers.LayerType.Ground);
        this.isGrounded = groundCollider.IsTouchingLayers(groundLayerMask);
        this.animator.SetBool("IsGrounded", this.isGrounded);
    }

    public void FixedUpdate() {
        this.animator.SetFloat("MoveSpeed", this.characterRigidBody.velocity.x);
        this.animator.SetFloat("VerticalSpeed", this.characterRigidBody.velocity.y);
    }

    public void Jump() {
        this.characterRigidBody.AddForce(new Vector2(0f, 300f));
    }

    public void SetMoveAnimation() {
        this.animator.SetFloat(Constants.Animation.MainCharacter.MoveSpeedParameterName, 1f);
    }
}