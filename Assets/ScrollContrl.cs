using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
public class ScrollContrl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    
   [SerializeField]
   private struct ItemInfo
    {
        public string name;
        public Sprite sprite;
        public string description;
        public ItemInfo(string name,Sprite sprite,string description)
        {
            this.name = name;
            this.sprite = sprite;
            this.description = description;
        }
    }

    public GameObject itemPrefab;
    public RectTransform itemParent;
    public Text descriptionText;
    [SerializeField]private ItemInfo[] itemInfos;
    public int displayNumber;
    public float itemSpace;
    public float moveSmooth;
    public float dragSpeed;
    public float scaleMultiplying;
    public float alphaMultiplying;

    public event Action<int> SelectAction;

    private ScrollItem[] items;
    private float displayWidth;
    private int offsetTimes;
    private bool isDrag;
    private int currentItemIndex;
    private float[] distances;
    private float selectItemX;
    private bool isSelectMove;
    private bool isSelected;
    private void Start()
    {
        Init();
        MoveItems(0);
    }
    private void Init()
    {
        displayWidth = (displayNumber - 1) * itemSpace;
        items = new ScrollItem[displayNumber];
        for(int i =0; i<displayNumber;i++)
        {
            ScrollItem item = Instantiate(itemPrefab, itemParent).GetComponent<ScrollItem>();
            item.Itemindex = i;
            items[i] = item;
        }
    }
    public void SetItemsInfo(string[] name,Sprite[] sprites,string[] descriptions) 
    {
        itemInfos = new ItemInfo[name.Length];
        for(int i =0;i<itemInfos.Length;i++)
        {
            itemInfos[i] = new ItemInfo(name[i], sprites[i], descriptions[i]);

        }
        SelectAction = null;
        isSelected = false;
    }

    public void Select(int itemIndex,int infoIndex,RectTransform itemRectTransform) 
    {
        Debug.Log(isSelected);
        if (!isSelected)
        {
            Debug.Log(isSelected+"2");
            SelectAction?.Invoke(infoIndex);
            isSelected = true;
            Debug.Log("select" + (infoIndex + 1).ToString());
        }
        else
        {
            isSelectMove = true;
            selectItemX = itemRectTransform.localPosition.x;
        }
        
    }

    public void MoveItems(int offsetTimes)
    {
        for(int i = 0;i<displayNumber;i++)
        {
            float x = itemSpace * (i - offsetTimes) - displayWidth / 2;
            items[i].rectTransform.localPosition = new Vector2(x, items[i].rectTransform.localPosition.y);
            
        }

        int middle;
        if(offsetTimes>0)
        {
            middle = itemInfos.Length - offsetTimes % itemInfos.Length;
        }
        else
        {
            middle = -offsetTimes % itemInfos.Length;
        }

        int infoindex = middle;
        for (int i =Mathf.FloorToInt(displayNumber/2f);i<displayNumber;i++)
        {
            if(infoindex>=itemInfos.Length)
            {
                infoindex = 0;
            }
            items[i].SetInfo(itemInfos[infoindex].sprite, itemInfos[infoindex].name, itemInfos[infoindex].description, infoindex, this);
            infoindex++;

        }
         infoindex = middle-1;
        for (int i = Mathf.FloorToInt(displayNumber / 2f)-1; i >=0; i--)
        {
            if (infoindex <=-1)
            {
                infoindex = itemInfos.Length-1;
            }
            items[i].SetInfo(itemInfos[infoindex].sprite, itemInfos[infoindex].name, itemInfos[infoindex].description, infoindex, this);
            infoindex--;

        }
    }
    private void ItemsControl()
    {
        distances = new float[displayNumber];
        for(int i= 0;i<displayNumber;i++)
        {
            float distance = Mathf.Abs(items[i].rectTransform.position.x - transform.position.x);
            distances[i] = distance;
            float scale = 1 - distance * scaleMultiplying;
            items[i].rectTransform.localScale = new Vector3(scale, scale, 1);           
            items[i].SetAlpha(1 - distance * alphaMultiplying);
        }
    }

    private void Adscorption()
    {
        float targetX;
        if(!isSelectMove)
        {
            float distance = itemParent.localPosition.x % itemSpace;
            int times = Mathf.FloorToInt(itemParent.localPosition.x / itemSpace);
            if(distance>0)
            {
                if(distance<itemSpace/2)
                {
                    targetX = times * itemSpace;
                }
                else
                {
                    targetX = (times + 1) * itemSpace;
                }
            }
            else
            {
                if (distance < -itemSpace / 2)
                {
                    targetX = times * itemSpace;
                }
                else
                {
                    targetX = (times + 1) * itemSpace;
                }
            }
        }
        else
        {
            targetX = -selectItemX;
        }
        itemParent.localPosition = new Vector2(Mathf.Lerp(itemParent.localPosition.x, targetX, moveSmooth / 10), itemParent.localPosition.y);
    }
    private void Update()
    {
        if(!isDrag)
        {
            Adscorption();
        }
        int currentOffsetTimes = Mathf.FloorToInt(itemParent.localPosition.x / itemSpace);
        if(currentOffsetTimes!=offsetTimes)
        {
            offsetTimes = currentOffsetTimes;
            MoveItems(offsetTimes);

        }

        ItemsControl();
    }

   

    public void OnPointerUp(PointerEventData eventData)
    {
        isDrag = true;
        isSelected = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        isSelected = false;
        itemParent.localPosition = new Vector2(itemParent.localPosition.x + eventData.delta.x * dragSpeed, itemParent.localPosition.y);
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDrag = false;
    }
}
