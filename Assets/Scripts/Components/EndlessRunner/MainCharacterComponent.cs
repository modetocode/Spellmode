using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class MainCharacterComponent : MonoBehaviour {

    private Animator animator;
    private Rigidbody2D characterRigidBody;
    private CircleCollider2D groundCollider;
    private bool isGrounded;
    private float moveSpeed;

    public void Awake() {
        this.animator = this.GetComponent<Animator>();
        this.characterRigidBody = this.GetComponent<Rigidbody2D>();
        this.groundCollider = this.GetComponent<CircleCollider2D>();
    }

    public void Start() {
        this.characterRigidBody.freezeRotation = true;
    }

    public void Update() {
        int groundLayerMask = LayerManager.GetLayerMask(Constants.Layers.LayerType.Ground);
        this.isGrounded = groundCollider.IsTouchingLayers(groundLayerMask);
        this.animator.SetBool("IsGrounded", this.isGrounded);
    }

    public void FixedUpdate() {
        if (moveSpeed > 0f) {
            float currentMoveSpeed = Time.fixedDeltaTime * moveSpeed;
            //TODO check how to properly move a character using Unity Physics
            this.characterRigidBody.velocity = new Vector2(currentMoveSpeed, this.characterRigidBody.velocity.y);
            //this.characterRigidBody.AddForce(new Vector2(currentMoveSpeed, 0f));
            this.animator.SetFloat(Constants.Animations.MainCharacter.MoveSpeedParameterName, currentMoveSpeed);
        }

        this.animator.SetFloat("MoveSpeed", this.characterRigidBody.velocity.x);
        this.animator.SetFloat("VerticalSpeed", this.characterRigidBody.velocity.y);
    }

    public void Jump() {
        this.characterRigidBody.AddForce(new Vector2(0f, 700f));
    }

    public void Move(float moveSpeed) {
        this.moveSpeed = moveSpeed;
    }
}