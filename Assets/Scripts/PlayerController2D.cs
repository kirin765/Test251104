using UnityEngine;

namespace CollectionGame
{
    [RequireComponent(typeof(Rigidbody2D))]
    [DisallowMultipleComponent]
    public class PlayerController2D : MonoBehaviour
    {
        [SerializeField]
        private float moveSpeed = 6f;

        private Rigidbody2D body;
        private Vector2 movement;

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            movement = movement.sqrMagnitude > 1f ? movement.normalized : movement;
        }

        private void FixedUpdate()
        {
            body.MovePosition(body.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }
}
