using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProjectilesOnExit : MonoBehaviour
{
	private void OnDrawGizmos()
	{
		BoxCollider2D bc = GetComponent<BoxCollider2D>();
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(new Vector3(bc.offset.x, bc.offset.y, 0.0f), new Vector3(bc.size.x, bc.size.y, 0.0f));
	}

	void OnTriggerExit2D(Collider2D collision)
	{
		Destroy(collision.gameObject);
	}
}