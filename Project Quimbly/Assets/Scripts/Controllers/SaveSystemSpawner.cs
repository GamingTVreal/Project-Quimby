
using UnityEngine;

namespace ProjectQuimbly.Controllers
{
    public class SaveSystemSpawner : MonoBehaviour
    {
        // CONFIG DATA
        [Tooltip("This prefab will only be spawned once and persisted between " +
        "scenes.")]
        [SerializeField] GameObject saveSystemPrefab = null;

        // PRIVATE STATE
        static bool hasSpawned = false;

        private void Awake() 
        {
            if(hasSpawned) return;

            SpawnPersistentObjects();

            hasSpawned = true;
        }

        private void SpawnPersistentObjects()
        {
            GameObject persistentObject = Instantiate(saveSystemPrefab);
            DontDestroyOnLoad(persistentObject);
        }
    }
}
