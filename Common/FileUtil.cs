//! @file FileUtil.cs


using UnityEngine;
using System.IO;
using System;


//! @class FileUtil
//! @brief 文件工具
public class FileUtil
{
	//! 存档目录
	private static string m_savePath;
    //返回的SavePath最后面带有"/"
    public static string SavePath { get { return m_savePath + "/"; } }

	//! 静态构造
	static FileUtil()
	{
		string path = Application.dataPath;

	#if UNITY_IPHONE
		if (!Application.isEditor)
		{
			path = path.Substring(0, path.LastIndexOf('/'));
			path = path.Substring(0, path.LastIndexOf('/'));
		}
		path += "/Documents";
	#else // Win32
		path += "/../Documents";
	#endif

		if (!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
		}

		m_savePath = path;
        Debug.Log(m_savePath);
	}

	//! 加载Resources目录下文件
	public static string LoadResourcesFile(string filename)
    {
#if UNITY_IPHONE
        TextAsset textAsset = Resources.Load(filename, typeof(TextAsset)) as TextAsset;
		return textAsset.text;
#else
        return ReadSave(filename);
        //TextAsset textAsset = Resources.Load(filename, typeof(TextAsset)) as TextAsset;
        //return textAsset.text;
#endif

        //return ReadSave(filename + ".xml");
	}
    //! 加载Resources目录下文件
    public static byte[] LoadResourcesFileByte(string filename)
    {
#if UNITY_IPHONE
        TextAsset textAsset = Resources.Load(filename, typeof(TextAsset)) as TextAsset;
		return textAsset.bytes;
#else
        return ReadSaveBinary(filename + ".xml");
        //TextAsset textAsset = Resources.Load(filename, typeof(TextAsset)) as TextAsset;
        //return textAsset.bytes;
#endif

        //return ReadSave(filename + ".xml");
    }

	//! 写存档
	public static void WriteSave(string filename, string content)
	{
		try
		{
			FileStream fileStream = new FileStream(m_savePath + "/" + filename, FileMode.Create);
            StreamWriter streamWriter = new StreamWriter(fileStream);
			streamWriter.Write(content);
			streamWriter.Close();
			fileStream.Close();
		}
		catch
		{
			Debug.Log("WriteSave error, filename:" + filename);
		}
	}

	//! 读存档
	public static string ReadSave(string filename)
	{
		//
		if (!File.Exists(m_savePath + "/" + filename))
		{
            Debug.Log("fuck " + m_savePath + "/"+ filename);
			return "";
		}
        Debug.Log("file name : " + m_savePath + "/" + filename);
		//
		try
		{
			FileStream fileStream = new FileStream(m_savePath + "/" + filename, FileMode.Open);
			StreamReader streamReader = new StreamReader(fileStream);
			string content = streamReader.ReadToEnd();
			streamReader.Close();
			fileStream.Close();

			return content;
		}
		catch
		{
			Debug.Log("ReadSave error, filename:" + filename);
			return "";
		}
	}
    public static byte[] ReadSaveBinary(string filename)
    {
        	//
		if (!File.Exists(m_savePath + "/" + filename))
		{
            Debug.Log("fuck " + m_savePath + filename);
			return null;
		}
		//
        try
        {
            FileStream stream = new FileStream(m_savePath + "/" + filename, FileMode.Open);
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, Convert.ToInt32(stream.Length));
            return buffer;
        }
        catch
        {
            Debug.Log("ReadSave error, filename:" + filename);
            return null;
        }
    }
    public static void WriteSaveBinary(string filename,byte[] content)
    {
        try
        {
            FileStream fileStream = new FileStream(m_savePath + "/" + filename, FileMode.Create);
            fileStream.Write(content, 0, content.Length);
            fileStream.Close();
        }
        catch
        {
            Debug.Log("WriteSave error, filename:" + filename);
        }
    }
}

