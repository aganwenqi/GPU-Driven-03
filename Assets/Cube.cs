using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class Cube : MonoBehaviour
{

    private float time;

    public EasyTouchMove stick;
    public EasyTouchMove stickRotation;

    public float speed = 5f;

    //��ת���Ƕ�
    public int yRotationMinLimit = -20;
    public int yRotationMaxLimit = 80;
    //��ת�ٶ�
    public float xRotationSpeed = 250.0f;
    public float yRotationSpeed = 120.0f;
    //��ת�Ƕ�
    private float xRotation = 0.0f;
    private float yRotation = 0.0f;

    // Use this for initialization
    void Start()
    {

    }

    float ClampValue(float value, float min, float max)//������ת�ĽǶ�
    {
        if (value < -360)
            value += 360;
        if (value > 360)
            value -= 360;
        return Mathf.Clamp(value, min, max);//����value��ֵ��min��max֮�䣬 ���valueС��min������min�� ���value����max������max�����򷵻�value
    }
           // Update is called once per frame
    void Update()
    {

        Vector2 stickValue;
        stickValue = stickRotation.TouchedAxis;
        //Input.GetAxis("MouseX")��ȡ����ƶ���X��ľ���
        xRotation -= stickValue.x * xRotationSpeed * 0.02f;
        yRotation += stickValue.y * yRotationSpeed * 0.02f;

        yRotation = ClampValue(yRotation, yRotationMinLimit, yRotationMaxLimit);//��������ڽ�β
                                                                                //ŷ����ת��Ϊ��Ԫ��
        Quaternion rotation = Quaternion.Euler(-yRotation, -xRotation, 0);
        if (stickValue.x != 0 || stickValue.y != 0)
        {
            transform.rotation = rotation;
        }
        
        //if (autoMove)
        //    stickValue = autoMoveDir;
        //else
        stickValue = stick.TouchedAxis;

        if (stickValue.x == 0 && stickValue.y == 0)
        {
            time = 0;
            // anim.SetBool("run", false);
            return;
        }

        transform.position += stickValue.y * transform.forward * speed * 0.01f;
        transform.position += stickValue.x * transform.right * speed * 0.01f;

       

    }
}
