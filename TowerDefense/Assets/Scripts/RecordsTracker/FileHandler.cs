using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
/// <summary>
/// Handles Data registries serialization(saving) and deserialization(loading) of files
/// </summary>
public static class FileHandler
{
    private const string scoresHistoryFilePath = "/ScoreHistory.scores";//File path for RunTimeHistoryRegistry file

    /// <summary>
    /// Serialize and save RunTimeHistoryRegistry data file
    /// </summary>
    /// <param name="runTimeHistory"></param>
    public static void SaveRunTimeHistoryRegistry(RunTimeHistoryRegistry runTimeHistory)
    {
        string path = Application.persistentDataPath + scoresHistoryFilePath;
        SaveRegistry(runTimeHistory, path);
    }

    /// <summary>
    /// Deserialize RunTimeHistoryRegistry data file
    /// </summary>
    /// <returns>Returns a loaded runTimeHistoryRegistry if there was one, 
    /// else - a new RunTimeHistoryRegistry</returns>
    public static RunTimeHistoryRegistry LoadRunTimeHistoryRegistry()
    {
        string path = Application.persistentDataPath + scoresHistoryFilePath;
        RunTimeHistoryRegistry loadedRegistry = LoadRegistry<RunTimeHistoryRegistry>(path);
        if (loadedRegistry == null)
        {
            return new RunTimeHistoryRegistry();
        }
        return loadedRegistry;
    }

    /// <summary>
    /// Serialize and save registry data file
    /// WARNING! make sure that input registry is Serializable
    /// </summary>
    /// <param name="registry">serializable registry</param>
    private static void SaveRegistry(object registry, string filePath)
    {
        if (File.Exists(filePath))
            File.Delete(filePath);

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(filePath);
        bf.Serialize(file, registry);
        file.Close();
    }

    /// <summary>
    /// Deserialize registry data file
    /// WARNING! make sure that input registry is Serializable
    /// </summary>
    /// <returns>Returns loaded file of Type T. Returns null if file is not found</returns>
    private static T LoadRegistry<T>(string filePath) where T : class
    {
        if (File.Exists(filePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filePath, FileMode.Open);
            if (file.Length > 0)
            {
                T runTimeHistory = (T)bf.Deserialize(file);
                file.Close();
                return runTimeHistory;
            }
            file.Close();
        }
        return null;
    }

}