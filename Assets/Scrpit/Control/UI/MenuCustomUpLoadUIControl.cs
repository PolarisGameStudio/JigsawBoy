using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class MenuCustomUpLoadUIControl : BaseUIControl
{
    public Image uploadImage;
    public Button uploadBT;
    public Button submitBT;

    public InputField inputName;
    public InputField inputHorizontalNumber;
    public InputField inputVerticalNumber;

    private string uploadPath;


    private new void Awake()
    {
        base.Awake();
        uploadImage = CptUtil.getCptFormParentByName<Transform, Image>(transform, "UploadImage");
        uploadBT = CptUtil.getCptFormParentByName<Transform, Button>(transform, "UploadImage");

        inputName = CptUtil.getCptFormParentByName<Transform, InputField>(transform, "InputName");
        inputHorizontalNumber = CptUtil.getCptFormParentByName<Transform, InputField>(transform, "InputHorizontalNumber");
        inputVerticalNumber = CptUtil.getCptFormParentByName<Transform, InputField>(transform, "InputVerticalNumber");

        submitBT = CptUtil.getCptFormParentByName<Transform, Button>(transform, "SubmitBT");


        uploadBT.onClick.AddListener(showUploadImage);
        submitBT.onClick.AddListener(submitCustomData);
    }

    public override void openUI()
    {
        mUICanvas.enabled = true;
    }

    public override void closeUI()
    {
        mUICanvas.enabled = false;
        cleanData();
    }

    public override void loadUIData()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 开始上传图片
    /// </summary>
    public void showUploadImage()
    {
        uploadPath = FileUtil.OpenFileDialog();
        if (uploadPath == null || uploadPath.Length == 0)
            return;
        StartCoroutine(ResourcesManager.loadLocationImage(uploadPath, uploadImage));
    }

    /// <summary>
    /// 提交数据
    /// </summary>
    public void submitCustomData()
    {
        if (uploadImage.sprite == null)
        {
            LogUtil.log("没有图片");
            return;
        }
        if (uploadPath == null&& uploadPath.Length==0)
        {
            LogUtil.log("没有路径");
            return;
        }
        if (inputName.text == null || inputName.text.Length == 0)
        {
            LogUtil.log("没有拼图名字");
            return;
        }
        if (inputHorizontalNumber.text == null || inputHorizontalNumber.text.Length == 0)
        {
            LogUtil.log("没有拼图横向块数");
            return;
        }
        if (inputVerticalNumber.text == null || inputVerticalNumber.text.Length == 0)
        {
            LogUtil.log("没有拼图纵向块数");
            return;
        }
        if (!CheckUtil.checkIsNumber(inputHorizontalNumber.text))
        {
            LogUtil.log("横向块数 数据类型错误");
            return;

        }
        if (!CheckUtil.checkIsNumber(inputVerticalNumber.text))
        {
            LogUtil.log("纵向块数 数据类型错误");
            return;

        }
        string markFileName = SystemUtil.getUUID();
        FileUtil.CreateDirectory(CommonInfo.Custom_Res_Save_Path);
        FileUtil.CopyFile(uploadPath, CommonInfo.Custom_Res_Save_Path + "/"+ markFileName, true);

        List<PuzzlesInfoBean> listInfoData = DataStorageManage.getCustomPuzzlesInfoDSHandle().getAllData();
        if (listInfoData == null)
            listInfoData = new List<PuzzlesInfoBean>();

        PuzzlesInfoBean itemInfo = new PuzzlesInfoBean();
        itemInfo.id = 0;
        itemInfo.Name = inputName.text;
        itemInfo.Horizontal_number = Convert.ToInt32(inputHorizontalNumber.text);
        itemInfo.Vertical_number = Convert.ToInt32(inputVerticalNumber.text);
        itemInfo.Level = 1;
        itemInfo.Data_type = (int)JigsawResourcesEnum.Custom;
        itemInfo.Data_file_path = CommonInfo.Custom_Res_Save_Path + "/" ;
        itemInfo.Mark_file_name = markFileName;
        listInfoData.Add(itemInfo);
        DataStorageManage.getCustomPuzzlesInfoDSHandle().saveAllData(listInfoData);

        cleanData();
    }

    /// <summary>
    /// 清空数据
    /// </summary>
    public void cleanData()
    {
        if (uploadPath != null)
            uploadPath = null;
        if (uploadImage != null)
            uploadImage.sprite = null;
        if (inputName != null)
            inputName.text = null;
        if (inputHorizontalNumber != null)
            inputHorizontalNumber.text = null;
        if (inputVerticalNumber != null)
            inputVerticalNumber.text = null;
    }

}

