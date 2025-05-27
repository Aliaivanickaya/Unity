using System;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public int maxResources = 10;

    public bool Busy { get; private set; }
    public bool Empty { get; private set; }

    private int currentResources;
    private Vector3 baseScale;


    private void Awake()
    {
        baseScale = transform.localScale;
        currentResources = maxResources;
    }
    public int Mine(int amount)
    {
        if (currentResources < amount)
        {
            amount = currentResources;
            currentResources = 0;
        }
        else
        {
            currentResources -= amount;
        }
        if (currentResources <= 0)
        {
            OnEmpty();
        }
        UpdateView();
        return amount;
    }

    private void UpdateView()
    {
        transform.localScale = baseScale * ((float)currentResources / (float)maxResources);
    }

    private void OnEmpty()
    {
        Empty = true;
    }
    public void Bind()
    {
        Busy = true;

    }
    public void Release()
    {
        Busy = false;
    }


}
