using UnityEngine;

namespace CollectionGame
{
    [RequireComponent(typeof(PlayerController2D))]
    [DisallowMultipleComponent]
    public class PlayerMovementAnimator : MonoBehaviour
    {
        [SerializeField]
        private Transform spriteRoot;

        [SerializeField]
        private float bobAmplitude = 0.1f;

        [SerializeField]
        private float bobFrequency = 7f;

        [SerializeField]
        private float swayAngle = 8f;

        [SerializeField]
        private float returnSpeed = 8f;

        private PlayerController2D controller;
        private SpriteRenderer spriteRenderer;
        private Vector3 defaultLocalPosition;
        private Quaternion defaultLocalRotation;
        private float bobTimer;

        private void Reset()
        {
            spriteRoot = transform;
        }

        private void Awake()
        {
            controller = GetComponent<PlayerController2D>();
            if (spriteRoot == null)
            {
                spriteRoot = transform;
            }

            spriteRenderer = spriteRoot.GetComponent<SpriteRenderer>();
            defaultLocalPosition = spriteRoot.localPosition;
            defaultLocalRotation = spriteRoot.localRotation;
        }

        private void OnEnable()
        {
            ResetPose(true);
        }

        private void Update()
        {
            Vector2 move = controller.Movement;
            float speed = move.sqrMagnitude;

            if (speed > 0.0001f)
            {
                bobTimer += Time.deltaTime * bobFrequency;
                float bobOffset = Mathf.Sin(bobTimer) * bobAmplitude;
                Vector3 targetPosition = defaultLocalPosition + new Vector3(0f, bobOffset, 0f);
                float damp = DampFactor();
                spriteRoot.localPosition = Vector3.Lerp(spriteRoot.localPosition, targetPosition, damp);

                float sway = Mathf.Sin(bobTimer) * swayAngle;
                Quaternion targetRotation = Quaternion.Euler(0f, 0f, sway);
                spriteRoot.localRotation = Quaternion.Slerp(spriteRoot.localRotation, targetRotation, damp);

                if (spriteRenderer != null)
                {
                    if (move.x > 0.05f)
                    {
                        spriteRenderer.flipX = false;
                    }
                    else if (move.x < -0.05f)
                    {
                        spriteRenderer.flipX = true;
                    }
                }
            }
            else
            {
                ResetPose(false);
            }
        }

        private void ResetPose(bool immediate)
        {
            bobTimer = 0f;
            if (immediate || Mathf.Approximately(Time.deltaTime, 0f))
            {
                spriteRoot.localPosition = defaultLocalPosition;
                spriteRoot.localRotation = defaultLocalRotation;
                return;
            }

            float damp = DampFactor();
            spriteRoot.localPosition = Vector3.Lerp(spriteRoot.localPosition, defaultLocalPosition, damp);
            if ((spriteRoot.localPosition - defaultLocalPosition).sqrMagnitude < 0.0001f)
            {
                spriteRoot.localPosition = defaultLocalPosition;
            }

            spriteRoot.localRotation = Quaternion.Slerp(spriteRoot.localRotation, defaultLocalRotation, damp);
            if (Quaternion.Angle(spriteRoot.localRotation, defaultLocalRotation) < 0.1f)
            {
                spriteRoot.localRotation = defaultLocalRotation;
            }
        }

        private float DampFactor()
        {
            return 1f - Mathf.Exp(-returnSpeed * Time.deltaTime);
        }
    }
}
