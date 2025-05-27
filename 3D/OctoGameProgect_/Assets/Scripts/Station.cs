using UnityEngine;
using Octrees;

public class Station : MonoBehaviour
{
    public int storedResources = 0;
    public int hp = 100;

    public int droneCost = 10; 
    public GameObject dronePrefab;
    public Transform spawnPoint;
    int i = 0;
    public void ReceiveResources(int amount)
    {
        storedResources += amount;
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        if (hp <= 0) Debug.Log("Станция уничтожена!");
    }

    public void Repair(int amount)
    {
        hp += amount;
        if (hp > 100) hp = 100;
    }

    public bool TryProduceDrone(out DroneBehavior produced, OctreeGenerator generator)
    {
        produced = null;

        if (storedResources >= droneCost)
        {
            storedResources -= droneCost;
            GameObject droneObj = Instantiate(dronePrefab, spawnPoint.position, Quaternion.identity);
            droneObj.name += i++;

            DroneBehavior drone = droneObj.GetComponent<DroneBehavior>();
            Mover moverComponent = droneObj.GetComponent<Mover>();

            drone.Init(this.transform, generator, moverComponent);
            produced = drone;
            return true;
        }

        return false;
    }

}
