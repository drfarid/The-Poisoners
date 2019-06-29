using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{

	public IInventoryItem Item { get; set; }
    // Start is called before the first frame update
    public void OnDrag(PointerEventData eventData) {
    	GameObject bucket = GameObject.Find("S_bucket");

		if (bucket.activeSelf) {

    		transform.position = Input.mousePosition;
    	}

    }

    public void OnEndDrag(PointerEventData eventData) {
        GameObject bucket = GameObject.Find("S_bucket");

        if (bucket.activeSelf) {
            Image imageHolder = GetComponent<Image>();
            transform.localPosition = Vector3.zero;
            imageHolder.enabled = false;
            imageHolder.sprite = null;
            Item.OnDrop();
        }
    	

        
    	
    }

   
   
}
