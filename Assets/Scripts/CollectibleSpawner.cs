using System.Collections.Generic;
using UnityEngine;

namespace CollectionGame
{
    [DisallowMultipleComponent]
    public class CollectibleSpawner : MonoBehaviour
    {
        [SerializeField]
        private Collectible collectiblePrefab;

        [SerializeField]
        private Vector2 areaSize = new Vector2(12f, 6f);

        [SerializeField, Min(1)]
        private int maxActiveCollectibles = 6;

        private readonly List<Collectible> activeCollectibles = new List<Collectible>();
        private bool allowSpawning;

        public Bounds SpawnBounds
        {
            get
            {
                Vector3 size = new Vector3(areaSize.x, areaSize.y, 0f);
                return new Bounds(transform.position, size);
            }
        }

        public void BeginRound(int initialCount)
        {
            allowSpawning = true;
            ClearCollectibles();

            int spawnCount = Mathf.Clamp(initialCount, 1, maxActiveCollectibles);
            for (int i = 0; i < spawnCount; i++)
            {
                SpawnCollectible();
            }
        }

        public void EndRound()
        {
            allowSpawning = false;
            ClearCollectibles();
        }

        public void HandleCollected(Collectible collectible)
        {
            if (collectible == null)
            {
                return;
            }

            if (activeCollectibles.Remove(collectible))
            {
                collectible.NotifyRemoved();
                Destroy(collectible.gameObject);
            }

            if (allowSpawning && activeCollectibles.Count < maxActiveCollectibles)
            {
                SpawnCollectible();
            }
        }

        private void SpawnCollectible()
        {
            if (collectiblePrefab == null)
            {
                Debug.LogWarning("Collectible prefab is not assigned on the spawner.");
                return;
            }

            Vector3 position = GetRandomPosition();
            Collectible collectible = Instantiate(collectiblePrefab, position, Quaternion.identity, transform);
            collectible.Initialize(this);
            activeCollectibles.Add(collectible);
        }

        private Vector3 GetRandomPosition()
        {
            Vector2 halfSize = areaSize * 0.5f;
            float x = Random.Range(-halfSize.x, halfSize.x);
            float y = Random.Range(-halfSize.y, halfSize.y);
            Vector3 offset = new Vector3(x, y, 0f);
            return transform.position + offset;
        }

        private void ClearCollectibles()
        {
            for (int i = activeCollectibles.Count - 1; i >= 0; i--)
            {
                Collectible collectible = activeCollectibles[i];
                if (collectible != null)
                {
                    collectible.NotifyRemoved();
                    Destroy(collectible.gameObject);
                }
            }

            activeCollectibles.Clear();
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Bounds bounds = SpawnBounds;
            Gizmos.DrawWireCube(bounds.center, bounds.size);
        }
#endif
    }
}
