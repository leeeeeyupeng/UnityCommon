//! @file FileUtil.cs


using UnityEngine;
using System.IO;
using System;


//! @class FileUtil
//! @brief �ļ�����
public class FileUtil
{
	//! �浵Ŀ¼
	private static string m_savePath;
    //���ص�SavePath��������"/"
    public static string SavePath { get { return m_savePath + "/"; } }

	//! ��̬����
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

	//! ����ResourcesĿ¼���ļ�
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
    //! ����ResourcesĿ¼���ļ�
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

	//! д�浵
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

	//! ���浵
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

