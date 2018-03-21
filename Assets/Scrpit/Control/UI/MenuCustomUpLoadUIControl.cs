using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;


public class MenuCustomUpLoadUIControl : BaseUIControl
{
    public Image uploadImage;
    public Button uploadBT;

    public InputField inputName;

    private new void Awake()
    {
        base.Awake();
        uploadImage= CptUtil.getCptFormParentByName<Transform, Image>(transform,"UploadImage");
        uploadBT = CptUtil.getCptFormParentByName<Transform, Button>(transform, "UploadImage");
        uploadBT.onClick.AddListener(startUploadImage);
    }

    public override void openUI()
    {
        mUICanvas.enabled = true;
    }

    public override void closeUI()
    {
        mUICanvas.enabled = false;
    }

    public override void loadUIData()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 开始上传图片
    /// </summary>
    public void startUploadImage()
    {
        string targetPath = Application.persistentDataPath;
        string resPath = FileUtil.OpenFileDialog();
        if (resPath == null || resPath.Length == 0)
            return;
        FileUtil.CreateDirectory(targetPath + "/Custom");
        FileUtil.CopyFile(resPath, targetPath + "/Custom/1.png", true);

  
    }
}

