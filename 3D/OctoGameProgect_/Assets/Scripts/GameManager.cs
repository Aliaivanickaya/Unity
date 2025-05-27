using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Octrees;

public class GameManager : MonoBehaviour
{
    public Station station;
    public List<Asteroid> startAsteroids;
    public DroneBehavior firstDrone;
    public OctreeGenerator octreeGenerator;
    public float timeScale = 1;
    private void Start()
    {
    }
    private void Update()
    {
        Time.timeScale = timeScale;
    }
    public async void OnProduceDroneButton()
    {
        if (firstDrone.currentState == DroneBehavior.State.Idle)
        {
            if (firstDrone.carriedResources > 0)
            {
                firstDrone.DropResource(() =>
                {
                });
            }
            else
            {
                var asteroid = firstDrone.FindClosest(startAsteroids);
                if (asteroid != null)
                {
                    firstDrone.GoToPoint(asteroid.transform.position, () =>
                    {
                        Debug.Log(firstDrone.gameObject.name + " Дошел до   " + asteroid.gameObject.name);
                        firstDrone.MineResource(asteroid, () =>
                        {

                        });
                    });
                }
            }
            if (true)
            {

            }
        }

        void AssignDrone(DroneBehavior drone)
        {
            var asteroid = drone.FindClosest(startAsteroids);
            if (asteroid != null)
            {
                asteroid.Bind();
                Debug.Log(drone.gameObject.name + " направлен к  " + asteroid.gameObject.name);
                drone.GoToPoint(asteroid.transform.position, () =>
                {
                    Debug.Log(drone.gameObject.name + " Дошел до   " + asteroid.gameObject.name);
                    drone.MineResource(asteroid, () =>
                    {
                        asteroid.Release();
                        Debug.Log(drone.gameObject.name + " замайнил    " + asteroid.gameObject.name);
                        drone.DropResource(() =>
                        {
                            Debug.Log("Drone цикл завершил");
                            StartCoroutine(RepeatTask(drone));
                        });
                    });
                });
            }
            else
            {
                Debug.Log("Астероиды кончились");
            }
        }


        IEnumerator RepeatTask(DroneBehavior drone)
        {
            yield return null;
            AssignDrone(drone);
        }
    }
}
