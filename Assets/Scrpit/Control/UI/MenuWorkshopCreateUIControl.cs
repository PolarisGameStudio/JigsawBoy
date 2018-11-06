using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;
using Steamworks;

public class MenuWorkshopCreateUIControl : BaseUIControl
{

    public Button exitBt;
    public Button uploadBT;
    public Button submitBT;

    public Text tvHint;
    public Text tvCancel;
    public Text tvSubmit;

    public InputField inputName;
    public InputField inputDescription;
    public InputField inputHorizontalNumber;
    public InputField inputVerticalNumber;

    public Text inputNameTitle;
    public Text inputDescriptionTitle;
    public Text inputHorizontalNumberTitle;
    public Text inputVerticalNumberTitle;

    public Image uploadImage;
    public string uploadPath;

    public string fileName;
    public string fileNamePath;
    public string fileSavePath;

    private new void Awake()
    {
        base.Awake();
        fileSavePath = UnityEngine.Application.streamingAssetsPath + "/SteamWorkshopPicUpdate";

        exitBt.onClick.AddListener(ExitOnClick);
        uploadBT.onClick.AddListener(ShowUploadImageOnClick);
        submitBT.onClick.AddListener(SubmitOnClick);
        refreshUI();
    }
    public override void closeUI()
    {
        mUICanvas.enabled = false;
        ClearData();
    }

    public override void loadUIData()
    {
        throw new System.NotImplementedException();
    }

    public override void openUI()
    {
        mUICanvas.enabled = true;
    }

    public override void refreshUI()
    {
        inputNameTitle.text = CommonData.getText(33);
        inputDescriptionTitle.text = CommonData.getText(123);
        inputHorizontalNumberTitle.text = CommonData.getText(34);
        inputVerticalNumberTitle.text = CommonData.getText(35);
        tvCancel.text = CommonData.getText(36);
        tvSubmit.text = CommonData.getText(37);
    }

    /// <summary>
    /// 开始上传图片
    /// </summary>
    public void ShowUploadImageOnClick()
    {
        SoundUtil.playSoundClip(AudioButtonOnClickEnum.btn_sound_1);
        uploadPath = FileUtil.OpenFileDialog();
        if (uploadPath == null || uploadPath.Length == 0)
        {
            uploadImage.color = new Color(0, 0, 0, 0);
            return;
        }

        uploadImage.color = Color.white;
        SelectCallBack callBack = new SelectCallBack(this);
        StartCoroutine(ResourcesManager.LoadAsyncLocationImage(uploadPath, uploadImage, callBack));
    }

    /// <summary>
    /// 提交按钮
    /// </summary>
    private void SubmitOnClick()
    {
        if (!CheckData())
        {
            return;
        }

        SteamWorkshopUpdateBean updateData = new SteamWorkshopUpdateBean();
        updateData.title = inputName.text;
        updateData.description = inputDescription.text;

        PuzzlesInfoBean infoBean = new PuzzlesInfoBean();
        infoBean.mark_file_name = fileName;
        infoBean.horizontal_number= Convert.ToInt32(inputHorizontalNumber.text);
        infoBean.Vertical_number = Convert.ToInt32(inputVerticalNumber.text);
        string infoStr=  JsonUtil.ToJson(infoBean);
        updateData.metadata = infoStr;
        updateData.content = fileSavePath;
        updateData.preview = fileNamePath + "_Thumb";
        //SteamWorkshopHandle.CreateWorkshopItem(this, updateData,new UpdateCallBack(this));
    }

    /// <summary>
    /// 退出按钮
    /// </summary>
    private void ExitOnClick()
    {
        SoundUtil.playSoundClip(AudioButtonOnClickEnum.btn_sound_2);
        mUIMasterControl.openUIByTypeAndCloseOther(UIEnum.MenuWorkshop);
    }

    /// <summary>
    /// 上传图片回调
    /// </summary>
    public class UpdateCallBack : ISteamWorkshopUpdateCallBack
    {
        public MenuWorkshopCreateUIControl customUI;
        public UpdateCallBack(MenuWorkshopCreateUIControl customUI)
        {
            this.customUI = customUI;
        }
        public void UpdateFail(SteamWorkshopUpdateImpl.SteamWorkshopUpdateFailEnum failEnum)
        {
            DialogManager.createToastDialog().setToastText(CommonData.getText(125));
        }

