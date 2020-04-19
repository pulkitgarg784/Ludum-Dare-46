 using UnityEngine;
 using UnityEngine.UI;
 public class TimeManager : MonoBehaviour
 {
 public float timeScale;
 public int timeMode;
 public int startHour, startDay, startMonth, startYear;
 public string monthName;    
 public bool am, noonAm, leapYear;
 private Text calendarText;
 public double minute, hour, day, second, month;
 public int year;
 void Start()
 {
     startHour = 01;
     startDay = 29;
     startMonth = 12;
     startYear = 2020;
     year = startYear;
     month = startMonth;
     day = startDay;
     hour = startHour;
     year = startYear;

     am = true;
     noonAm = false; //true = noon is AM; option for 12 am/pm confusion
     calendarText = GameObject.Find("Calendar").GetComponent<Text>(); //create text box "Calendar" in Unity
     DetermineMonth();
 }
 void Update()
 {
     CalculateTime();
 }
 //for UI
 void TextCallFunction()
 {
     // minutes included
     if (timeMode == 1)
     {
         if (am == true)
         {
             if (hour <= 9) //single digit hours get a 0
             {
                 if (minute <= 9)
                 {
                     calendarText.text = monthName + " " + day + " " + year + " " + "0" + hour + ":0" + minute + " AM";
                 }
                 else
                 {
                     calendarText.text = monthName + " " + day + " " + year + " " + "0" + hour + ":" + minute + " AM";
                 }
             }
             else if (minute <= 9)
             {
                 calendarText.text = monthName + " " + day + " " + year + " " + hour + ":0" + minute + " AM";
                 if (noonAm == false && hour == 12)
                 {
                     calendarText.text = monthName + " " + day + " " + year + " " + hour + ":0" + minute + " PM";
                 }
             }
             else
             {
                 calendarText.text = monthName + " " + day + " " + year + " " + hour + ":" + minute + " AM";
                 if (noonAm == false && hour == 12)
                 {
                     calendarText.text = monthName + " " + day + " " + year + " " + hour + ":" + minute + " PM";
                 }
             }
         }
         else if (am == false)
         {
             if (hour <= 9)
             {
                 if (minute <= 9)
                 {
                     calendarText.text = monthName + " " + day + " " + year + " " + "0" + hour + ":0" + minute + " PM";
                 }
                 else
                 {
                     calendarText.text = monthName + " " + day + " " + year + " " + "0" + hour + ":" + minute + " PM";
                 }
             }
             else
             {
                 if (minute <= 9)
                 {
                     calendarText.text = monthName + " " + day + " " + year + " " + hour + ":0" + minute + " PM";
                     if (noonAm == false && hour == 12)
                     {
                         calendarText.text = monthName + " " + day + " " + year + " " + hour + ":0" + minute + " AM";
                     }
                 }
                 else
                 {
                     calendarText.text = monthName + " " + day + " " + year + " " + hour + ":" + minute + " PM";
                     if (noonAm == false && hour == 12)
                     {
                         calendarText.text = monthName + " " + day + " " + year + " " + hour + ":" + minute + " AM";
                     }
                 }
             }
         }
     }
     //minutes excluded
     else if (timeMode == 2)
     {
         if (am == true)
         {
             calendarText.text = monthName + " " + day + " " + year + " " + hour + " AM";
             if (noonAm == false && hour == 12)
             {
                 calendarText.text = monthName + " " + day + " " + year + " " + hour + " PM";
             }
         }
         else if (am == false)
         {
             calendarText.text = monthName + " " + day + " " + year + " " + hour + " PM";
             if (noonAm == false && hour == 12)
             {
                 calendarText.text = monthName + " " + day + " " + year + " " + hour + " AM";
             }
         }
     }
     
     //hours excluded
     else if (timeMode == 3)
     {
         calendarText.text = monthName + " " + day + " " + year; 
     }
 }
 
 //determining month names
 void DetermineMonth()
 {
     if (month == 1)
     {
         monthName = "Jan";
     }
     if (month == 2)
     {
         monthName = "Feb";
     }
     if (month == 3)
     {
         monthName = "Mar";
     }
     if (month == 4)
     {
         monthName = "Apr";
     }
     if (month == 5)
     {
         monthName = "May";
     }
     if (month == 6)
     {
         monthName = "Jun";
     }
     if (month == 7)
     {
         monthName = "Jul";
     }
     if (month == 8)
     {
         monthName = "Aug";
     }
     if (month == 9)
     {
         monthName = "Sep";
     }
     if (month == 10)
     {
         monthName = "Oct";
     }
     if (month == 11)
     {
         monthName = "Nov";
     }
     if (month == 12)
     {
         monthName = "Dec";
     }
     TextCallFunction();
 }
 //determining total days in a month and leap years
 void CalculateMonthLength()
 {
     if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
     {
         if (day >= 32)
         {
             month++;
             GameManager.isMonthEnd = true;
             day = 1;
             DetermineMonth();
         }
     }
     if (month == 2)
     {
         if (day >= 29)
         {
             //leap year
             if (year % 4 == 0 && year % 100 != 0)
             {
                 TextCallFunction();
                 DetermineMonth();
                 leapYear = true;
             }
             if (leapYear == false)
             {
                 month++;
                 GameManager.isMonthEnd = true;

                 day = 1;
                 DetermineMonth();
             }
             else if (day == 30)
             {
                 month++;
                 GameManager.isMonthEnd = true;

                 day = 1;
                 DetermineMonth();
             }
         }
         if (month == 4 || month == 6 || month == 9 || month == 11)
         {
             if (day >= 31)
             {
                 month++;
                 GameManager.isMonthEnd = true;

                 day = 1;
                 DetermineMonth();
             }
         }
     }
 }
 
 //time counter
 void CalculateTime()
 {
     if (timeMode == 1)
     {
         second += Time.fixedDeltaTime * timeScale;
         if (second >= 60)
         {
             minute++;
             second = 0;
             TextCallFunction();
         }
         else if (minute >= 60)
         {
             if (hour <= 12 && am == true)
             {
                 hour++;
                 if (hour >= 13)
                 {
                     am = false;
                     hour = 1;
                 }
             }
             else if (hour <= 12 && am == false)
             {
                 hour++;
                 if (hour == 12)
                 {
                     day++;
                 }
                 else if (hour >= 13)
                 {
                     hour = 1;
                     am = true;
                 }
             }
             minute = 0;
             TextCallFunction();
         }
         else if (day >= 28)
         {
             CalculateMonthLength();
         }
         else if (month >= 12)
         {
             month = 1;
             year++;
             DetermineMonth();
         }
     }
     else if (timeMode == 2)
     {
         minute+= Time.fixedDeltaTime * timeScale*Time.timeScale;
         
         
         
         if (second >= 60)
         {
             minute++;
             second = 0;
             TextCallFunction();
         }
         else if (minute >= 60)
         {
             if (hour <= 12 && am == true)
             {
                 hour++;
                 if (hour >= 13)
                 {
                     am = false;
                     hour = 1;
                 }
             }
             else if (hour <= 12 && am == false)
             {
                 hour++;
                 if (hour == 12)
                 {
                     day++;
                 }
                 else if (hour >= 13)
                 {
                     hour = 1;
                     am = true;
                 }
             }
             minute = 0;
             TextCallFunction();
         }
         else if (day >= 28)
         {
             CalculateMonthLength();
             //month end
         }
         else if (month >= 12)
         {
             month = 1;
             year++;
             DetermineMonth();
         }
     }
     else if (timeMode == 3)
     {
         second += Time.fixedDeltaTime * timeScale;
         
         if (second >= 0.4f)
         {
             day++;
             second = 0;
             DetermineMonth();
         }
         else if (day >= 28)
         {
             CalculateMonthLength();
             DetermineMonth();
         }
         else if (month >= 12)
         {
             month = 1;
             year++;
             DetermineMonth();
         }
     }            
 }
 }