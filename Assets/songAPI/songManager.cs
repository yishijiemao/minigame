using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class songManager : MonoBehaviour
{
    public static songManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {

            Destroy(gameObject);

        }
    }
    
    
    public GameObject lister;
    private void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            test();
        }
    }
    public void test()//���ڲ��ԣ����¼����ϵ�q����
    {
        AkSoundEngine.PostEvent("test", lister);
    }
    public void AKStart()//��Ϸ��ʼʱ����
    {
        Debug.Log("AK��ʼ");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKPlayRound()//��һغϿ�ʼʱ����
    {
        Debug.Log("AK��һغ�");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKPlayRoundEnd()//��һغϽ�������
    {
        Debug.Log("AK��һغϽ���");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKCreateBin()//��ҽ������ʱ����
    {
        Debug.Log("AK���");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKCreateJianzhu()//��ҽ��콨��ʱ����
    {
        Debug.Log("AK�콨��");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKBinLevelUp()//��������ʱ����
    {
        Debug.Log("AK������");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKchengLevelUp()//�Ǳ�����ʱ����
    {
        Debug.Log("AK�Ǳ�����");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKClickzhihui()//��ҵ��ָ�ӵ���
    {
        Debug.Log("AKָ��");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKClickjian()//��ҵ����ս������
    {
        Debug.Log("AK��ս");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKClickgong()//��ҵ����������
    {
        Debug.Log("AK��");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKClickdun()//��ҵ���ܱ�����
    {
        Debug.Log("AK��");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKClickma()//��ҵ���������
    {
        Debug.Log("AK��");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKClickJuntuan()//��ҵ�����ţ����ž��Ƕ��������ɵĵ�λ��
    {
        Debug.Log("AK����");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKClickOrder()//��ҵ������UI���ã����������Ǹ����ƶ����Ǹ�ui��
    {
        Debug.Log("AK����");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKClickInFirePoint()//��ҵ�����ڴ��̵ĵ�λ
    {
        Debug.Log("AK���̵�");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKClickNoFirecheng()//��ҵ��û��ս���ĳ�
    {
        Debug.Log("AK��ͨ��");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKClickzujianTeam()//����齨����
    {
        Debug.Log("AK�齨����");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKClickjiesanTeam()//��ҽ�ɢ����
    {
        Debug.Log("AK��ɢ����");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKClickfeige()//���ʹ�÷ɸ뼼�ܣ����˿��Էɸ봫�飩
    {
        Debug.Log("AK�ɸ�");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKFireFengHuo()//���������ƣ����Ѿ��������˻ᴥ���Ļ��ƣ�
    {
        Debug.Log("AK���");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKFireWin()//ս��ʤ��
    {
        Debug.Log("AKս��ʤ��");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKFireLose()//ս��ʧ��
    {
        Debug.Log("AKս��ʧ��");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKFireing()//ս����
    {
        Debug.Log("AKս����");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKEmenyBir()//�������ɳ���
    {
        Debug.Log("AK��������");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKHard()//��Ϸ�������ڴ���������������״̬�л���
    {
        Debug.Log("AK��Ϸ����");
        //AkSoundEngine.PostEvent("test", lister);
    }
}
