using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Remenber : MonoBehaviour
{
    public Button[] days;
    public Button[] skipdays;
    public Text StartDate,EndDate;



    public int Startselecteddate;
    public int Endselecteddate;
    public int buttontapcount;
    public int DaysLeft;
    [SerializeField]
    private int Startdate, Enddate;
    private void Start()
    {
        buttontapcount = PlayerPrefs.GetInt("TapCount", 0);
        Startdate = PlayerPrefs.GetInt("StartDate", 0);
        Enddate = PlayerPrefs.GetInt("EndDate", 0);
        Startselecteddate = Startdate;
        Endselecteddate = Enddate;
        StartDate.text ="StartDate: "+ PlayerPrefs.GetInt("StartDate", 0).ToString();
        EndDate.text =  "EndDate: " + PlayerPrefs.GetInt("EndDate", 0).ToString();

        for(int i = 0; i < days.Length; i++)
        {
            if (Startdate - 1 == i)
            {
                days[i].GetComponent<Image>().color = Color.red;
            }
            if (Enddate - 1 == i)
            {
                days[i].GetComponent<Image>().color = Color.red;
            }
        }
        foreach(Button b in skipdays)
        {
                string txt = b.transform.GetChild(0).GetComponent<Text>().text;
                int num = int.Parse(txt);
            if (num== PlayerPrefs.GetInt($"skip{num}", 0))
            {
                b.GetComponent<Image>().color = Color.green;
            }
        }
    }
    public  void clickedbutton(Button b)
    {
        if (buttontapcount == 0)
        {
            b.GetComponent<Image>().color = Color.red;
            string startselecteddate = b.transform.GetChild(0).GetComponent<Text>().text;
            Startselecteddate = int.Parse(startselecteddate);
            buttontapcount++;
        }else if (buttontapcount == 1)
        {
            b.GetComponent<Image>().color = Color.red;
            string endselecteddate = b.transform.GetChild(0).GetComponent<Text>().text;
            Endselecteddate = int.Parse(endselecteddate);
            buttontapcount++;
            DaysLeft = Mathf.Abs((Startselecteddate - 1) - Endselecteddate);
        }
    }
    public void SkipDay(Button b)
    {
        string text = b.transform.GetChild(0).GetComponent<Text>().text;
        int num = int.Parse(text);
        if (PlayerPrefs.GetInt($"skip{num}", 0) != num)
        {
            PlayerPrefs.SetInt($"skip{num}", num);
        }
        b.GetComponent<Image>().color = Color.green;
        Enddate = PlayerPrefs.GetInt("EndDate", 0);
        Enddate++;
        PlayerPrefs.SetInt("EndDate", Enddate);
        
        CheckDays();
    }
    public void CheckDays()
    {
      int enddate = PlayerPrefs.GetInt("EndDate", 0);
      int startdate= PlayerPrefs.GetInt("StartDate", 0);
      
     
      for (int i = 0; i <skipdays.Length; i++)
      {
          if (i >= startdate - 1 && i <= enddate - 1)
          {
                Debug.Log("working");
              skipdays[i].gameObject.SetActive(true);
          }
          else
          {
                Debug.Log("working1");
                skipdays[i].gameObject.SetActive(false);
          }
      }
    }
    public void Erase()
    {
        Startselecteddate = 0;
        buttontapcount = 0;
        Endselecteddate = 0;
        PlayerPrefs.DeleteAll();
        StartDate.text = "StartDate";
        EndDate.text = "EndDate: ";
        DaysLeft = 0;
        foreach(Button b in days)
        {
            b.GetComponent<Image>().color = Color.white;
        }
        foreach(Button b in skipdays)
        {
            b.GetComponent<Image>().color = Color.white;
        }
    }
    public void LockDate()
    {
        PlayerPrefs.SetInt("StartDate", Startselecteddate);
        StartDate.text = "StartDate: " + PlayerPrefs.GetInt("StartDate", 0).ToString();
        PlayerPrefs.SetInt("EndDate", Endselecteddate);
        EndDate.text = "EndDate: " + PlayerPrefs.GetInt("EndDate", 0).ToString();
        PlayerPrefs.SetInt("TapCount", buttontapcount);
    }
}
