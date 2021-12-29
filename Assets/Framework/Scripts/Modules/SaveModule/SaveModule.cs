using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System;

/// <summary>
/// �浵ģ�顣����浵���ݵĴ�ȡ�ʹ浵�ļ��Ķ�д��
/// </summary>
public class SaveModule : IGameModule
{
    /// <summary>
    /// ע��ģ�飬��ʼ���浵��
    /// </summary>
    public static void Init() {
        ModuleDispatcher.Instance.Register<SaveModule>();
        DevUtils.Log("Inited", "SaveModule");

        StartupProcess();
    }

    // ���������ж��Ƿ��һ��������Ϸ��
    public static bool InitedWithGlobalData;
    public static bool ExistGlobalSaveFile => File.Exists(GlobalSaveFile);

    // ���������ж�ĳ����λ�Ƿ��д浵��
    public static bool ExistSlotSaveFile(int slotNum)
    {
        return saveSlots.ContainsKey(slotNum);
    }
    
    // �д浵�Ĳ�λ�б�
    public static Dictionary<int, string> saveSlots = new();
    
    private static string SavePath => Application.persistentDataPath + "/saves/";
    private static string SaveSlotPath(int slot)
    {
        return SavePath + slot + ".sav";
    }

    private static string GlobalSaveFile => SavePath + "global.sav";

    #region Game Start-up

    /// <summary>
    /// ֻ����������һ�³�ʼ���浵���̵�˳��
    /// </summary>
    private static void StartupProcess() {
        CreateSaveDir();
        ReadUsedSlots();
        InitWithGlobalData();
    }
    
    /// <summary>
    /// ����浵�ļ��в������򴴽���
    /// </summary>
    private static void CreateSaveDir() {
        if (!Directory.Exists(SavePath)) { 
            Directory.CreateDirectory(SavePath);
            DevUtils.Log("Save directory created.", "SaveModule");
        }
    }

    /// <summary>
    /// ɨһ��浵�ļ��У���ȡ���д浵�Ĳ�λ�б�
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
    /// ��ȡGlobal�浵�������������浵�������ж���ʼ���ɹ���
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
    /// ��ǰ����Ĵ浵��λ��
    /// ��ʹ��SwitchSaveSlot���л�����Ĳ�λ��
    /// </summary>
    public static int ActiveSlot { get; private set; }

    private static Dictionary<string, string> slotData = new Dictionary<string, string>();

    #region Read and Write Utils
    
    /// <summary>
    /// ���ʵ�data�е�����д��浵�ļ�path��
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
    /// �Ӵ浵�ļ�path��ȡ�ʵ����ݡ�
    /// </summary>
    /// <returns>��ȡ���Ĵʵ����ݡ�</returns>
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
    /// ����Global�浵�ļ���
    /// </summary>
    /// <param name="overwrite">�Ƿ������ǾɵĴ浵</param>
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
    
    // �����㲢����ɾ��Global�浵�ļ���

    /// <summary>
    /// ���ڴ��е�Global�浵����д���ļ���
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
    /// ��Global�浵�ļ��н��浵���ݶ����ڴ档
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
    /// ����ֵ��(key, value)�����ڴ��С�
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
    /// ���ڴ��ж�ȡ��Ŀkey��ֵ��
    /// </summary>
    /// <returns>�����û�ж�Ӧ�����ݣ����ص���string.Empty</returns>
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
    /// ��ȡ��һ�����õĿմ浵�ۡ�
    /// ��1��ʼ���ϵ�����
    /// </summary>
    /// <returns>�浵�۵�Index</returns>
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
    /// �л���ǰ����Ĵ浵��λ��slotNum��
    /// ���ȱ��浱ǰ��λ�����ݣ��л�֮���ٶ�ȡ�²�λ�����ݡ�
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
    /// �ڲ�λSlotNum�����µĴ浵�ļ���
    /// </summary>
    /// <param name="overwrite">�Ƿ������ǾɵĴ浵</param>
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
    /// ɾ����λSlotNum�Ĵ浵�ļ���
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
    /// ���ڴ��еĴ浵���ݴ��뵱ǰ�����λ�Ĵ浵�ļ���
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
    /// �ӵ�ǰ��λ�Ĵ浵�ļ����ȡ���ݵ��ڴ档
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
    /// ����ֵ��(key, value)�����ڴ档
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
    /// ���ڴ��ж�ȡ��Ŀkey��ֵ��
    /// </summary>
    /// <returns>�����û�ж�Ӧ�����ݣ����ص���string.Empty</returns>
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
    /// ������鵱ǰ�Ƿ񼤻��˴浵�ۡ�
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
