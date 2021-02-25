using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProjectilesOnExit : MonoBehaviour
{
	void OnTriggerExit2D(Collider2D collision)
	{
		Destroy(collision.gameObject);
	}
}
