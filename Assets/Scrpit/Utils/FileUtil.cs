using UnityEngine;
using System;
using System.IO;
using System.Windows.Forms;

public class FileUtil : ScriptableObject
{

    /// <summary>
    /// 创建文本
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="strData"></param>
    public static void CreateTextFile(string filePath, string fileName, string strData)
    {
        StreamWriter writer = null;
        try
        {
            writer = File.CreateText(filePath + "/" + fileName);
            writer.Write(strData);
        }
        catch (Exception e)
        {
            string strError = "创建文件失败-" + e.Message;
            LogUtil.logError(strError);
        }
        finally
        {
            if (writer != null)
                writer.Close();
        }
    }

    /// <summary>
    /// 加载文本
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static string LoadTextFile(string filePath)
    {
        StreamReader reader = null;
        try
        {
            reader = File.OpenText(filePath);
            String strData = reader.ReadToEnd();
            return strData;
        }
        catch (Exception e)
        {
            string strError = "读取文件失败-" + e.Message;
            LogUtil.log(strError);
            return null;
        }
        finally
        {
            if (reader != null)
                reader.Close();
        }
    }

    /// <summary>
    /// 打开文件选择弹窗
    /// </summary>
    public static string OpenFileDialog()
    {
        OpenFileDialog ofd = new OpenFileDialog();   //new一个方法
        ofd.InitialDirectory = "file://" + UnityEngine.Application.dataPath;  //定义打开的默认文件夹位置//定义打开的默认文件夹位置
        ofd.Filter = "jpg(*.jpg)|*.jpg|png(*.png)|*.png";
        if (ofd.ShowDialog() == DialogResult.OK)
        {
            return ofd.FileName;
        }
        else
        {
            return "";
        }
    }


    /// <summary>
    /// 复制文件到指定路径
    /// </summary>
    /// <param name="localFilePath">源文件路径</param>
    /// <param name="saveFilePath">存储路径</param>
    /// <param name="isReplace">若存储路径有相同文件是否替换</param>
    public static void CopyFile(string localFilePath, string saveFilePath, bool isReplace)
    {
        if (File.Exists(localFilePath))//必须判断要复制的文件是否存在
        {
            File.Copy(localFilePath, saveFilePath, isReplace);
        }
    }

    /// <summary>
    /// 创建文件夹
    /// </summary>
    /// <param name="directoryPath"></param>
    public static void CreateDirectory(string directoryPath)
    {
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
    }
}