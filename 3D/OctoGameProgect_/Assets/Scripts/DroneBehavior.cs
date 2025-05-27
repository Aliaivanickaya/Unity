using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Octrees;
using System.Linq;

public class DroneBehavior : MonoBehaviour
{
    public int minePerStep = 2;
    public enum State { Idle, Moving, Mining, Returning, Repairing }
    public State currentState = State.Idle;

    public float mineTime = 5f;
    public int carriedResources = 0;
    public int maxCarry = 5;

    public Transform stationTarget;
    public OctreeGenerator octreeGenerator;
    public Mover mover;

    private System.Action onArriveCallback;

    void Update()
    {
    }

    public void Init(Transform station, OctreeGenerator generator, Mover moverComponent)
    {
        stationTarget = station;
        octreeGenerator = generator;
        mover = moverComponent;
        mover.octreeGenerator = octreeGenerator;
    }


    // Идти в точку
    public void GoToPoint(Vector3 targetPoint, System.Action onArrive = null)
    {
        currentState = State.Moving;
        onArriveCallback = onArrive;
        if (mover != null)
        {
            mover.GoTo(targetPoint, () =>
            {
                currentState = State.Idle;
                onArriveCallback?.Invoke();
            });
        }
    }

    // Добыть ресурс
    public void MineResource(Asteroid asteroid, System.Action onFinished = null)
    {
        currentState = State.Mining;
        StartCoroutine(MineCoroutine(asteroid, onFinished));
    }

    private IEnumerator MineCoroutine(Asteroid asteroid, System.Action onFinished)
    {
        transform.LookAt(asteroid.transform.position);
        while (carriedResources < maxCarry)
        {
            Debug.Log("Забирает астероид");
            int howMuchCanIMine = maxCarry - carriedResources;
            var mained = asteroid.Mine(howMuchCanIMine < minePerStep ? howMuchCanIMine : minePerStep);

            if (mained == 0) break;
            carriedResources += mained;
            yield return new WaitForSeconds(mineTime);
        }
        Debug.Log("Забрал");

        currentState = State.Idle;
        onFinished?.Invoke();
    }

    // Положить ресурс на базу
    public void DropResource(System.Action onFinished = null)
    {
        currentState = State.Returning;

        mover.GoTo(stationTarget.position, () =>
        {
            Station station = stationTarget.GetComponent<Station>();
            if (station != null)
            {
                station.ReceiveResources(carriedResources);
            }

            carriedResources = 0;
            currentState = State.Idle;
            onFinished?.Invoke();
        });
    }

    // Поиск ближайшего объекта
    public Asteroid FindClosest(List<Asteroid> asteroids)
    {
        return asteroids
            .Where(a => !a.Empty)
            .OrderBy(go => Vector3.Distance(go.transform.position, transform.position))
            .FirstOrDefault();
    }
}
