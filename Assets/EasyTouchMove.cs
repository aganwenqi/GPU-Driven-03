using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;

/// <summary>
/// ����ҡ��(�������ڿ��Ʋ�)
/// </summary>
public class EasyTouchMove : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public List<Vector2> caluatePostionQueue;
    /// <summary>
    /// ҡ�����뾶
    /// ������Ϊ��λ
    /// </summary>
    public float JoyStickRadius = 50;

    /// <summary>
    /// ҡ����������
    /// </summary>
    public float JoyStickResetSpeed = 5.0f;

    /// <summary>
    /// ��ǰ�����Transform���
    /// </summary>
    private RectTransform selfTransform;

    /// <summary>
    /// �Ƿ���������ҡ��
    /// </summary>
    private bool isTouched = false;

    /// <summary>
    /// ����ҡ�˵�Ĭ��λ��
    /// </summary>
    private Vector2 originPosition;

    /// <summary>
    /// ����ҡ�˵��ƶ�����
    /// </summary>
    private Vector2 touchedAxis;
    public Vector2 TouchedAxis
    {
        get
        {
            return touchedAxis.normalized;
        }
    }

    /// <summary>
    /// ���崥����ʼ�¼�ί�� 
    /// </summary>
    public delegate void JoyStickTouchBegin(Vector2 vec);

    /// <summary>
    /// ���崥�������¼�ί�� 
    /// </summary>
    /// <param name="vec">����ҡ�˵��ƶ�����</param>
    public delegate void JoyStickTouchMove(Vector2 vec);

    /// <summary>
    /// ���崥�������¼�ί��
    /// </summary>
    public delegate void JoyStickTouchEnd();

    /// <summary>
    /// ע�ᴥ����ʼ�¼�
    /// </summary>
    public event JoyStickTouchBegin OnJoyStickTouchBegin;

    /// <summary>
    /// ע�ᴥ�������¼�
    /// </summary>
    public event JoyStickTouchMove OnJoyStickTouchMove;

    /// <summary>
    /// ע�ᴥ�������¼�
    /// </summary>
    public event JoyStickTouchEnd OnJoyStickTouchEnd;


    void Start()
    {
        //��ʼ������ҡ�˵�Ĭ�Ϸ���
        selfTransform = this.GetComponent<RectTransform>();
        originPosition = selfTransform.anchoredPosition;
    }
    public bool IsTouched()
    {
        return isTouched;
    }
    //��ʼ����ҡ��
    public void OnPointerDown(PointerEventData eventData)
    {
        //MainPanelContrller.Instance.SetAutoMove(false);
        isTouched = true;
        touchedAxis = GetJoyStickAxis(eventData);
        if (this.OnJoyStickTouchBegin != null)
            this.OnJoyStickTouchBegin(TouchedAxis);
    }

    //�ɿ�ҡ��
    public void OnPointerUp(PointerEventData eventData)
    {
        isTouched = false;
        selfTransform.anchoredPosition = originPosition;
        touchedAxis = Vector2.zero;
        if (this.OnJoyStickTouchEnd != null)
            this.OnJoyStickTouchEnd();

    }
    //�϶�ҡ��
    public void OnDrag(PointerEventData eventData)
    {
        touchedAxis = GetJoyStickAxis(eventData);
        if (this.OnJoyStickTouchMove != null)
            this.OnJoyStickTouchMove(TouchedAxis);
    }


    void Update()
    {

        //�ɿ�����ҡ�˺�������ҡ�˻ص�Ĭ��λ��
        if (selfTransform.anchoredPosition.magnitude > originPosition.magnitude)
            selfTransform.anchoredPosition -= TouchedAxis * Time.deltaTime * JoyStickResetSpeed;

    }

    /// <summary>
    /// ��������ҡ�˵�ƫ����
    /// </summary>
    /// <returns>The joy stick axis.</returns>
    /// <param name="eventData">Event data.</param>
    private Vector2 GetJoyStickAxis(PointerEventData eventData)
    {
        //��ȡ��ָλ�õ���������
        Vector3 worldPosition;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(selfTransform,
                 eventData.position, eventData.pressEventCamera, out worldPosition))
            selfTransform.position = worldPosition;
        //��ȡҡ�˵�ƫ����
        Vector2 touchAxis = selfTransform.anchoredPosition - originPosition;
        //ҡ��ƫ��������
        if (touchAxis.magnitude >= JoyStickRadius)
        {
            touchAxis = touchAxis.normalized * JoyStickRadius;
            selfTransform.anchoredPosition = touchAxis;
        }
        return touchAxis;
    }

}
