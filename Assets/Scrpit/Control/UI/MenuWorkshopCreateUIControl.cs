﻿using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;
using Steamworks;
using System.Collections.Generic;

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
    public Text uploadLoadingTitle;
    public Text uploadLoadingProgress;

    public GameObject tagsGroup;
    public GameObject tagModel;
    public Text tagTitle;

    public List<Toggle> listTag;

    public Image uploadImage;
    public string uploadPath;

    public string fileName;
    public string fileNamePath;
    public string fileSavePath;

    public Button loadingBT;
    public Button loadingCancelBT;

    private new void Awake()
    {
        base.Awake();
        fileSavePath = UnityEngine.Application.streamingAssetsPath + "/SteamWorkshopPicUpdate";

        InitTags();
        loadingCancelBT.onClick.AddListener(ExitOnClick);
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
        tagTitle.text = CommonData.getText(128);
        uploadLoadingTitle.text = CommonData.getText(129);
    }

    private void InitTags()
    {
        if (tagsGroup == null)
            return;
        List<string> tags = new List<string>();
        tags.Add("other");
        tags.Add("painting");
        tags.Add("anime");
        tags.Add("life");
        tags.Add("movie");
        tags.Add("people");
        tags.Add("animal");
        tags.Add("scenery");
        tags.Add("starrysky");
        tags.Add("food");
        tags.Add("game");
        tags.Add("celebrity");
        listTag = new List<Toggle>();
        for (int i = 0; i < tags.Count; i++)
        {
            GameObject tagObj = Instantiate(tagModel);
            tagObj.name = tags[i];
            tagObj.SetActive(true);
            tagObj.transform.parent = tagsGroup.transform;
            tagObj.transform.localScale = new Vector3(1f, 1f, 1f);
            Text tagText = CptUtil.getCptFormParentByName<Transform, Text>(tagObj.transform, "Label");
            tagText.text = tags[i];
            listTag.Add(tagObj.GetComponent<Toggle>());
        }
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
        PuzzlesInfoBean infoBean = new PuzzlesInfoBean();
        infoBean.mark_file_name = fileName;
        infoBean.horizontal_number = Convert.ToInt32(inputHorizontalNumber.text);
        infoBean.Vertical_number = Convert.ToInt32(inputVerticalNumber.text);
        string infoStr = JsonUtil.ToJson(infoBean);

        List<string> selectedTags = new List<string>();
        foreach (Toggle item in listTag)
        {
            if (item.isOn)
            {
                selectedTags.Add(item.gameObject.name);
            }
        }
        SteamWorkshopUpdateBean updateData = new SteamWorkshopUpdateBean();
        updateData.title = inputName.text;
        updateData.description = inputDescription.text;
        updateData.metadata = infoStr;
        updateData.content = fileSavePath;
        updateData.preview = fileNamePath + "_Thumb";
        updateData.tags = selectedTags;
        loadingBT.gameObject.SetActive(true);
        SteamWorkshopHandle.CreateWorkshopItem(this, updateData, new UpdateCallBack(this));
    }

    /// <summary>
    /// 退出按钮
    /// </summary>
    private void ExitOnClick()
    {
        loadingBT.gameObject.SetActive(false);
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
            customUI.loadingBT.gameObject.SetActive(false);
            ToastDialog dialog = DialogManager.createToastDialog();
            dialog.setToastTime(5);
            dialog.setToastText(CommonData.getText(125));
        }

        public void UpdateProgress(EItemUpdateStatus status, ulong progressBytes, ulong totalBytes)
        {
            string contentStr = "";
            if (status == EItemUpdateStatus.k_EItemUpdateStatusUploadingPreviewFile)
            {
                contentStr = "Uploading Preview File";
            }
            else if (status == EItemUpdateStatus.k_EItemUpdateStatusUploadingContent)
            {
                contentStr = "Uploading Content:  " + progressBytes + "/" + totalBytes;
            }
            else if (status == EItemUpdateStatus.k_EItemUpdateStatusPreparingConfig)
            {
                contentStr = "Preparing Config";
            }
            else if (status == EItemUpdateStatus.k_EItemUpdateStatusPreparingContent)
            {
                contentStr = "Preparing Content";
            }
            else if (status == EItemUpdateStatus.k_EItemUpdateStatusCommittingChanges)
            {
                contentStr = "Committing Changes";
            }
            customUI.uploadLoadingProgress.text = contentStr;
        }

        public void UpdateSuccess()
        {
            if (customUI.loadingBT.IsActive())
            {
                customUI.loadingBT.gameObject.SetActive(false);
                DialogManager.createToastDialog().setToastText(CommonData.getText(124));
                this.customUI.ExitOnClick();
            }
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

            if (textureWidth <= 1000 && textureHigh <= 1000)
            {
                FileUtil.ImageSaveLocal(customUI.fileNamePath + "_Thumb", data.texture);
            }
            else
            {

                int thumbWidth = 0;
                int thumbHigh = 0;

                if (textureWidth > textureHigh)
                {
                    thumbWidth = 1000;
                    thumbHigh = (int)(((float)thumbWidth) / (((float)textureWidth / (float)textureHigh)));
                }
                else
                {
                    thumbHigh = 1000;
                    thumbWidth = (int)(((float)thumbHigh) / (((float)textureHigh / (float)textureWidth)));
                }

                if (thumbWidth <= 0)
                {
                    thumbWidth = 1;
                }
                if (thumbHigh <= 0)
                {
                    thumbHigh = 1;
                }
                Texture thumb = TextureUtil.ScaleTexture(data.texture, thumbWidth, thumbHigh);
                FileUtil.ImageSaveLocal(customUI.fileNamePath + "_Thumb", thumb);
            }
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
        if (fileName != null)
            fileName = null;
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
        bool isToggleSelected = false;
        if (listTag != null)
            foreach (Toggle item in listTag)
            {
                if (item.isOn)
                    isToggleSelected = true;
            }
        if (!isToggleSelected)
        {
            DialogManager.createToastDialog().setToastText(CommonData.getText(127));
            return false;
        }
        return true;
    }
}