using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class Inventory : MonoBehaviour
{
    
	private const int SLOTS = 9;
	private List<IInventoryItem> mItems = new List<IInventoryItem>();
	public event EventHandler<InventoryEventArgs> ItemAdded;
	public event EventHandler<InventoryEventArgs> ItemRemoved;
    
	public void AddItem(IInventoryItem item) {
		if (mItems.Count < SLOTS) {
			Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
			if (collider.enabled) {
				collider.enabled = false;
				mItems.Add(item);
				item.OnPickup();

				if (ItemAdded != null) {
					ItemAdded(this, new InventoryEventArgs(item));
				}
			}
		}
	}

    public bool ContainsItem(String itemName) 
    {
        foreach(IInventoryItem item in mItems)
        {
            if (item.Name == itemName)
            {
                return true;
            }
        }
        return false;
    }

	public void RemoveItem(IInventoryItem item) {
		if (mItems.Contains(item)) {
			mItems.Remove(item);

			item.OnDrop();
			Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
			if (collider != null) {
				collider.enabled = true;
			}
			if (ItemRemoved != null) {
				ItemRemoved(this, new InventoryEventArgs(item));
			}
		}
	}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
