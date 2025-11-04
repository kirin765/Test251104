using UnityEngine;

namespace CollectionPrototype
{
    [DisallowMultipleComponent]
    public class CollectionCollectible : MonoBehaviour
    {
        [SerializeField]
        private int points = 1;

        [SerializeField]
        private float rotationSpeed = 90f;

        private CollectionCollectibleSpawner spawner;
        private bool isCollected;

        public int Points => Mathf.Max(1, points);

        internal void Initialize(CollectionCollectibleSpawner owner)
        {
            spawner = owner;
        }

        private void Update()
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime, Space.Self);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player"))
            {
                return;
            }

            if (isCollected)
            {
                return;
            }

            isCollected = true;

            if (CollectionGameManager.Instance != null)
            {
                CollectionGameManager.Instance.RegisterCollectiblePickup(this);
            }
        }

        internal void NotifyRemoved()
        {
            isCollected = true;
            spawner = null;
        }
    }
}
