using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using VatsimLibrary.VatsimClient;
using VatsimLibrary.VatsimDb;

namespace hw
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine($"{VatsimDbHepler.DATA_DIR}");

            // using(var db = new VatsimDbContext())
            // {
                               // Call Querys with IF statements here...
                Console.Write("Which query would you like to run? (1-10) ");
                string response =Console.ReadLine();
                if(response == "1"){
                    Query1();                    
                }else if(response =="2"){
                    Query2();
                }else if(response =="3"){
                   Query3();
                }else if(response =="4"){
                   Query4();
                }else if(response =="5"){
                    Query5();
                }else if(response =="6"){
                    Query6();
                }else if(response =="7"){
                   Query7();
                }else if(response =="8"){
                   Query8();
                }else if(response == "9"){
                    Query9();
                }else{
                    Query10();
                }

                // var pLength = db.Pilots.TimeLogon.Max(t => t.Realname);
                // Console.WriteLine(pLength);
            
                /*
                var _pilots = db.Pilots.Select(p => p).ToList();
                Console.WriteLine($"The number of pilots records is: {_pilots.Count} ");

                //1238470                
                //UAL2865
                //20201013162413
                var _pilot = db.Pilots.Find("1238470", "UAL2865", "20201013162413");                
                if(_pilot != null){
                    Console.WriteLine($"Pilot found: {_pilot.Realname}");
                } else {
                    Console.WriteLine("Pilot not found");
                }                

                //1385451
                //N130JM
                //20201021233811
                _pilot = db.Pilots.Find("1385451", "N130JM", "20201021233811");
                if(_pilot != null){
                    Console.WriteLine($"Pilot found: {_pilot.Realname}");
                } else {
                    Console.WriteLine("Pilot not found");
                }                                
                */

            // }            
        
        }
        static void Query1(){
            using(var db = new VatsimDbContext()){
             var _pilots = db.Pilots.Select(p => p).ToList();
                var timeLog = from p in _pilots select p.TimeLogon;
                var tlMax = timeLog.Max();
                var plength =
                from p in _pilots
                where p.TimeLogon == tlMax
                select p.Realname;
                Console.WriteLine(plength.ToList()[0]);
            }
        }
        static void Query2(){
            using(var db = new VatsimDbContext()){
            var controller = db.Controllers.Select(p => p).ToList();
                var timeLog = from c in controller select c.TimeLogon;
                var tlMax = timeLog.Max();
                var clength =
                from c in controller
                where c.TimeLogon == tlMax
                select c.Realname;
                Console.WriteLine(clength.ToList()[0]);
            }
        }
        static void Query3(){
            using(var db = new VatsimDbContext()){
            var departs = db.Flights.Select(f => f).ToList();
                var departAirport = from d in departs
                group d by d.PlannedDepairport into departGroup
                select new {
                    Airport = departGroup.Key,
                    departCount = departGroup.Count(),
                };
            
                
                List<int> airportCount = new List<int>();
                    foreach (var item in departAirport)
                    {
                        airportCount.Add(item.departCount);
                    }
                
                var nameOfAirport = from a in departAirport
                where
                a.departCount == airportCount.Max()
                select a.Airport; 
                Console.WriteLine(nameOfAirport.ToList()[0]);
            }

        }
        static void Query4(){
            using(var db = new VatsimDbContext()){
            var arrival = db.Flights.Select(f => f).ToList();
                var arriveAirport = from d in arrival
                group d by d.PlannedDestairport into arriveGroup
                select new {
                    Airport = arriveGroup.Key,
                    arriveCount = arriveGroup.Count(),
                };
                
                List<int> airportCount = new List<int>();
                    foreach (var item in arriveAirport)
                    {
                        airportCount.Add(item.arriveCount);
                    }
                
                var nameOfAirport = from a in arriveAirport
                where
                a.arriveCount == airportCount.Max()
                select a.Airport; 
                Console.WriteLine(nameOfAirport.ToList()[0]);
            }
        }
        static void Query5(){
            using(var db = new VatsimDbContext()){
             var _positions = db.Positions.Select(p => p).ToList();
                 var alti = from p in _positions select p.Altitude;
                 var altiMax = alti.Max();
                var pName =
                from p in _positions
                where p.Altitude == altiMax
                select p.Realname;
               
                var _flights = db.Flights.Select(f => f).ToList();
                var pilotName = from pl in _flights where pl.Realname == pName.ToList()[0]
                select pl;
                var plane = from pn in pilotName select pn.PlannedAircraft;

                Console.WriteLine(alti.ToList()[0]);
                Console.WriteLine(pName.ToList()[0]);
                Console.WriteLine(plane.ToList()[0]);
            }
        }
        static void Query6(){
            using(var db = new VatsimDbContext()){
                var _positions = db.Positions.Select(p => p).ToList();
                var gSpeed = from s in _positions select s.Groundspeed;
                var orderSpeed = from o in gSpeed orderby Convert.ToInt32(o) ascending select o;
                orderSpeed.ToList();
                var lowestSpeed = from l in orderSpeed where l !=  "0" select l;
                Console.WriteLine(lowestSpeed.ToList()[0]);


            }
        }
        static void Query7(){
            using(var db = new VatsimDbContext()){
            var _flights = db.Flights.Select(f => f).ToList();
                var topAircraft = from d in _flights
                group d by d.PlannedAircraft into planeGroup
                select new {
                    plane = planeGroup.Key,
                    planeCount = planeGroup.Count(),
                };
                
                List<int> airplaneCount = new List<int>();
                    foreach (var item in topAircraft)
                    {
                        airplaneCount.Add(item.planeCount);
                    }
                
                var nameOfAirplane = from a in topAircraft
                where
                a.planeCount == airplaneCount.Max()
                select a.plane; 
                Console.WriteLine(nameOfAirplane.ToList()[0]);
                
            }
        }
        static void Query8(){
            using(var db = new VatsimDbContext()){
                var _positions = db.Positions.Select(p => p).ToList();
                var gSpeed = from s in _positions select s.Groundspeed;
                var orderSpeed = from o in gSpeed orderby Convert.ToInt32(o) descending select o;                
                Console.WriteLine(orderSpeed.ToList()[0]);
            }
        }
        static void Query9(){
            using(var db = new VatsimDbContext()){
            var _positions = db.Positions.Select(p => p).ToList();
            var stringHeadings = from b in _positions select b.Heading;
            var northHeading = from h in stringHeadings where 90 <= Convert.ToInt32(h) && Convert.ToInt32(h) <= 270 select h; 
            int numberOfPilots = northHeading.Count();
            Console.WriteLine(numberOfPilots);
            
            }
        }
        static void Query10(){
            using(var db = new VatsimDbContext()){
                var _flights = db.Flights.Select(f => f).ToList();
                var remarks = from r in _flights select r.PlannedRemarks;
                var charRemarks = from c in remarks orderby remarks.Length descending select c;
                Console.WriteLine(charRemarks);

            }
        }
        /*
            1) Which pilot has been logged on the longest?DONE
            2) Which controller has been logged on the longest?DONE
            3) Which airport has the most departures?DONE
            4) Which airport has the most arrivals?DONE
            5) Who is flying at the highest altitude and what kind of plane are they flying?DONE
            6) Who is flying the slowest (hint: they can't be on the ground)
            7) Which aircraft type is being used the most?same as number 5 and count groups 
            8) Who is flying the fastest?
            9) How many pilots are flying North? (270 degrees to 90 degrees)
            10) Which pilot has the longest remarks section of their flight?
*/
    }
}
