using System.Collections;
using UnityEngine;
using DG.Tweening;

public class GameParticleControl : BaseMonoBehaviour
{

    public ParticleSystem particleSystem;

    void Start()
    {
        particleSystem= CreateParticleUtil.createMergeParticle(new Vector3(0, 0, 0), 3f, 3f, MergeParticleEnum.Def);
       
    }

    void Update()
    {

    }

    public void playParticle(Transform mergeObj)
    {
        if (particleSystem == null|| mergeObj==null)
            return;
        MeshRenderer[] mergeObjMeshs = mergeObj.GetComponentsInChildren<MeshRenderer>();
        if (mergeObjMeshs == null)
            return;
        int meshSize = mergeObjMeshs.Length;

        for (int i = 0; i < meshSize; i++)
        {
            MeshRenderer itemMesh = mergeObjMeshs[i];
            StartCoroutine(ShowA(itemMesh,i));
        }
    }

    private IEnumerator ShowA(MeshRenderer itemMesh,int position)
    {
        yield return new WaitForSeconds(0.1f* position);
        ParticleSystem.ShapeModule shapeModule = particleSystem.shape;
        shapeModule.meshRenderer = itemMesh;
        particleSystem.Play();
    }

}