using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HubRE.Controllers
{
    public class HospitalController : Controller
    {
        int[,] hospital_sick = new int[,] {
                                /*Sick Degre*/
        /*Hospital_1*/   { 05 , 07 , 16 , 30 , 35 },
        /*Hospital_2*/   { 10 , 11 , 15 , 20 , 29 },
        /*Hospital_3*/   { 15 , 18 , 20 , 20 , 30 },
        /*Hospital_4*/   { 10 , 10 , 15 , 20 , 28 },
        /*Hospital_5*/   { 18 , 20 , 20 , 25 , 32 },
        /*Hospital_6*/   { 12 , 20 , 30 , 32 , 38 },
        };

       static List<Models.Hospital> hospital_list = new List<Models.Hospital>()
        {
            new Models.Hospital{id=0,name="بیمارستان امام خمینی ",hospital_waite_queue=new List<int>()},
            new Models.Hospital{id=1,name="بیمارستان امام علی ",hospital_waite_queue=new List<int>()},
            new Models.Hospital{id=2,name="بیمارستان شریعتی ",hospital_waite_queue=new List<int>()},
            new Models.Hospital{id=3,name="بیمارستان منتظری ",hospital_waite_queue=new List<int>()},
            new Models.Hospital{id=4,name="بیمارستان خانواده ",hospital_waite_queue=new List<int>()},
            new Models.Hospital{id=5,name="بیمارستان مطهری ",hospital_waite_queue=new List<int>()},
            new Models.Hospital{id=6,name="بیمارستان معین ",hospital_waite_queue=new List<int>()},
        };
        

        public ActionResult Index(int sick_level=0)
        {

            //محاسبه 
            //نشان دادن بیمارستان

            List<Models.View_hospital> list = new List<Models.View_hospital>();
            foreach (var item in hospital_list)
            {
                Models.View_hospital temp = new Models.View_hospital();
                temp.Hospital_id = item.id;
                temp.Hospital_name = item.name;
                temp.time = calcte_time(sick_level, item.id, item.hospital_waite_queue);
                list.Add( temp); 
            }
            list=list.OrderBy(x=>x.time).ToList();
            ViewBag.Hospital_Wait_Time = list;
            ViewBag.SickLevel = sick_level;
            return View();
        }

        int calcte_time(int id,int H_id,List<int> hospital_list)
        {
            int sum = 0;
            foreach (var item in hospital_list)
                if (item <= id)
                {
                    sum += hospital_sick[H_id, item];
                }
            return sum;
        }
        [HttpPost]
        public ActionResult Submit_Turn(string date, string time, string hostpital_name, int hospital_id, string user_name, string user_phone ,int sick_level,int hospital_wait_time)
        {
            //save user to Database
            //upadate hospital list 
            hospital_list[hospital_id].hospital_waite_queue.Add(sick_level);
            hospital_list[hospital_id].hospital_waite_queue.Sort();
           int jayegah_dar_saf= hospital_list[hospital_id].hospital_waite_queue.LastIndexOf(sick_level)+1;
            ViewBag.hostpital_name = hostpital_name;
            ViewBag.user_name = user_name;
            ViewBag.user_phone = user_phone;
            ViewBag.jayegah_dar_saf = jayegah_dar_saf;
            ViewBag.hospital_wait_time = hospital_wait_time;
            return View();

        }




    }
}
