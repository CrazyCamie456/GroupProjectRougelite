using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingElementGenerator : MonoBehaviour
{
	public AddElementOnPickup pickupA;
	public AddElementOnPickup pickupB;
	// Fire, Water, Earth, Air
	public List<Sprite> sprites;

	void Start()
	{
		int temp = Mathf.FloorToInt(Random.Range(1, 5));
		int temp2 = Mathf.FloorToInt(Random.Range(1, 5));

		if (temp == 5 || temp2 == 5)
			Debug.Log("Just being certain I'm rnging between 1 and 4");

		while (temp == temp2)
			temp = Mathf.FloorToInt(Random.Range(1, 5));

		pickupA.element = (Element)temp;
		pickupA.GetComponent<SpriteRenderer>().sprite = sprites[temp - 1];
		pickupB.element = (Element)temp2;
		pickupB.GetComponent<SpriteRenderer>().sprite = sprites[temp2 - 1];
	}
}
