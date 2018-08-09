using System.IO;
using UnityEditor;
using UnityEngine;

public class Builder : Editor
{

    [MenuItem("Custom/BuildAssetBundle")]
    public static void BuildAssetBundle()
    {

        //根据BuildSetting里面所激活的平台进行打包 设置过AssetBundleName的都会进行打包
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles", BuildAssetBundleOptions.UncompressedAssetBundle, EditorUserBuildSettings.activeBuildTarget);
        AssetDatabase.Refresh();
        Debug.Log("打包完成");

    }

    [MenuItem("Custom/ReNamePuzzlesAsset")]
    public static void ReAssetName()
    {
        Object[] objs = Selection.objects;
        for (int i = 0; i < objs.Length; i++)
        {
            string path = AssetDatabase.GetAssetPath(objs[i]);
            FileInfo dir = new FileInfo(path);
            string parent = dir.Directory.Name;
            AssetImporter.GetAtPath(path).assetBundleName = "puzzlespic/"+ parent + "/" + objs[i].name;
            if (i % 10 == 0)
            {
                bool isCancel = EditorUtility.DisplayCancelableProgressBar("修改中", path, (float)i / objs.Length);
                if (isCancel)
                {
                    EditorUtility.ClearProgressBar();
                    break;
                }
            }
        }
        EditorUtility.ClearProgressBar();
    }
}
