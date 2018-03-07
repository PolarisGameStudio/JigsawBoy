using System.Collections;
using UnityEngine;
using DG.Tweening;

public class GameParticleControl : BaseMonoBehaviour
{
    //合并粒子特效样式
    public MergeParticleEnum mergeParticleEnum;
    //移动粒子特效样式
    public MoveParticleEnum moveParticleEnum;


    private void Awake()
    {
        mergeParticleEnum = MergeParticleEnum.Def;
        moveParticleEnum = MoveParticleEnum.Def;
    }


    /// <summary>
    /// 播放合并动画
    /// </summary>
    /// <param name="mergeObj"></param>
    public void playMergeParticle(Transform mergeObj)
    {
        if (mergeObj == null)
            return;
        Transform[] mergeObjChilds = mergeObj.GetComponentsInChildren<Transform>();
        if (mergeObjChilds == null || mergeObjChilds.Length == 0)
            return;
        int meshSize = mergeObjChilds.Length;
        for (int i = 0; i < meshSize; i++)
        {
            Transform itemTF = mergeObjChilds[i];
            ParticleSystem itemParticle = itemTF.GetComponentInChildren<ParticleSystem>();
            if (itemParticle == null || !itemParticle.name.Contains("ParticleMerge"))
            {
                itemParticle = CreateParticleUtil.createMergeParticle(itemTF, mergeParticleEnum);
            }
           itemParticle.Play();
        }
    }


}