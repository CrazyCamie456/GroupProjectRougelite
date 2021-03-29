using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
	public int cost;
	BoxCollider2D bc;
	PlayerCombatStats pcs;
	TextMeshPro tmp;

	void Start()
	{
		tmp = GetComponentInChildren<TextMeshPro>();
		tmp.text = cost.ToString();
		pcs = GameObject.Find("Player").GetComponent<PlayerCombatStats>();
		pcs.currencyUpdateManager += UpdateLock;
		bc = GetComponent<BoxCollider2D>();
		bc.enabled = pcs.currency >= cost;
	}

	void UpdateLock()
    {
		bc.enabled = pcs.currency >= cost;
    }

	void OnDestroy()
	{
		pcs.currencyUpdateManager -= UpdateLock;
	}

	void OnEnable()
	{
		UpdateLock();
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.name == "Player")
		{
			pcs.currency -= cost;
			Destroy(gameObject);
		}
	}
}
