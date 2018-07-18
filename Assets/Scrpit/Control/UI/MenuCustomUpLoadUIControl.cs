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
    public Button cancelBT;

    public InputField inputName;
    public InputField inputHorizontalNumber;
    public InputField inputVerticalNumber;

    private string uploadPath;

    /// <summary>
    /// 预加载数据
    /// </summary>
    private PuzzlesInfoBean oldInfoBean;


    private new void Awake()
    {
        base.Awake();
        uploadImage = CptUtil.getCptFormParentByName<Transform, Image>(transform, "UploadImageContent");
        uploadBT = CptUtil.getCptFormParentByName<Transform, Button>(transform, "UploadImage");

        inputName = CptUtil.getCptFormParentByName<Transform, InputField>(transform, "InputName");
        inputHorizontalNumber = CptUtil.getCptFormParentByName<Transform, InputField>(transform, "InputHorizontalNumber");
        inputVerticalNumber = CptUtil.getCptFormParentByName<Transform, InputField>(transform, "InputVerticalNumber");

        submitBT = CptUtil.getCptFormParentByName<Transform, Button>(transform, "SubmitBT");
        cancelBT= CptUtil.getCptFormParentByName<Transform, Button>(transform, "CancelBT");

        cancelBT.onClick.AddListener(delegate{
            jumpSelectUI();
        });
    }

    public override void openUI()
    {
        mUICanvas.enabled = true;

        submitBT.onClick.RemoveAllListeners();
        uploadBT.onClick.RemoveAllListeners();
        if (oldInfoBean == null)
        {
            submitBT.onClick.AddListener(submitCustomData);
            uploadBT.onClick.AddListener(showUploadImage);
        }
        else
        {
            submitBT.onClick.AddListener(changeCustomData);
        }
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
    public override void refreshUI()
    {
    }
    /// <summary>
    /// 设置初始化数据
    /// </summary>
    /// <param name="infoBean"></param>
    public void setInitData(PuzzlesInfoBean infoBean)
    {
        this.oldInfoBean = infoBean;
        inputName.text = infoBean.Name;
        inputHorizontalNumber.text = infoBean.Horizontal_number + "";
        inputVerticalNumber.text = infoBean.Vertical_number + "";
        uploadPath = infoBean.Data_file_path + infoBean.Mark_file_name;
        StartCoroutine(ResourcesManager.loadAsyncLocationImage(uploadPath, uploadImage));
    }

    /// <summary>
    /// 开始上传图片
    /// </summary>
    public void showUploadImage()
    {
        uploadPath = FileUtil.OpenFileDialog();
        if (uploadPath == null || uploadPath.Length == 0) {
            uploadImage.color = new Color(0,0,0,0);
            return;
        }
      
        uploadImage.color = Color.white;
        StartCoroutine(ResourcesManager.loadAsyncLocationImage(uploadPath, uploadImage));
    }

    /// <summary>
    /// 修改数据
    /// </summary>
    public void changeCustomData()
    {
        if (!checkData())
            return;

        this.oldInfoBean.Name = inputName.text;
        this.oldInfoBean.Horizontal_number = Convert.ToInt32(inputHorizontalNumber.text);
        this.oldInfoBean.Vertical_number = Convert.ToInt32(inputVerticalNumber.text);

        //修改数据
        CustomPuzzlesInfoDSHandle handle = (CustomPuzzlesInfoDSHandle)DataStorageManage.getCustomPuzzlesInfoDSHandle();
        handle.changeData(this.oldInfoBean);

        jumpSelectUI();
    }

    /// <summary>
    /// 提交数据
    /// </summary>
    public void submitCustomData()
    {
        if (!checkData())
            return;

        string markFileName = SystemUtil.getUUID();
        PuzzlesInfoBean infoBean = new PuzzlesInfoBean();
        infoBean.id = 0;
        infoBean.Name = inputName.text;
        infoBean.Horizontal_number = Convert.ToInt32(inputHorizontalNumber.text);
        infoBean.Vertical_number = Convert.ToInt32(inputVerticalNumber.text);
        infoBean.Level = 1;
        infoBean.Data_type = (int)JigsawResourcesEnum.Custom;
        infoBean.Mark_file_name = markFileName;
        infoBean.Data_file_path = CommonInfo.Custom_Res_Save_Path + "/";
        FileUtil.CreateDirectory(CommonInfo.Custom_Res_Save_Path);
        FileUtil.CopyFile(uploadPath, CommonInfo.Custom_Res_Save_Path + "/" + markFileName, true);

        List<PuzzlesInfoBean> listInfoData = DataStorageManage.getCustomPuzzlesInfoDSHandle().getAllData();
        if (listInfoData == null)
            listInfoData = new List<PuzzlesInfoBean>();

        listInfoData.Add(infoBean);

        //保存数据
        CustomPuzzlesInfoDSHandle handle = (CustomPuzzlesInfoDSHandle)DataStorageManage.getCustomPuzzlesInfoDSHandle();
        handle.saveAllData(listInfoData);

        jumpSelectUI();
    }

    /// <summary>
    /// 检测提交数据
    /// </summary>
    /// <returns></returns>
    public bool checkData()
    {
        if (uploadImage.sprite == null)
        {
            LogUtil.log("没有图片");
            DialogManager.createToastDialog().setToastText("没有图片");
            return false;
        }
        if (uploadPath == null && uploadPath.Length == 0)
        {
            LogUtil.log("没有路径");
            DialogManager.createToastDialog().setToastText("没有路径");
            return false;
        }
        if (inputName.text == null || inputName.text.Length == 0)
        {
            LogUtil.log("没有拼图名字");
            DialogManager.createToastDialog().setToastText("没有拼图名字");
            return false;
        }
        if (inputHorizontalNumber.text == null || inputHorizontalNumber.text.Length == 0)
        {
            LogUtil.log("没有拼图横向块数");
            DialogManager.createToastDialog().setToastText("没有拼图横向块数");
            return false;
        }
        if (inputVerticalNumber.text == null || inputVerticalNumber.text.Length == 0)
        {
            LogUtil.log("没有拼图纵向块数");
            DialogManager.createToastDialog().setToastText("没有拼图纵向块数");
            return false;
        }
        if (!CheckUtil.checkIsNumber(inputHorizontalNumber.text))
        {
            LogUtil.log("横向块数 数据类型错误");
            DialogManager.createToastDialog().setToastText("横向块数 数据类型错误");
            return false;
        }
        if (!CheckUtil.checkIsNumber(inputVerticalNumber.text))
        {
            LogUtil.log("纵向块数 数据类型错误");
            DialogManager.createToastDialog().setToastText("纵向块数 数据类型错误");
            return false;
        }
        int horizontalNumber = Convert.ToInt32(inputHorizontalNumber.text);
        int verticalNumber = Convert.ToInt32(inputVerticalNumber.text);
        if (horizontalNumber > 50 || horizontalNumber < 2)
        {
            LogUtil.log("拼图横向块数必须小于等于50并且大于2");
            DialogManager.createToastDialog().setToastText("拼图横向块数必须小于等于50并且大于2");
            return false;
        }
        if (verticalNumber > 50 || verticalNumber < 2)
        {
            LogUtil.log("拼图纵向块数必须小于等于50并且大于2");
            DialogManager.createToastDialog().setToastText("拼图纵向块数必须小于等于50并且大于2");
            return false;
        }
        return true;
    }

    /// <summary>
    /// 清空数据
    /// </summary>
    public void cleanData()
    {
        if (uploadPath != null)
            uploadPath = null;
        if (uploadImage != null)
        {
            uploadImage.color = new Color(0, 0, 0, 0);
            uploadImage.sprite = null;
        }
        if (inputName != null)
            inputName.text = null;
        if (inputHorizontalNumber != null)
            inputHorizontalNumber.text = null;
        if (inputVerticalNumber != null)
            inputVerticalNumber.text = null;
        if (oldInfoBean != null)
            oldInfoBean = null;
    }


    /// <summary>
    /// 跳转到拼图选择界面（自定义模块）
    /// </summary>
    public void jumpSelectUI()
    {
        MenuSelectUIControl selectUIControl = mUIMasterControl.getUIByType<MenuSelectUIControl>(UIEnum.MenuSelectUI);
        mUIMasterControl.openUIByTypeAndCloseOther(UIEnum.MenuSelectUI);
        selectUIControl.setJigsawSelectData(JigsawResourcesEnum.Custom);
    }
}

