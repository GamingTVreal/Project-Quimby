using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Singleton<PlayerStats>
{
    [SerializeField] private GameObject ScriptController;
    
    private StatsManager Stats;
    private int _maxMoney = 1000;
    private int _money;

    private int _maxEnergy = 20;
    private int _energy;

    private int _daysOfRentDue = 30;
    private int _maxRentDays = 30;

    public string Name;

    private void Awake()
    {
        ScriptController = GameObject.Find("Script Controller");
        Stats = ScriptController.GetComponent<StatsManager>();
    }
    private void Start()
    {
        GetMoney();
        GetEnergy();
    }
    public int GetMoney(bool max = false)
    {
        if (max)
            return _maxMoney;
        else
            return _money;
    }

    public void AdjustMoney(int amount, bool max = false)
    {
        if (max)
            _maxMoney += amount;
        else
            _money += amount;
    }

    public int GetEnergy(bool max = false)
    {
        if (max)
            return _maxEnergy;
        else
            return _energy;
    }

    public void AdjustEnergy(int amount, bool max = false)
    {
        if (max)
            _maxEnergy += amount;
        else
            _energy = amount;
    }

    public void Sleep()
    {
        Stats.Fadeout.SetBool("Fadeout", true);
        Stats.SnoringSFX.Play();
        Invoke("Sleep2", 4);
        
    }
    private void Sleep2()
    {
        _daysOfRentDue -= _daysOfRentDue;
        Stats.Fadeout.SetBool("Fadeout", false);
        AdjustEnergy(_maxEnergy, false);

    }
}
