using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthManager : MonoBehaviour
{
    public int heartCount;
    public int maxHeartCount;
    private int enabledHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;
    private PlayerCombatStats combatStats;
    bool halfHeartNeeded = false;
    void Start()
    {
        combatStats = GameObject.Find("Player").GetComponent<PlayerCombatStats>();

    }

    private void Update()
    {

        enabledHearts = 0;
        maxHeartCount = (int)Mathf.Ceil(combatStats.maxHealth / 2.0f) - 1;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i <= maxHeartCount)
            {
                hearts[i].enabled = true;
                hearts[i].sprite = emptyHeart;
                enabledHearts++;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        if (combatStats.currentHealth % 2 == 1)
        {
            halfHeartNeeded = true;
            heartCount = (int)Mathf.Ceil(combatStats.currentHealth / 2.0f) - 1;
        }
        else
        {
            halfHeartNeeded = false;
            heartCount = (int)Mathf.Ceil(combatStats.currentHealth / 2.0f);
        }
        for (int i = 0; i < enabledHearts; i++)
        {
            if (i < heartCount)
            {
                hearts[i].sprite = fullHeart;
            }
            else if (halfHeartNeeded && i == heartCount)
            {
                hearts[i].sprite = halfHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;

            }
        }


    }




}
