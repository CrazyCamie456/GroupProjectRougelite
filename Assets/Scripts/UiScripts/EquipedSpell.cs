using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipedSpell : MonoBehaviour
{

    public Sprite fire;
    public Sprite water;
    public Sprite earth;
    public Sprite air;
    public Image equipedSpell;

    public IElementalAttack elementalAttack;
    private GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        elementalAttack = player.GetComponent<IElementalAttack>();
        string currentlyEquippedSpell = elementalAttack.Name();

        switch (currentlyEquippedSpell)
        {
            case "Fire":
                equipedSpell.enabled = true;
                equipedSpell.sprite = fire;
                break;
            case "Water":
                equipedSpell.enabled = true;
                equipedSpell.sprite = water;
                break;
            case "Earth":
                equipedSpell.enabled = true;
                equipedSpell.sprite = earth;
                break;
            case "Air":
                equipedSpell.enabled = true;
                equipedSpell.sprite = air;
                break;
            default:
                equipedSpell.enabled = false;
                break;
        }
    }
}