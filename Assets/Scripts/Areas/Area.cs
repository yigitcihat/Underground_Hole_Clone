using UnityEngine;

public abstract class Area : MonoBehaviour
{
    [SerializeField] protected int requiredIron;
    [SerializeField] protected int requiredWood;
    [SerializeField] protected int requiredPlastic;

    protected int currentIron;
    protected int currentWood;
    protected int currentPlastic;

    protected bool isUnlocked;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerMovement>();
        if (player is not null)
        {
            TryUnlock();
        }
    }

    protected virtual void UnlockArea()
    {
        isUnlocked = true;
        Debug.Log($"{gameObject.name} is unlocked!");
    }

    protected void AddIron(int amount)
    {
        currentIron += amount;
    }

    protected void AddWood(int amount)
    {
        currentWood += amount;
    }

    protected void AddPlastic(int amount)
    {
        currentPlastic += amount;
    }


    protected void RemoveRequiredIron()
    {
        currentIron -= requiredIron;
        if (currentIron < 0)
        {
            currentIron = 0;
        }
    }

    protected void TryUnlock()
    {
        if (!isUnlocked && HasRequiredResources())
        {
            RemoveRequiredIron();
            UnlockArea();
        }
    }

    protected bool HasRequiredResources()
    {
        return currentIron >= requiredIron && currentWood >= requiredWood && currentPlastic >= requiredPlastic;
    }
}
