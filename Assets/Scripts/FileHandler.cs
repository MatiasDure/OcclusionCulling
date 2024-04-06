using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class FileHandler
{
    private static string FIILE_PATH = Path.Combine(Application.persistentDataPath, "tracking.txt");

    public static void WriteToFile(string pText)
    {
        try
        {
        StreamWriter write = new StreamWriter(FIILE_PATH, true);
        write.WriteLine(pText);
        write.Close();

        } catch (Exception e)
        {
        }
    }
}
