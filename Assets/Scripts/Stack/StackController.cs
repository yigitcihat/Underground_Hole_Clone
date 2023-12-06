using System.Collections.Generic;
using UnityEngine;


public class StackController : MonoBehaviour
{
    public static StackController Instance;

    [Header("Stack Variables")] public List<Transform> stackList = new List<Transform>();
    internal List<Vector3> _displacements = new List<Vector3>();
    public int stackAmount;
    public Transform previousObject;
    public Transform lastObject;
    public Transform stackParent;
    private int stackLimit =25;
    public int ironCount = 0, woodCount = 0, plasticCount = 0;
    public List<GameObject> collectableList;
    public List<GameObject> ironList;
    public List<GameObject> woodList;
    public List<GameObject> plasticList;


    Vector3 _stackDirection;
    public float stackSpeed = 10f;
    public float stackSpacing = 0.1f;

    private void OnValidate()
    {
        _stackDirection = -Vector3.up;
    }

    private void OnEnable()
    {
        EventManager.AddListener(GameEvent.OnCapacityUpgrade, UpgradeStackLimit);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(GameEvent.OnCapacityUpgrade, UpgradeStackLimit);
    }

    private void Awake()
    {
        Instance = this;
        stackLimit = PlayerPrefs.GetInt(PlayerPrefKeys.StackLimit, stackLimit);
    }


    private void Update()
    {
        stackAmount = stackList.Count;
        CalculateStackDeflection();
    }

    public void PickUpItem(Transform pickedObject)
    {
        if (stackAmount >= stackLimit)
        {
            EventManager.Broadcast(GameEvent.OnCapacityFull);
            return;
        }

        var resource = pickedObject.GetComponent<Resource>();
        switch (resource.type)
        {
            case ResourceTypes.Iron:
                ironCount++;
                ironList.Add(resource.gameObject);
                break;
            case ResourceTypes.Plastic:
                plasticCount++;
                plasticList.Add(resource.gameObject);
                break;
            case ResourceTypes.Wood:
                woodCount++;
                woodList.Add(resource.gameObject);
                break;
            
        }
        _displacements.Add(Vector3.zero);
        pickedObject.GetComponent<Rigidbody>().useGravity = false;
        stackList.Add(pickedObject);
        pickedObject.SetParent(stackParent, true);
        previousObject = lastObject;
        lastObject = pickedObject;
        pickedObject.tag = "Collected";
    }

    private void CalculateStackDeflection()
    {
        if (stackList.Count == 0)
        {
            return;
        }

        for (var i = 0; i < stackList.Count; i++)
        {
            Vector3 targetPosition;

            if (i == stackList.Count - 1)
            {
                targetPosition = stackParent.position;
            }
            else
            {
                var stack = stackList[i + 1];
                targetPosition = stack.position - new Vector3(0, stackSpacing, 0);
            }

            var currentVelocity = _displacements[i];
            stackList[i].position =
                Vector3.SmoothDamp(stackList[i].position, targetPosition, ref currentVelocity, 0.01f);
        }
    }

    private void UpgradeStackLimit()
    {
        stackLimit += 5;
        PlayerPrefs.SetInt(PlayerPrefKeys.StackLimit, stackLimit);
    }

}