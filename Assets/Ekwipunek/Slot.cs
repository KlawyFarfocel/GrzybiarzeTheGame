using UnityEngine;
using UnityEngine.UI;


public class Slot : MonoBehaviour
{
    //id slotu
    public int ID;
    public Item ItemInSlot;
    public Image ItemIcon;
    public Text ItemName;

    public InventoryManager InventoryManager;
    public CreateEqItems CreateEqItems;
    public UIManager UIManager;

    private void Awake()
    {
        CreateEqItems = CreateEqItems.Instance;
    }
    private void Update()
    {
        HandleTouchInput();
    }

    public void AddItemToSlot(Item item)
    {
        ItemInSlot = item;
        ItemIcon.sprite = item.Sprite;
        ItemIcon.gameObject.SetActive(true);
        ItemName.text = ItemInSlot.Name;
        ItemName.gameObject.SetActive(true);
    }

    //dla przedmiotow w eq na podstawie ID slotu jesli nie ma invetory menagera ktory wskazuje na eq to glowne
    public void AddItemToSlot(Item item, int SlotID)
    {
        Slot slot = this;
        if (slot.ID == SlotID)
        {
            ItemInSlot = item;
            ItemIcon.sprite = item.Sprite;
            ItemIcon.gameObject.SetActive(true);
            ItemName.text = ItemInSlot.Name;
            ItemName.gameObject.SetActive(true);
        }
    }

    public void RemoveItemFromSlot()
    {
        ItemInSlot = null;
        ItemIcon.sprite = null;
        ItemIcon.gameObject.SetActive(false);
        ItemName.text = null;
        ItemName.gameObject.SetActive(false);
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                Collider2D hitCollider = Physics2D.OverlapPoint(touchPosition);

                if (hitCollider != null)
                {
                    //jesli obiekt ma podpiety skrypt slot
                    Slot slotScript = hitCollider.GetComponent<Slot>();
                    if (slotScript != null)
                    {
                        if (hitCollider.gameObject.transform.parent.name == "EqSlots" && hitCollider.gameObject == gameObject)
                        {
                            if (ItemIcon.sprite != null)
                            {
                                RemoveItemFromSlot();
                            }
                            else
                            {
                                UIManager.Instance.ShowItemSelectionPanel(this);
                            }
                        }
                        //jesli rodziecem slotow jest panel Inv
                        if (hitCollider.gameObject.transform.parent.name == gameObject.transform.parent.name && hitCollider.gameObject == this.gameObject)
                        {
                            if (ItemInSlot != null)
                            {
                                UIManager.Instance.SelectItem(ItemInSlot);
                                RemoveItemFromSlot();
                            }
                            else
                            {
                                //slot jest pusty
                            }
                        }
                    }
                }
            }
        }
    }

}
