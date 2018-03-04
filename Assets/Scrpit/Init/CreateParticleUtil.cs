using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateParticleUtil : MonoBehaviour
{

    /// <summary>
    /// 创建背景粒子系统
    /// </summary>
    /// <param name="location"></param>
    /// <param name="particleW"></param>
    /// <param name="particleH"></param>
    /// <param name="particleEnum"></param>
    public static void createBackParticle(Vector3 location, float particleW, float particleH, BackParticleEnum particleEnum)
    {
        ParticleSystem particleSys = null;
        if (particleEnum.Equals(BackParticleEnum.Def))
        {
            particleSys = Instantiate(ResourcesManager.loadData<ParticleSystem>(ParticleResPath.Back_Def_Path));
        }

        //通用参数设置
        if (particleSys != null)
        {
            //设置位置
            Transform particleSysTF = particleSys.transform;
            particleSysTF.position = location;
            //设置大小
            ParticleSystem.ShapeModule shapeModule = particleSys.shape;
            ParticleSystem.MainModule mainModule = particleSys.main;
            if (particleW > particleH)
            {
                shapeModule.radius = particleW / 2f;
                mainModule.startSize = particleW / 10f;
            }
            else
            {
                shapeModule.radius = particleH / 2f;
                mainModule.startSize = particleH / 10f;
            }
        }

    }

    public static ParticleSystem createMergeParticle(Vector3 location, float particleW, float particleH, MergeParticleEnum particleEnum)
    {
        ParticleSystem particleSys = null;
        if (particleEnum.Equals(MergeParticleEnum.Def))
        {
            particleSys = Instantiate(ResourcesManager.loadData<ParticleSystem>(ParticleResPath.Merge_Def_Path));

        }


        //通用参数设置
        if (particleSys != null)
        {
            //设置位置
            Transform particleSysTF = particleSys.transform;
            particleSysTF.position = location;
            //设置大小
            ParticleSystem.ShapeModule shapeModule = particleSys.shape;
            ParticleSystem.MainModule mainModule = particleSys.main;
            if (particleW > particleH)
            {
                shapeModule.radius = particleW / 2f;
            }
            else
            {
                shapeModule.radius = particleH / 2f;
            }
        }

        return particleSys;
    }

}
