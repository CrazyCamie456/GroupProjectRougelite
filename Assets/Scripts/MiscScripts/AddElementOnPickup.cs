using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddElementOnPickup : MonoBehaviour
{
	public Element element;

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			collision.gameObject.GetComponent<ElementAttack>().AddElement(element);
			Destroy(gameObject);
		}
	}
}
