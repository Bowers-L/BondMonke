using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class PlaytestStats
{
    private const string fileName = "PlaytestData";
    private const string extension = ".txt";

    public int rolls { get; private set; }
    public int blockTime { get; private set; }
    public int sprintTime { get; private set; }
    public int lightAttacks { get; private set; }
    public int heavyAttacks { get; private set; }
    public int deaths { get; private set; }
    public int enemiesDefeated { get; private set; }
    public int lockOns { get; private set; }

    public PlaytestStats()
    {
        reset();
    }

    public void reset()
    {
        rolls = 0;
        blockTime = 0;
        sprintTime = 0;
        lightAttacks = 0;
        heavyAttacks = 0;
        deaths = 0;
        enemiesDefeated = 0;
        lockOns = 0;
    }

    public void incRolls()
    {
        rolls++;
    }

    public void incBlockTime()
    {
        blockTime++;
    }

    public void incSprintTime()
    {
        sprintTime++;
    }

    public void incLightAttacks()
    {
        lightAttacks++;
    }

    public void incHeavyAttacks()
    {
        heavyAttacks++;
    }

    public void incDeaths()
    {
        deaths++;
    }

    public void incEnemiesDefeated()
    {
        enemiesDefeated++;
    }

    public void incLockOns()
    {
        lockOns++;
    }

    public void PrintStats()
    {
        string path = "Assets/Resources/test.txt";

        string dest = Application.persistentDataPath + "/" + fileName + extension;
        //FileStream file;


        //if (File.Exists(dest)) file = File.OpenWrite(dest);
        //else file = File.Create(dest);
        

        StreamWriter writer = new StreamWriter(dest, true);
        writer.WriteLine("Data for this Playtest Session: \n");
        writer.WriteLine("Rolls: " + rolls);
        writer.WriteLine("Block Time: " + blockTime);
        writer.WriteLine("Sprint Time: " + sprintTime);
        writer.WriteLine("Light Attacks: " + lightAttacks);
        writer.WriteLine("Heavy Attacks: " + heavyAttacks);
        writer.WriteLine("Deaths: " + deaths);
        writer.WriteLine("Enemies Defeated: " + enemiesDefeated);
        writer.WriteLine("Lock Ons: " + lockOns);
        writer.Close();

        //file.Close();
    }
}

