using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] doors;
    [SerializeField] private Spawner[] spawners;
    public bool isActive;

    private int totalSpawners = 0;
    private int spawnersDie = 0;
    private void Start()
    {
        foreach (Spawner spawner in spawners)
        {
            totalSpawners++;
            spawner.deathHandler += DetectSpawner;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isActive)
        {
            PlayerMovement player = other.transform.GetComponent<PlayerMovement>();
            if (player == null) return;
            ChangeDors(true);
            ActivateSpawners();

            Debug.Log("Spawners activate");
        }
        
        
    }

    private void ChangeDors(bool active)
    {
        foreach (GameObject door in doors)
        {
            door.SetActive(active);
        }
    }

    private void ActivateSpawners()
    {
        foreach (Spawner spawner in spawners)
        {
            spawner.ActivateSpawn();
        }
    }
    
    private void DetectSpawner()
    {
        spawnersDie++;
        if (spawnersDie >= totalSpawners)
        {
            ChangeDors(active: false);
            Destroy(gameObject);
        }
    }
}
