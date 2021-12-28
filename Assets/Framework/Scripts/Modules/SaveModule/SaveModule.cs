using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System;

public class SaveModule : IGameModule
{
    public static void Init() {
        ModuleDispatcher.Instance.Register<SaveModule>();
        DevUtils.Log("Inited", "SaveModule");

        StartupProcess();
    }

    public static bool InitedWithGlobalData;
    public static bool ExistGlobalSaveFile => File.Exists(GlobalSaveFile);

    public static bool ExistSlotSaveFile(int slotNum)
    {
        return saveSlots.ContainsKey(slotNum);
    }
    
    private static string SavePath => Application.persistentDataPath + "/saves/";
    private static string SaveSlotPath(int slot)
    {
        return SavePath + slot + ".sav";
    }

    private static string GlobalSaveFile => SavePath + "global.sav";
    private static Dictionary<int, string> saveSlots = new();

    #region Game Start-up

    private static void StartupProcess() {
        CreateSaveDir();
        ReadUsedSlots();
        InitWithGlobalData();
    }
    
    private static void CreateSaveDir() {
        if (!Directory.Exists(SavePath)) { 
            Directory.CreateDirectory(SavePath);
            DevUtils.Log("Save directory created.", "SaveModule");
        }
    }

    private static void ReadUsedSlots() {
        string[] slotFiles = Directory.GetFiles(SavePath);
        foreach (string slotFile in slotFiles)
        {
            if (int.TryParse(slotFile.Split(new char[] { '/' }).Last().Replace(".sav", ""), out int slotNum))
            {
                if (slotNum <= 0) continue;
                if (saveSlots.ContainsKey(slotNum))
                {
                    DevUtils.Log($"Duplicated slot save {slotNum} found", "SaveModule");
                    continue;
                }
                saveSlots.Add(slotNum, slotFile);
            }
        }

        ActiveSlot = 0;
    }

    private static void InitWithGlobalData()
    {
        if (ExistGlobalSaveFile)
        {
            ReadGlobalSaveFile();
            if (globalData.Count != 0)
            {
                DevUtils.Log("Successfully inited with global save data", "SaveModule");
                InitedWithGlobalData = true;
            }
        }
    }
    
    #endregion

    private static Dictionary<string, string> globalData = new Dictionary<string, string>();

    public static int ActiveSlot { get; private set; }

    private static Dictionary<string, string> slotData = new Dictionary<string, string>();

    #region Read and Write Utils
    
    private static void TryWriteToPath(string path, Dictionary<string, string> data) {
        DevUtils.Log($"Trying to write to {path}");
        if (data.Count == 0)
        {
            DevUtils.Log($"Nothing to write to {path}", "SaveModule");
            return;
        }
        StreamWriter sw = new StreamWriter(path);
        try
        {
            foreach (KeyValuePair<string, string> item in data)
            {
                sw.WriteLine(Encode(item));
            }
        }
        catch
        {

            DevUtils.Log($"Unknown error when writing to {path}", "SaveModule");
        }
        finally { sw.Close(); }
    }

    private static Dictionary<string, string> TryReadFromPath(string path) {
        DevUtils.Log($"Trying to read from {path}");
        StreamReader sr = new StreamReader(path);
        Dictionary<string, string> data = new Dictionary<string, string>();
        try
        {
            do
            {
                KeyValuePair<string, string> item = Decode(sr.ReadLine());
                data.Add(item.Key, item.Value);
            }
            while (sr.Peek() != -1);
        }
        catch
        {
            DevUtils.Log($"Unknown error when reading {path}", "SaveModule");
        }
        finally { sr.Close(); }

        if (data.Count == 0)
        {
            DevUtils.Log($"Nothing read from {path}", "SaveModule");
        }

        return data;
    }

    #endregion

    #region Global Save
    
    public static void CreateGlobalSaveFile(bool overwrite = false)
    {
        if (ExistGlobalSaveFile)
        {
            if (overwrite)
            {
                File.Create(GlobalSaveFile).Dispose();
            }
            else
            {
                DevUtils.Log("Global save file already exists", "SaveModule");
            }
            return;
        }
        File.Create(GlobalSaveFile).Dispose();
        DevUtils.Log($"Global save file created at {GlobalSaveFile}", "SaveModule");
    }

