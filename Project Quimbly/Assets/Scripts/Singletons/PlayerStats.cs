using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Singleton<PlayerStats>
{
    private int _maxMoney = 1000;
    public int Money;

    private int _maxEnergy = 20;
    public int Energy;

    public int CurrentJob = 0;
    public int JobLevel = 0;
    public string Name;

    
    public int GetMoney(bool max = false)
    {
        if (max)
            return _maxMoney;
        else
            return Money;
    }

    public void SetMoney(int amount)
    {
        Money = amount;
    }

    public void AdjustMoney(int amount, bool max = false)
    {
        if (max)
            _maxMoney += amount;
        else
            Money += amount;
    }

    public int GetEnergy(bool max = false)
    {
        if (max)
            return _maxEnergy;
        else
            return Energy;
    }

    public void AdjustEnergy(int amount, bool max = false)
    {
        if (max)
            Energy = _maxEnergy;
        else
            Energy += amount;
    }

    public void SetEnergy(int amount)
    {
        Energy = amount;
    }
}
