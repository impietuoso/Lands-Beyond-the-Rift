using Explorations;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    public float moveSpeed;
    public Vector2 axis { get; private set; }
    public SpriteRenderer spr;
    public Rigidbody2D rb;
    public Animator anim;
    private bool facingRight = true;
    public Interactor interactor;

    public void Update() {
        if(rb) rb.linearVelocity = axis.normalized * moveSpeed;
        if(anim) anim.SetBool("Moving",axis.sqrMagnitude != 0);
    }

    public void Move(InputAction.CallbackContext context) {
        axis = context.ReadValue<Vector2>();
        if(axis.x > 0 && !facingRight) Flip();
        if(axis.x < 0 && facingRight) Flip();
    }

    public void Interact(InputAction.CallbackContext context) {
        if(context.action.WasPressedThisFrame()) {
            interactor.TryInteract();
        }
    }
    
    public void Attack(InputAction.CallbackContext context) {
        if(context.action.WasPressedThisFrame()) {
          Debug.Log("Atacou!");
          anim.SetTrigger("Attack");
        }
    }
    
    public void Flip() {
        if(spr) spr.flipX = !spr.flipX;
        facingRight = !facingRight;
    }
}