using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System;

/// <summary>
/// 存档模块。管理存档数据的存取和存档文件的读写。
/// </summary>
public class SaveModule : IGameModule
{
    /// <summary>
    /// 注册模块，初始化存档。
    /// </summary>
    public static void Init() {
        ModuleDispatcher.Instance.Register<SaveModule>();
        DevUtils.Log("Inited", "SaveModule");

        StartupProcess();
    }

    // 可以用来判定是否第一次启动游戏。
    public static bool InitedWithGlobalData;
    public static bool ExistGlobalSaveFile => File.Exists(GlobalSaveFile);

    // 可以用来判定某个槽位是否有存档。
    public static bool ExistSlotSaveFile(int slotNum)
    {
        return saveSlots.ContainsKey(slotNum);
    }
    
    // 有存档的槽位列表。
    public static Dictionary<int, string> saveSlots = new();
    
    private static string SavePath => Application.persistentDataPath + "/saves/";
    private static string SaveSlotPath(int slot)
    {
        return SavePath + slot + ".sav";
    }

    private static string GlobalSaveFile => SavePath + "global.sav";

    #region Game Start-up

    /// <summary>
    /// 只是用来梳理一下初始化存档过程的顺序。
    /// </summary>
    private static void StartupProcess() {
        CreateSaveDir();
        ReadUsedSlots();
        InitWithGlobalData();
    }
    
    /// <summary>
    /// 如果存档文件夹不存在则创建。
    /// </summary>
    private static void CreateSaveDir() {
        if (!Directory.Exists(SavePath)) { 
            Directory.CreateDirectory(SavePath);
            DevUtils.Log("Save directory created.", "SaveModule");
        }
    }

    /// <summary>
    /// 扫一遍存档文件夹，获取已有存档的槽位列表。
    /// </summary>
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

    /// <summary>
    /// 读取Global存档，如果读到任意存档数据则判定初始化成功。
    /// </summary>
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

    /// <summary>
    /// 当前激活的存档槽位。
    /// 请使用SwitchSaveSlot来切换激活的槽位。
    /// </summary>
    public static int ActiveSlot { get; private set; }

    private static Dictionary<string, string> slotData = new Dictionary<string, string>();

    #region Read and Write Utils
    
    /// <summary>
    /// 将词典data中的数据写入存档文件path。
    /// </summary>
    private static void TryWriteToPath(string path, Dictionary<string, string> data) {
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

    /// <summary>
    /// 从存档文件path读取词典数据。
    /// </summary>
    /// <returns>读取到的词典数据。</returns>
    private static Dictionary<string, string> TryReadFromPath(string path) {
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
    
    /// <summary>
    /// 创建Global存档文件。
    /// </summary>
    /// <param name="overwrite">是否允许覆盖旧的存档</param>
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
    
    // 不，你并不想删除Global存档文件。

    /// <summary>
    /// 将内存中的Global存档数据写入文件。
    /// </summary>
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

    /// <summary>
    /// 从Global存档文件中将存档数据读入内存。
    /// </summary>
    public static void ReadGlobalSaveFile() {
        if (File.Exists(GlobalSaveFile)) {
            globalData = TryReadFromPath(GlobalSaveFile);
        }
        else
        {
            DevUtils.Log("Global save file not exist", "SaveModule");
        }
    }

    /// <summary>
    /// 将键值对(key, value)存入内存中。
    /// </summary>
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

    /// <summary>
    /// 从内存中读取条目key的值。
    /// </summary>
    /// <returns>如果并没有对应的数据，返回的是string.Empty</returns>
    public static string ReadGlobal(string key)
    {
        if (globalData.ContainsKey(key))
        {
            return globalData[key];
        }
        else
        {
            DevUtils.Log($"No data with key {key}", "SaveModule");
            return string.Empty;
        }
    }
    
    #endregion
    
    #region Slot Save

    /// <summary>
    /// 获取下一个可用的空存档槽。
    /// 从1开始向上递增。
    /// </summary>
    /// <returns>存档槽的Index</returns>
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

    /// <summary>
    /// 切换当前激活的存档槽位到slotNum。
    /// 会先保存当前槽位的数据，切换之后再读取新槽位的数据。
    /// </summary>
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

    /// <summary>
    /// 在槽位SlotNum创建新的存档文件。
    /// </summary>
    /// <param name="overwrite">是否允许覆盖旧的存档</param>
    public static void CreateSlotSaveFile(int slotNum, bool overwrite = false)
    {
        if (slotNum == 0)
        {
            DevUtils.Log("Save slot 0 is reserved", "SaveModule");
            return;
        }
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

    /// <summary>
    /// 删除槽位SlotNum的存档文件。
    /// </summary>
    public static void DeleteSlotSaveFile(int slotNum)
    {
        if (ExistSlotSaveFile(slotNum))
        {
            File.Delete(saveSlots[slotNum]);
            saveSlots.Remove(slotNum);
            return;
        }
        DevUtils.Log($"Save file for slot {slotNum} not exist", "SaveModule");
    }

    /// <summary>
    /// 将内存中的存档数据存入当前激活槽位的存档文件。
    /// </summary>
    public static void WriteSlotSaveFile()
    {
        if (!CheckSlotSaveLoaded()) return;
        if (ExistSlotSaveFile(ActiveSlot))
        {
            TryWriteToPath(SaveSlotPath(ActiveSlot), slotData);
        }
        else
        {
            DevUtils.Log($"Save file for slot {ActiveSlot} not exist", "SaveModule");
        }
    }

    /// <summary>
    /// 从当前槽位的存档文件里读取数据到内存。
    /// </summary>
    public static void ReadSlotSaveFile()
    {
        if (!CheckSlotSaveLoaded()) return;
        if (ExistSlotSaveFile(ActiveSlot))
        {
            slotData = TryReadFromPath(SaveSlotPath(ActiveSlot));
        }
        else
        {
            DevUtils.Log($"Save file for slot {ActiveSlot} not exist", "SaveModule");
        }
    }

    /// <summary>
    /// 将键值对(key, value)存入内存。
    /// </summary>
    public static void SaveData(string key, string value)
    {
        if (!CheckSlotSaveLoaded()) return;
        if (slotData.ContainsKey(key))
        {
            slotData[key] = value;
        }
        else
        {
            slotData.Add(key, value);
        }
    }

    
    /// <summary>
    /// 从内存中读取条目key的值。
    /// </summary>
    /// <returns>如果并没有对应的数据，返回的是string.Empty</returns>
    public static string ReadData(string key)
    {
        if (!CheckSlotSaveLoaded()) return string.Empty;
        if (slotData.ContainsKey(key))
        {
            return slotData[key];
        }
        else
        {
            DevUtils.Log($"No data with key {key} in slot {ActiveSlot}", "SaveModule");
            return string.Empty;
        }
    }

    /// <summary>
    /// 用来检查当前是否激活了存档槽。
    /// </summary>
    private static bool CheckSlotSaveLoaded()
    {
        if (ActiveSlot == 0)
        {
            DevUtils.Log("Slot save not loaded", "SaveModule");
            return false;
        }

        return true;
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
