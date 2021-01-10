using UnityEngine;
using UnityEngine.EventSystems;

namespace Common.UI {
    [RequireComponent(typeof(Rigidbody2D))]
    public class DraggableUI : EventTrigger {
        public bool IsHeld { get; set; } = false;

        private Rigidbody2D rb;

        private Vector3 offset = Vector3.zero;

        private void Start() {
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate() {
            if (IsHeld && Input.GetMouseButton(0)) {
                Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
                rb.velocity = (target - transform.position) * 20;
            } else {
                rb.velocity = Vector3.zero;
            }
        }

        public override void OnBeginDrag(PointerEventData eventData) {
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            IsHeld = Input.GetMouseButton(0);

            eventData.selectedObject = gameObject;
        }

        public override void OnEndDrag(PointerEventData eventData) {
            IsHeld = false;

            eventData.selectedObject = null;
        }
    }
}