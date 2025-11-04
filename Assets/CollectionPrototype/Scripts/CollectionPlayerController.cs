using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace CollectionPrototype
{
    [RequireComponent(typeof(Rigidbody2D))]
    [DisallowMultipleComponent]
    public class CollectionPlayerController : MonoBehaviour
    {
        [SerializeField]
        private float moveSpeed = 6f;

        private Rigidbody2D body;
        private Vector2 movement;

        public Vector2 Movement => movement;

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            Vector2 input = Vector2.zero;

#if ENABLE_LEGACY_INPUT_MANAGER
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
#endif

#if ENABLE_INPUT_SYSTEM
            if (Keyboard.current != null)
            {
                if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
                {
                    input.y += 1f;
                }
                if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
                {
                    input.y -= 1f;
                }
                if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
                {
                    input.x -= 1f;
                }
                if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
                {
                    input.x += 1f;
                }
            }

            if (Gamepad.current != null)
            {
                input += Gamepad.current.leftStick.ReadValue();
            }
#endif

            movement = input.sqrMagnitude > 1f ? input.normalized : input;
        }

        private void FixedUpdate()
        {
            body.MovePosition(body.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }
}
