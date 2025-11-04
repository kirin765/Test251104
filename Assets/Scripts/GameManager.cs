using UnityEngine;
namespace CollectionGame
{
    [DisallowMultipleComponent]
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("Round Settings")]
        [SerializeField]
        private float roundDuration = 60f;

        [SerializeField]
        private int startingCollectibles = 5;

        [Header("Scene References")]
        [SerializeField]
        private PlayerController2D player;

        [SerializeField]
        private CollectibleSpawner spawner;

        private float timeRemaining;
        private int score;
        private bool isRoundActive;
        private string stateMessage = string.Empty;
        private GUIStyle hudStyle;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            ResetRoundState();
            stateMessage = "Press Space to Start";
        }

        private void Update()
        {
            if (!isRoundActive)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    StartRound();
                }

                return;
            }

            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0f)
            {
                timeRemaining = 0f;
                EndRound();
            }
        }

        public void RegisterCollectiblePickup(Collectible collectible)
        {
            if (!isRoundActive || collectible == null)
            {
                return;
            }

            score += collectible.Points;
            if (spawner != null)
            {
                spawner.HandleCollected(collectible);
            }
        }

        private void StartRound()
        {
            score = 0;
            timeRemaining = Mathf.Max(5f, roundDuration);
            isRoundActive = true;

            if (player != null)
            {
                player.transform.position = Vector3.zero;
            }

            if (spawner != null)
            {
                spawner.BeginRound(Mathf.Max(1, startingCollectibles));
            }
            stateMessage = "Collect as much as you can!";
        }

        private void EndRound()
        {
            isRoundActive = false;
            if (spawner != null)
            {
                spawner.EndRound();
            }
            stateMessage = $"Time's up! Final Score: {score}\nPress Space to Restart";
        }

        private void ResetRoundState()
        {
            score = 0;
            timeRemaining = roundDuration;
        }

        private void OnGUI()
        {
            if (hudStyle == null)
            {
                hudStyle = new GUIStyle(GUI.skin.label)
                {
                    fontSize = 24,
                    alignment = TextAnchor.UpperLeft
                };
            }

            Rect scoreRect = new Rect(20f, 20f, 400f, 30f);
            GUI.Label(scoreRect, $"Score: {score}", hudStyle);

            Rect timerRect = new Rect(20f, 50f, 400f, 30f);
            GUI.Label(timerRect, $"Time: {Mathf.CeilToInt(timeRemaining)}", hudStyle);

            Rect stateRect = new Rect(20f, 90f, 600f, 60f);
            GUI.Label(stateRect, stateMessage, hudStyle);
        }
    }
}
