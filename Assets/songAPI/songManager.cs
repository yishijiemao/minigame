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
    public void test()//用于测试，按下键盘上的q触发
    {
        AkSoundEngine.PostEvent("test", lister);
    }
    public void AKStart()//游戏开始时调用
    {
        Debug.Log("AK开始");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKPlayRound()//玩家回合开始时调用
    {
        Debug.Log("AK玩家回合");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKPlayRoundEnd()//玩家回合结束调用
    {
        Debug.Log("AK玩家回合结束");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKCreateBin()//玩家建造兵力时调用
    {
        Debug.Log("AK造兵");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKCreateJianzhu()//玩家建造建筑时调用
    {
        Debug.Log("AK造建筑");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKBinLevelUp()//兵力升级时调用
    {
        Debug.Log("AK兵升级");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKchengLevelUp()//城堡升级时调用
    {
        Debug.Log("AK城堡升级");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKClickzhihui()//玩家点击指挥调用
    {
        Debug.Log("AK指挥");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKClickjian()//玩家点击近战兵调用
    {
        Debug.Log("AK近战");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKClickgong()//玩家点击弓兵调用
    {
        Debug.Log("AK弓");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKClickdun()//玩家点击盾兵调用
    {
        Debug.Log("AK盾");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKClickma()//玩家点击马兵调用
    {
        Debug.Log("AK马");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKClickJuntuan()//玩家点击军团（军团就是多个兵种组成的单位）
    {
        Debug.Log("AK军团");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKClickOrder()//玩家点击命令UI调用（点击后就是那个兵移动的那个ui）
    {
        Debug.Log("AK命令");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKClickInFirePoint()//玩家点击正在打仗的点位
    {
        Debug.Log("AK打仗点");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKClickNoFirecheng()//玩家点击没有战争的城
    {
        Debug.Log("AK普通城");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKClickzujianTeam()//玩家组建军团
    {
        Debug.Log("AK组建军团");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKClickjiesanTeam()//玩家解散军团
    {
        Debug.Log("AK解散军团");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKClickfeige()//玩家使用飞鸽技能（用了可以飞鸽传书）
    {
        Debug.Log("AK飞鸽");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKFireFengHuo()//触发烽火机制（当友军碰到敌人会触发的机制）
    {
        Debug.Log("AK烽火");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKFireWin()//战斗胜利
    {
        Debug.Log("AK战斗胜利");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKFireLose()//战斗失败
    {
        Debug.Log("AK战斗失败");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKFireing()//战斗中
    {
        Debug.Log("AK战斗中");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKEmenyBir()//敌人生成出来
    {
        Debug.Log("AK敌人生成");
        //AkSoundEngine.PostEvent("test", lister);
    }
    public void AKHard()//游戏到达中期触发（可用于音乐状态切换）
    {
        Debug.Log("AK游戏中期");
        //AkSoundEngine.PostEvent("test", lister);
    }
}
