using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.UI;
using Steamworks;

public class SteamWorkshopSelect : BaseMonoBehaviour
{
    public GameObject installModel;

    public void CreateInstallItemList(List<SteamWorkshopQueryInstallInfoBean> listData)
    {
        //删除原数据
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
                Destroy(transform.GetChild(i).gameObject);
        }
        foreach (SteamWorkshopQueryInstallInfoBean itemData in listData)
        {
            CreateInstallItem(itemData);
        }
    }

    private void CreateInstallItem(SteamWorkshopQueryInstallInfoBean itemData)
    {
        GameObject itemObj = Instantiate(installModel);
        itemObj.transform.parent = this.transform;
        itemObj.transform.localScale = new Vector3(1f, 1f, 1f);
        itemObj.SetActive(true);

        Image installThumb = CptUtil.getCptFormParentByName<Transform, Image>(itemObj.transform, "InstallThumb");
        UGCHandle_t handle_T= itemData.detailsInfo.m_hPreviewFile;
        string installThumbPath = itemData.previewUrl;
        StartCoroutine(ResourcesManager.LoadAsyncHttpImage(installThumbPath, installThumb));
    }
}