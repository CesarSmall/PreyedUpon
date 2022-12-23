using UnityEngine;

namespace games
{
    public class DartManager : Spawner
    {
        #region variables

        [Tooltip("Set the location transform of where you want the darts to spawn")]
        [SerializeField] private Transform dartLoc;

        [Tooltip("Set the dart pack prefab to spawn in")]
        [SerializeField] private GameObject dartPack;

        [Tooltip("Set the amount of the item indicated that is going to be in the scene (based on prefab)")]
        [SerializeField] private int balloonCounter, dartCounter;

        [SerializeField] internal bool restart, won;
        private GameObject[] balloons;
        private bool enteredGameZone, isGameCurrentlyActive;
        internal int balloonsUnpopped, amountOfDartsRemaining;
        #endregion

        #region Unity calls

        private void Awake()
        {
            balloons = GameObject.FindGameObjectsWithTag("Balloon");
        }

        private void Start()
        {
            isGameCurrentlyActive = true;
            Spawn();
            SetItemCounters(isGameCurrentlyActive);
        }

        private void OnTriggerEnter(Collider other)
        {
            EnteredThrowCheckzone(other);
        }

        private void OnTriggerExit(Collider other)
        {
            ExitedThrowCheckZone(other);
        }

        private void Update()
        {
            CheckIfWon();
        }
        #endregion

        #region functions

        private void EnteredThrowCheckzone(Collider other)
        {
            if (other.gameObject.tag == "Dart") enteredGameZone = true;
        }

        private void ExitedThrowCheckZone(Collider other)
        {
            if (other.gameObject.tag == "Dart" && enteredGameZone)
            {
                enteredGameZone = false;
                amountOfDartsRemaining--;
            }
        }

        void CheckIfWon()
        {
            if (balloonsUnpopped == 0 && isGameCurrentlyActive)
            {
                won = true;
                isGameCurrentlyActive = false;
            }

            else if (balloonsUnpopped > 0 && amountOfDartsRemaining <= 0 && isGameCurrentlyActive)
            {
                ResetGame(isGameCurrentlyActive);
            }
            else return;
        }

        void Spawn()
        {
            if (isGameCurrentlyActive)
            {
                for (int i = 0; i < balloons.Length; i++)
                {
                    balloons[i].SetActive(true);
                }

                SpawnObject(dartLoc, dartPack);
                Debug.Log($"Spawned {amountOfDartsRemaining} Darts and { balloonsUnpopped} Balloons at Dart Game");
            }
        }

        private void ResetGame(bool gameState)
        {
            Spawn();
            SetItemCounters(gameState);
        }

        private void SetItemCounters(bool activeGame)
        {
            balloonsUnpopped = balloonCounter;
            amountOfDartsRemaining = dartCounter;
            activeGame = true;
        }
        #endregion
    }
}
