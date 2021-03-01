using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryOnDestroy : MonoBehaviour
{
	CombatStats cs;

	private void Start()
	{
		cs = GetComponent<CombatStats>();
	}

	private void OnDestroy()
	{
		if (cs.currentHealth <= 0)
		{
			SceneManager.LoadScene(2);
		}
	}
}