    public static void WriteGlobalSaveFile() {
        if (File.Exists(GlobalSaveFile))
        {
            TryWriteToPath(GlobalSaveFile, globalData);
        }
        else
        {
            DevUtils.Log("Global save file not exist", "SaveModule");
        }
    }

    public static void ReadGlobalSaveFile() {
        if (File.Exists(GlobalSaveFile)) {
            globalData = TryReadFromPath(GlobalSaveFile);
        }
        else
        {
            DevUtils.Log("Global save file not exist", "SaveModule");
        }
    }

    public static void SaveGlobal(string key, string value)
    {
        if (globalData.ContainsKey(key))
        {
            globalData[key] = value;
        }
        else
        {
            globalData.Add(key, value);
        }
    }

    public static string ReadGlobal(string key)
    {
        if (globalData.ContainsKey(key))
        {
            return globalData[key];
        }
        else
        {
            DevUtils.Log($"No data with key {key}", "SaveModule");
            return String.Empty;
        }
    }
    
    #endregion
    
    #region Slot Save

    public static int GetNextValidSlotNumber()
    {
        if (saveSlots.Count == 0) return 1;
        int validNumber = 1;
        while (saveSlots.ContainsKey(validNumber))
        {
            validNumber++;
        }

        return validNumber;
    }

    public static void SwitchSaveSlot(int slotNum)
    {
        if (ActiveSlot != 0)
        {
            WriteSlotSaveFile();
        }
        if (!ExistSlotSaveFile(slotNum))
        {
            CreateSlotSaveFile(slotNum);
        }
        ActiveSlot = slotNum;
        ReadSlotSaveFile();
    }

    public static void CreateSlotSaveFile(int slotNum, bool overwrite = false)
    {
        if (ExistSlotSaveFile(slotNum))
        {
            if (overwrite)
            {
                File.Create(saveSlots[slotNum]).Dispose();
                saveSlots[slotNum] = SaveSlotPath(slotNum);
            }
            else
            {
                DevUtils.Log($"Save file at slot {slotNum} already exists", "SaveModule");
            }

            return;
        }

        string slotPath = SaveSlotPath(slotNum);
        File.Create(slotPath).Dispose();
        saveSlots.Add(slotNum, slotPath);
        DevUtils.Log($"Save file for slot {slotNum} created at {slotPath}", "SaveModule");
    }

    public static void WriteSlotSaveFile()
    {
        if (ExistSlotSaveFile(ActiveSlot))
        {
            TryWriteToPath(SaveSlotPath(ActiveSlot), slotData);
        }
        else
        {
            DevUtils.Log($"Save file for slot {ActiveSlot} not exist", "SaveModule");
        }
    }

    public static void ReadSlotSaveFile()
    {
        if (ExistSlotSaveFile(ActiveSlot))
        {
            slotData = TryReadFromPath(SaveSlotPath(ActiveSlot));
        }
        else
        {
            DevUtils.Log($"Save file for slot {ActiveSlot} not exist", "SaveModule");
        }
    }

    public static void SaveData(string key, string value)
    {
        if (slotData.ContainsKey(key))
        {
            slotData[key] = value;
        }
        else
        {
            slotData.Add(key, value);
        }
    }

    public static string ReadData(string key)
    {
        if (slotData.ContainsKey(key))
        {
            return slotData[key];
        }
        else
        {
            DevUtils.Log($"No data with key {key} in slot {ActiveSlot}", "SaveModule");
            return String.Empty;
        }
    }
    
    #endregion

    #region Data Codecs

    public static KeyValuePair<string, string> Decode(string save) {
        string[] segments = save.Split(FrameworkGlobals.SAVEFILE_DELIMITER);
        if (segments.Length == 2) {
            return new KeyValuePair<string, string>(segments[0], segments[1]);
        }
        if (segments.Length < 1) {
            DevUtils.Log($"Empty or corrupted save item : {save}", "SaveModule");
        }
        else {
            DevUtils.Log($"Invalid Save file #{segments[1]}", "SaveModule");
        }
        return new KeyValuePair<string, string>();
    }

    public static string Encode(KeyValuePair<string, string> data) { 
        return data.Key + FrameworkGlobals.SAVEFILE_DELIMITER[0] + data.Value;
    }

    #endregion
}
