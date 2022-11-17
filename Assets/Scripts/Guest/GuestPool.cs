using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestPool : Singleton<GuestPool>
{
    public List<GameObject> guestPrefab = new List<GameObject>();
    public List<GameObject> guestList = new List<GameObject>();
    private Vector3 pos = new Vector3(0,0,0);
    public int guestValue = 0;
    public int guestIndex = 0;
    public int chair = 0;
    public bool isCoolTime = true;
    public IEnumerator guestCool;

    private void Start()
    {
        for(int i = 0; i < 3; i++)
        {
            for(int j =0; j < guestPrefab.Count; j++)
            {
                GameObject guest = Instantiate(guestPrefab[j]);
                guestList.Add(guest);
                guest.SetActive(false);
                guest.transform.parent = transform;
            }
        }
    }

    private void Update()
    {
        if((UIManager.Instance.starValue * 15) <= guestValue)
        {
            StopCoroutine(guestCool);
            guestIndex = 0;
            guestValue = 0;
        }

        if (guestIndex >= ChairManager.Instance.seatPos.Count)
        {
            StopCoroutine(guestCool);
        }
    }

    public IEnumerator GuestCO()
    {
        if(isCoolTime)
        {
            for (int i = 0; i < (UIManager.Instance.starValue * 15); i++)
            {
                isCoolTime = false;
                guestList[0].transform.localPosition = pos;
                guestList[0].SetActive(true);
                guestList[0].transform.parent = null;
                guestList.Remove(guestList[0]);
                guestValue++;
                guestIndex++;
                yield return new WaitForSeconds(8f);
                if (guestList.Count <= 1)
                {
                    Refill();
                }
            }
        }
    }

    public void StartCO()
    {
        StartCoroutine(guestCool);
    }

    public void OpenClick()
    {
        guestCool = GuestCO();
        StartCoroutine(guestCool);
    }
    public void CloseClick()
    {
        StopCoroutine(guestCool);
        guestIndex = 0;
        guestValue = 0;
    }

    private void Refill()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < guestPrefab.Count; j++)
            {
                GameObject guest = Instantiate(guestPrefab[j]);
                guestList.Add(guest);
                guest.SetActive(false);
                guest.transform.parent = transform;
            }
        }
    }
}
