using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IInventoryItem {
	string Name { get; }
	Sprite Image {get;}
	GameObject gObj {get;}
	void OnPickup();
	void OnDrop(); 
}

public class InventoryEventArgs : EventArgs 
{
	public InventoryEventArgs(IInventoryItem item) {
		Item = item;
	}

	public IInventoryItem Item;	
}