        public void UpdateProgress(EItemUpdateStatus status, ulong progressBytes, ulong totalBytes)
        {
         
        }

        public void UpdateSuccess()
        {
            DialogManager.createToastDialog().setToastText(CommonData.getText(124));
            this.customUI.ExitOnClick();
        }
    }

    /// <summary>
    /// 选择图片成功回调
    /// </summary>
    public class SelectCallBack : ResourcesManager.LoadCallBack<Sprite>
    {
        public MenuWorkshopCreateUIControl customUI;
        public SelectCallBack(MenuWorkshopCreateUIControl customUI)
        {
            this.customUI = customUI;
        }

        public void loadFail(string msg)
        {

        }

        public void loadSuccess(Sprite data)
        {
            if (data == null)
                return;
            int textureWidth = data.texture.width;
            int textureHigh = data.texture.height;
            customUI.tvHint.text = CommonData.getText(87) + ":" + textureWidth / 200 + " " + CommonData.getText(88) + ":" + textureHigh / 200;

            customUI.fileName = "workshop_pic_" + SystemUtil.getUUID();
            customUI.fileNamePath = customUI.fileSavePath + "/" + customUI.fileName;
            FileUtil.DeleteAllFile(customUI.fileSavePath);
            FileUtil.CreateDirectory(customUI.fileSavePath);
            FileUtil.CopyFile(customUI.uploadPath, customUI.fileNamePath, true);

            Texture thumb=  TextureUtil.ScaleTexture(data.texture,100,100);
            FileUtil.ImageSaveLocal(customUI.fileNamePath + "_Thumb", thumb);
        }
    }

    /// <summary>
    /// 清楚数据
    /// </summary>
    private void ClearData()
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
        if (inputDescription != null)
            inputDescription.text = null;
        if (tvHint != null)
            tvHint.text = null;
        if (fileNamePath != null)
            fileNamePath = null;
    }


    /// <summary>
    /// 检测提交数据
    /// </summary>
    /// <returns></returns>
    public bool CheckData()
    {
        if (uploadImage.sprite == null)
        {
            DialogManager.createToastDialog().setToastText(CommonData.getText(68));
            return false;
        }
        if (uploadPath == null && uploadPath.Length == 0)
        {
            DialogManager.createToastDialog().setToastText(CommonData.getText(69));
            return false;
        }
        if (inputName.text == null || inputName.text.Length == 0)
        {
            DialogManager.createToastDialog().setToastText(CommonData.getText(70));
            return false;
        }
        if (inputDescription.text == null || inputDescription.text.Length == 0)
        {
            DialogManager.createToastDialog().setToastText(CommonData.getText(126));
            return false;
        }
        if (inputHorizontalNumber.text == null || inputHorizontalNumber.text.Length == 0)
        {
            DialogManager.createToastDialog().setToastText(CommonData.getText(71));
            return false;
        }
        if (inputVerticalNumber.text == null || inputVerticalNumber.text.Length == 0)
        {
            DialogManager.createToastDialog().setToastText(CommonData.getText(72));
            return false;
        }
        if (!CheckUtil.CheckIsNumber(inputHorizontalNumber.text))
        {
            DialogManager.createToastDialog().setToastText(CommonData.getText(73));
            return false;
        }
        if (!CheckUtil.CheckIsNumber(inputVerticalNumber.text))
        {
            DialogManager.createToastDialog().setToastText(CommonData.getText(74));
            return false;
        }
        int horizontalNumber = Convert.ToInt32(inputHorizontalNumber.text);
        int verticalNumber = Convert.ToInt32(inputVerticalNumber.text);
        if (horizontalNumber > 50 || horizontalNumber < 2)
        {
            DialogManager.createToastDialog().setToastText(CommonData.getText(75));
            return false;
        }
        if (verticalNumber > 50 || verticalNumber < 2)
        {
            DialogManager.createToastDialog().setToastText(CommonData.getText(76));
            return false;
        }
        return true;
    }
}