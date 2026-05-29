using TurnBasedRPG;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Explorations {
    public class PlayerController : MonoBehaviour {
        public float moveSpeed;
        [SerializeField] private SpriteRenderer spr;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Animator anim;
        [SerializeField] private Interactor interactor;
        [SerializeField] private PlayerPartyView partyViewPrefab;

        private Vector2 Axis { get; set; }
        private bool _facingRight = true;
        private PlayerPartyView _partyView;

        public void Update() {
            if(rb) rb.linearVelocity = Axis.normalized * moveSpeed;
            if(anim) anim.SetBool("Moving", Axis.sqrMagnitude != 0);
        }

        public void Move(InputAction.CallbackContext context) {
            Axis = context.ReadValue<Vector2>();
            if(Axis.x > 0 && !_facingRight) Flip();
            if(Axis.x < 0 && _facingRight) Flip();
        }

        public void Interact(InputAction.CallbackContext context) {
            if(context.action.WasPressedThisFrame())
                interactor.TryInteract();
        }

        public void Attack(InputAction.CallbackContext context) {
            if(context.action.WasPressedThisFrame()) {
                Debug.Log("Atacou!");
                anim.SetTrigger("Attack");
            }
        }

        public void Flip() {
            if(spr) spr.flipX = !spr.flipX;
            _facingRight = !_facingRight;
        }

        public void Menu(InputAction.CallbackContext context) {
            if(!context.action.WasPressedThisFrame()) return;
            var party = GetComponent<PlayerParty>();
            if(!party) return;

            if(!_partyView) {
                _partyView = partyViewPrefab.Clone();
                _partyView.SetData(party);
            }

            _partyView.gameObject.SetActive(true);
        }
    }
}