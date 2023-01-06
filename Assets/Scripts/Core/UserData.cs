using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Collections.Specialized.BitVector32;
using System.Runtime.InteropServices.ComTypes;

[Serializable]
public class UserData
{
    #region Variables
    public static UserData current;
    public string name = "NV Thân";
    public string lastActiveTime;
    public int Value_TPBank;
    public int Value_Techcombank;
    public int Value_MSB;
    public int Value_Credit;
    public int Value_Cash;


    #endregion

    #region Constructor

    public Expenditures expenditures = new Expenditures();
    public List<Expenditures> expendituresList = new List<Expenditures>();


    #endregion

    [Serializable]
    public class Expenditures
    {
        public int value;
        public string time;
        public Name_Bank nameBank;
        public Source source;
        public Target target;
        public string description;
    }

    #region Type
    public enum Name_Bank
    {
        TPBank,
        Techcombank,
        MSB,
        Credit,
        Cash
    }
    public enum Source
    {
        Income,
        Outcome,
        Debt,
        Loan
    }
    public enum Target
    {
        Breakfast,
        Lunch,
        Dinner,
        Snacks,
        Shopping,
        Vehicle,
        Debt,
        Loan,
        Room,
        Unknown
    }

    #endregion


    #region Function

    public void AddExpenditures(int value, string time, Name_Bank name_Bank, Source source, Target target, string description)
    {
        var expenditures = new Expenditures();
        expenditures.value = value;
        expenditures.time = time;
        expenditures.nameBank = name_Bank;
        expenditures.source = source;
        expenditures.target = target;
        expenditures.description = description;
        expendituresList.Add(expenditures);
        
    }

    #endregion


    [NonSerialized]
    public bool isDifferentDayFromLastSession;

    public void OnBeforeSerialize()
    {
        lastActiveTime = DateTime.UtcNow.ToString();
    }
    private void CreateDefaultData()
    {
        
    }
    public void OnAfterDeserialize()
    {
        isDifferentDayFromLastSession = false;
        if (DateTime.TryParse(lastActiveTime, out DateTime lastActiveDateTime))
        {
            var now = DateTime.UtcNow;
            if (now.Date != lastActiveDateTime.Date)
            {
                isDifferentDayFromLastSession = true;
            }
        }
    }

    #region DeviceSerialization
    private static bool isLoaded = false;

#if UNITY_EDITOR
    private static readonly string directory = "D:/";
#else
    private static readonly string directory = Application.persistentDataPath;
#endif
    private static string fileName = "Userdata_Dino_MonsterDemolition" + ".txt";

    public static bool IsLoaded
    {
        get
        {
            return isLoaded;
        }
    }
    public static void Save()
    {
        if (current == null || !isLoaded) return;

        current.OnBeforeSerialize();

        string json = JsonUtility.ToJson(current);
        PlayerPrefs.SetString("user_data", json);
        PlayerPrefs.Save();
    }

    public static bool Load(bool forceReload = false)
    {
        if (isLoaded == true && forceReload == false) return false;

        var playerDataJson = PlayerPrefs.GetString("user_data", null);
        current = JsonUtility.FromJson<UserData>(playerDataJson);

        if (current == null)
        {
            current = new UserData();
            current.CreateDefaultData();
            
        }

        current.OnAfterDeserialize();

        isLoaded = true;

        return isLoaded;
    }
    #endregion

}
