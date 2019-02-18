using UnityEngine;
using System;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using System.Runtime.InteropServices;

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
            String filePathName = filePath + "/" + fileName;
            DeleteFile(filePathName);
            writer = File.CreateText(filePathName);
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
        //OpenFileDialog ofd = new OpenFileDialog();   //new一个方法
        //ofd.InitialDirectory = "file://" + UnityEngine.Application.dataPath;  //定义打开的默认文件夹位置//定义打开的默认文件夹位置
        //ofd.Filter = "(*.jpg,*.png,*.jpeg,*.bmp)|*.jpg;*.png;*.jpeg;*.bmp";
        //if (ofd.ShowDialog() == DialogResult.OK)
        //{
        //    return ofd.FileName;
        //}
        //else
        //{
        //    return "";
        //}

            OpenFileName ofn = new OpenFileName();
            ofn.structSize = Marshal.SizeOf(ofn);
            //三菱(*.gxw)\0*.gxw\0西门子(*.mwp)\0*.mwp\0All Files\0*.*\0\0
            ofn.filter = "*.jpg\0*.png\0*.jpeg\0*.bmp\0\0";
            ofn.file = new string(new char[256]);
            ofn.maxFile = ofn.file.Length;
            ofn.fileTitle = new string(new char[64]);
            ofn.maxFileTitle = ofn.fileTitle.Length;
            ofn.initialDir = UnityEngine.Application.dataPath;//默认路径
            ofn.title = "Open Project";
            ofn.defExt = "JPG";//显示文件的类型
                               //注意 一下项目不一定要全选 但是0x00000008项不要缺少
            ofn.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;//OFN_EXPLORER|OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST| OFN_ALLOWMULTISELECT|OFN_NOCHANGEDIR
            if (DllTest.GetOpenFileName(ofn))
            {
             return ofn.file;
             }
        return "";
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

    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="filePath"></param>
    public static void DeleteFile(string filePath)
    {
        if (File.Exists(filePath))//必须判断要删除的文件是否存在
        {
            File.Delete(filePath);
        }
    }

    /// <summary>
    /// 删除指定文件目录下的所有文件
    /// </summary>
    /// <param name="fullPath">文件路径</param>
    public static bool DeleteAllFile(string fullPath)
    {
        //获取指定路径下面的所有资源文件  然后进行删除
        if (Directory.Exists(fullPath))
        {
            DirectoryInfo direction = new DirectoryInfo(fullPath);
            FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);

            Debug.Log(files.Length);

            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Name.EndsWith(".meta"))
                {
                    continue;
                }
                string FilePath = fullPath + "/" + files[i].Name;
                File.Delete(FilePath);
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// 保存图片到本地
    /// </summary>
    /// <param name="filePathName"></param>
    /// <param name="tex"></param>
    public static void ImageSaveLocal(string filePathName, Texture tex)
    {
        Texture2D saveImageTex = tex as Texture2D;
        FileStream newFs = new FileStream(filePathName, FileMode.Create, FileAccess.Write);
        byte[] bytes = saveImageTex.EncodeToJPG();
        newFs.Write(bytes, 0, bytes.Length);
        newFs.Close();
        newFs.Dispose();
    }
}