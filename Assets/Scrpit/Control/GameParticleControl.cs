using System.Collections;
using UnityEngine;
using DG.Tweening;

public class GameParticleControl : BaseMonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    public void playParticle(Transform mergeObj)
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
            if (itemParticle == null)
            {
                ParticleSystem particleSystem = CreateParticleUtil.createMergeParticle(itemTF.position, 3f, 3f, MergeParticleEnum.Def);
                particleSystem.transform.parent = itemTF;
                particleSystem.Play();
            }
            else
            {
                itemParticle.Play();
            }
        }
    }


}