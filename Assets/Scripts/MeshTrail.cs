using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTrail : MonoBehaviour
{
    public float activeTime = 2.0f;
    public MovementInput moveScript;
    public float speedBoost = 6;
    public Animator animator;
    public float animSpeedBoost = 1.5f;

    [Header("Mesh Releted")]
    public float meshRefreshRate = 1.0f; 
    public float meshDestoryDelay = 3.0f;
    public Transform positionToSpawn;

    [Header("Shader Releted")]
    public Material mat;
    public string shaderVerRef;
    public float shaderVarRate = 0.1f;
    public float shaderVarRefreshRate = 0.05f;

    private SkinnedMeshRender[] skinnedRenderer;
    private bool jsTrailActive;

    private float normalSpeed;
    private float normalAnimSpeed;

    //������ ������ ������ �����ϴ� �ڷ�ƾ
    ���� 0��
    IEnumerator AnimateMaterialFloat(Material m, float valueGoal, float rate, float refreshRate)
    {
        float valueToAnimate = m.GetFloat(shaderVerRef);

        //��ǥ ���� ���� �� �� ���� �ݺ�
        while (valueToAnimate > valueGoal)
        {
            valueToAnimate = rate;
            m.SetFloat(shader Ver Ref, valueToAnimate);
            yield return new WaitForSeconds(refreshRate);
        }
    }
    IEnumerator Activate Trail(float timeActivated)
    {
        //���� ���� ������ ����
        normalSpeed = moveScript.movementSpeed;
        moveScript.movementSpeed = speedBoost;

        normalAnimSpeed = animator.GetFloat("animSpeed");
        animator.SetFloat("animSpeed", animSpeedBoost);

        while (timeActivated > 0)
        {
            timeActivated -= meshRefreshRate;

            if (skinnedRenderer == null)
                skinnedRenderer = positionToSpawn.GetComponentsInChildren<SkinnedMeshRenderer>();

            for (int i = 0; i < skinnedRenderer.Length; i++)
            {
                GameObject gobj = new GameObject();
                gobj.transform.SetPositionAndRotation(positionToSpawn.position, positionToSpawn.rotation);

                MeshRenderer mr = gObj.AddComponent<MeshRenderer>();
                MeshFilter mr = gObj.AddComponent<MeshFilter>();

                Mesh m = new Mesh();
                skinnedRenderer[i].BakeMesh(m);
                mf.mesh = m;
                mr.material = mat;

                StartCoroutine(AnimateMaterialFloat(mr.material, 0, shaderVarRate, shaderVarRefreshRate));

                Destroy(gObj, meshDestoryDelay);
            }
            yield return new WaitForSeconds(meshRefreshRate);
        }

        moveScript.movement Speed normal Speed;
        animator SetFloat("animSpeed", normal AnimSpeed);
        isTrailActive = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isTrailActive) //�����̽��ٸ� ������ ���� �ܻ� ȿ���� ��Ȱ��ȭ�� ��
        {
            isTrailActive = true;
            StartCoroutine(ActivateTrail(activeTime));
        }
    }
}











