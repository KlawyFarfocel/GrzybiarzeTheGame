using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public GameObject[] slots; // Assuming you have an array of slot GameObjects
    public bool[] isLocked;     // Boolean array indicating whether a slot is locked

    [SerializeField]
    private int numberOfSlotsToLock = 5; // Number of slots to lock from the end

    public GameObject lockPrefab; // Lock asset prefab

    private GameObject[] lockObjects; // Array to store instantiated lock objects



    void Start()
    {

        // Initialize the isLocked array based on your logic
        // For example, set all slots to locked initially
        isLocked = new bool[slots.Length];
        for (int i = 0; i < isLocked.Length; i++)
        {
            // Lock the specified number of slots from the end
            isLocked[i] = i >= (isLocked.Length - numberOfSlotsToLock);
        }

        // Initialize the array to store instantiated lock objects
        lockObjects = new GameObject[slots.Length];

        // Instantiate lock objects for each slot
        for (int i = 0; i < slots.Length; i++)
        {
            lockObjects[i] = Instantiate(lockPrefab, slots[i].transform.position, Quaternion.identity);
            lockObjects[i].transform.SetParent(slots[i].transform);
            lockObjects[i].transform.localScale = Vector3.one; // set scale to (1,1,1)
            lockObjects[i].transform.localPosition = Vector3.zero; // Set local position to (0, 0, 0)
            lockObjects[i].SetActive(isLocked[i]);
        }

        // Call a function to update the slot colors
        UpdateSlotColors();

    }

    void UpdateSlotColors()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            // Assuming your slots have a SpriteRenderer component for simplicity
            SpriteRenderer slotRenderer = slots[i].GetComponent<SpriteRenderer>();

            if (isLocked[i])
            {
                // Change the color to gray for locked slots
                slotRenderer.color = Color.gray;
            }
            else
            {
                // Reset the color to its original state for unlocked slots
                slotRenderer.color = Color.white;
            }
        }
    }

    // Call this function when a slot is unlocked (e.g., after a backpack upgrade)
    public void UnlockSlot(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < isLocked.Length)
        {
            isLocked[slotIndex] = false;
            UpdateSlotColors();
            // Disable the lock object when the slot is unlocked
            lockObjects[slotIndex].SetActive(false);
        }
    }

    public void AddItemToSlot(GameObject itemObject)
    {
        for (int i = 0; i < slots.Length;i++)
        {
            if (!isLocked[i])
            {
                SpriteRenderer slotRenderer = slots[i].GetComponent<SpriteRenderer>();
                Sprite itemSprite = itemObject.GetComponent<SpriteRenderer>().sprite;
                slotRenderer.sprite = itemSprite;

                break;

            }
        }
    }
}
