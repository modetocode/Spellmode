using UnityEngine;

public class SpawnComponent : MonoBehaviour {

    [SerializeField]
    private GameObject[] spawnTemplates;

    private float minRespawnRateInSeconds = 2;
    private float maxRespawnRateInSeconds = 3;

    public void Start() {
        this.Spawn();
    }

    public void Spawn() {
        GameObject randomTemplate = spawnTemplates[Random.Range(0, spawnTemplates.Length)];
        Instantiate(randomTemplate, transform.position, Quaternion.identity);
        //Change this to start coroutine
        Invoke("Spawn", Random.Range(this.minRespawnRateInSeconds, this.maxRespawnRateInSeconds));
    }
}
