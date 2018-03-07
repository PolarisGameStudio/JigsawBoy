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
            if(reader!=null)
            reader.Close();
        } 
    }

    /// <summary>
    /// 打开文件选择弹窗
    /// </summary>
    public static void OpenFileDialog()
    {
        OpenFileDialog ofd = new OpenFileDialog();   //new一个方法
        ofd.InitialDirectory = "file://" + UnityEngine.Application.dataPath;  //定义打开的默认文件夹位置//定义打开的默认文件夹位置
        if (ofd.ShowDialog() == DialogResult.OK)
        {
            LogUtil.log(ofd.FileName);
        }
    }
}