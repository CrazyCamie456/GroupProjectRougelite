using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour
{

    private PlayerCombatStats combatStats;
    public Text currencyDisplay;
    void Start()
    {
        combatStats = GameObject.Find("Player").GetComponent<PlayerCombatStats>();
        combatStats.currencyUpdateManager += UpdateCurrency;
    }

    void UpdateCurrency()
    {
        currencyDisplay.text = combatStats.currency.ToString();
    }
}
