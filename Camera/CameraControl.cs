/************************************************************************/
/*    ��������� UpdateFollowSmooth�����������Smooth                   */
/************************************************************************/

using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour 
{
    //����������Ŀ��
    public float m_targetAim = 20;
    //�������ģ�ͱ��ֵķ���
    public Vector3 m_direction = Vector3.back;
    //���߻���ģ�ͱ��ֵľ���
    public float m_distance = 10.0f;
    //�߶�����
    public float m_directionDamping = 2.0f;
    //��ת����
    public float m_distanceDamping = 3.0f;
    //�������
    public GameObject m_targetFollow;

    private Vector3 m_prePositionTargetFollow;
    private bool m_smoothFlag;

    void Awake()
    {
        if (m_targetFollow != null)
            SetTarget(m_targetFollow);
    }
    public void SetTarget(GameObject target)
    {
        m_targetFollow = target;
        if(m_targetFollow == null)
            return;
        
        m_prePositionTargetFollow = m_targetFollow.transform.position;
        UpdateFollowSmooth(100f);
    }

    void LateUpdate()
    {
        if(m_targetFollow != null)
        {
            if((m_targetFollow.transform.position - m_prePositionTargetFollow).sqrMagnitude >= 0.001f)
            {
                m_smoothFlag = true;
                UpdateFollowSmooth(Time.deltaTime);
                m_prePositionTargetFollow = m_targetFollow.transform.position;
            }
            
            else if(m_smoothFlag)
            {
                UpdateFollowSmooth(Time.deltaTime);
            }
        }
    }
    void UpdateFollowSmooth(float deltaTime)
    {
        Vector3 currentDirection = transform.position - m_targetFollow.transform.position;        
        float currentdistance = currentDirection.magnitude;
        currentDirection = Vector3.RotateTowards(currentDirection,m_direction,m_directionDamping * deltaTime,0.0f);
        currentDirection.Normalize();
        float distance = m_direction.magnitude;

        //if(currentdistance > distance)
        //    currentdistance = Mathf.Clamp(currentdistance - m_distanceDamping * deltaTime,distance,currentdistance);
        //else
        //    currentdistance = Mathf.Clamp(currentdistance + m_distanceDamping * deltaTime,currentdistance,distance);

        currentdistance = Mathf.Lerp(currentdistance,m_direction.magnitude,m_distanceDamping * deltaTime);

        Vector3 direction = currentDirection * currentdistance;
        if ((direction - m_direction).sqrMagnitude < 0.01f)
            m_smoothFlag = false;
        Vector3 position = m_targetFollow.transform.TransformPoint(direction);

        transform.position = position;

        transform.LookAt(m_targetFollow.transform.position + m_targetFollow.transform.forward * m_targetAim);
    }
}
