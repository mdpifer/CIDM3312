using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using LINQDataSources;

namespace client
{
    class Program
    {
        static void Main(string[] args)
        {

            IEnumerable<IrisRecord> records = LoadIrisData();
            IEnumerable<IrisRecord> filtered;

            if(records != null)
            {
                // Call Querys with IF statements here...
                Console.Write("Which query would you like to run? (1-10");
                string response =Console.ReadLine();
                if(response == "1"){
                    Query1(records);                    
                }else if(response =="2"){
                    Query2(records);
                }else if(response =="3"){
                    Console.WriteLine(Query3(records));
                }else if(response =="4"){
                    Console.WriteLine(Query4(records));
                }else if(response =="5"){
                    Console.WriteLine(Query5(records));
                }else if(response =="6"){
                    Console.WriteLine(Query6(records));
                }else if(response =="7"){
                    Console.WriteLine(Query7(records));
                }else if(response =="8"){
                    Console.WriteLine(Query8(records));
                }else{
                    Console.WriteLine("Sorry that is not valid");
                }
                
            }
        }
        static void Query1(IEnumerable<IrisRecord> records)
        {
            // Create and show a LINQ Query that lists all Sepal Widths that are above the average Sepal Width
            var aveSepWidth = records.Average(sw => sw.SepalWidth);
               var querySepWidth =
                from record in records
                where record.SepalWidth > aveSepWidth
                select record;
                PrintRecords($"These Sepal Width are above the average Sepal Width {aveSepWidth}", querySepWidth);


        }
        static void Query2(IEnumerable<IrisRecord> records){
             // Create and show a LINQ Query that lists all Sepal Lengths that are below the average Sepal Length
            var aveSepLength = records.Average(sl => sl.SepalLength);
               var querySepLength =
                from record in records
                where record.SepalLength < aveSepLength
                select record;
                PrintRecords($"These Sepal Width are below the average Sepal Length {aveSepLength}", querySepLength);


        }
        static string Query3(IEnumerable<IrisRecord> records){
             // Create and show a LINQ Query that indicates which class of iris has the lowest average Petal Width
            var satosaAvg = (Name: "Iris-setosa", Avg: 0f);
            satosaAvg.Avg = records.Where(i => i.IrisClassificationName == satosaAvg.Name).Average<IrisRecord>(t => t.PetalWidth);
            var versicolorAvg = (Name: "Iris-versicolor", Avg: 0f);
            versicolorAvg.Avg = records.Where(i => i.IrisClassificationName == versicolorAvg.Name).Average<IrisRecord>(t => t.PetalWidth);
             var virginicaAvg = (Name: "Iris-virginica", Avg: 0f);
            virginicaAvg.Avg = records.Where(i => i.IrisClassificationName == virginicaAvg.Name).Average<IrisRecord>(t => t.PetalWidth);
            List<(string, double)> avgs = new List<(string, double)>(){
                satosaAvg,
                versicolorAvg,
                virginicaAvg
            };
            var petalWidthMinAvg = avgs.Min(a => a.Item2);
            string answer = $"min avg: {petalWidthMinAvg}";
            return answer;

        }
        static string Query4(IEnumerable<IrisRecord> records){
            // Create and show a LINQ Query that indicates which class of iris has the highest average Petal Length
            var satosaAvg = (Name: "Iris-setosa", Avg: 0f);
            satosaAvg.Avg = records.Where(i => i.IrisClassificationName == satosaAvg.Name).Average<IrisRecord>(t => t.PetalLength);
            var versicolorAvg = (Name: "Iris-versicolor", Avg: 0f);
            versicolorAvg.Avg = records.Where(i => i.IrisClassificationName == versicolorAvg.Name).Average<IrisRecord>(t => t.PetalLength);
             var virginicaAvg = (Name: "Iris-virginica", Avg: 0f);
            virginicaAvg.Avg = records.Where(i => i.IrisClassificationName == virginicaAvg.Name).Average<IrisRecord>(t => t.PetalLength);
            List<(string, double)> avgs = new List<(string, double)>(){
                satosaAvg,
                versicolorAvg,
                virginicaAvg
            };
            var petalWidthMaxAvg = avgs.Max(a => a.Item2);
            string answer = $"max avg: {petalWidthMaxAvg}";
            return answer;
        }
        static string Query5(IEnumerable<IrisRecord> records){
            //Create and show a LINQ Query that indicates the widest Sepal Width for each class of iris
            var setosaWidth = (Name: "Iris-setosa", Width: 0f);
            setosaWidth.Width = records.Where(i => i.IrisClassificationName == setosaWidth.Name).Max<IrisRecord>(t => t.SepalWidth);
            var versicolorWidth = (Name: "Iris-versicolor", Width: 0f);
            versicolorWidth.Width = records.Where(i => i.IrisClassificationName == versicolorWidth.Name).Max<IrisRecord>(t => t.SepalWidth);
            var virginicaWidth = (Name: "Iris-virginica", Width: 0f);
            virginicaWidth.Width = records.Where(i => i.IrisClassificationName == virginicaWidth.Name).Max<IrisRecord>(t => t.SepalWidth);
            List<(string, double)> maxs = new List<(string, double)>(){
                setosaWidth,
                versicolorWidth,
                virginicaWidth
            };
                string answer = $"Widest Sepal Width {maxs}";
                return answer;

        }
        static string Query6(IEnumerable<IrisRecord> records){
             // Create and show a LINQ Query that indicates the shortest Sepal Length for each class of iris
             var setosaLength = (Name: "Iris-setosa", Length: 0f);
            setosaLength.Length = records.Where(i => i.IrisClassificationName == setosaLength.Name).Min<IrisRecord>(t => t.SepalLength);
            var versicolorLength = (Name: "Iris-versicolor", Length: 0f);
            versicolorLength.Length = records.Where(i => i.IrisClassificationName == versicolorLength.Name).Min<IrisRecord>(t => t.SepalLength);
            var virginicaLength = (Name: "Iris-virginica", Length: 0f);
            virginicaLength.Length = records.Where(i => i.IrisClassificationName == virginicaLength.Name).Min<IrisRecord>(t => t.SepalLength);
            List<(string, double)> mins = new List<(string, double)>(){
                setosaLength,
                versicolorLength,
                virginicaLength
            };
                string answer = $"Smallest Sepal Length {mins}";
                return answer;
            
        }
        static string Query7(IEnumerable<IrisRecord> records){
            
            //Create and show a LINQ Query that indicates the ranks the top 5 widest Petal Widths
          var WidePetals = 
                (from record in records
                orderby record.PetalWidth descending
                select record.PetalWidth).Take(5);
                List<double> petals = new List<double>();
               foreach(var petal in WidePetals){
                   petals.Add(petal);
               }
                string answer = $"5 Widest Petals: {petals}";
                return answer;

        }
        static string Query8(IEnumerable<IrisRecord> records){
            // Create and show a LINQ Query that indicates the ranks the bottom 5 shortest Petal Lengths
             var LongPetals = 
                (from record in records
                orderby record.PetalLength ascending
                select record.PetalLength).Take(5);
                List<double> petals = new List<double>();
               foreach(var petal in LongPetals){
                   petals.Add(petal);
               }
                string answer = $"5 Longest Petals: {petals}";
                return answer;
            
        }
        // static string Query9(IEnumerable<IrisRecord> records){
            
        // }
        // static string Query10(IEnumerable<IrisRecord> records){
            
        // }

        static void PrintRecords(string message, IEnumerable<IrisRecord> records)
        {
            // simplest query shows all records
            Console.WriteLine(message);
            foreach(IrisRecord record in records)
            {
                Console.WriteLine(record);
            }
        }

        static IEnumerable<IrisRecord> LoadIrisData()
        {
            // this is somewhat "brittle" code as it only works when the project is
            // run within the client folder.
            Console.WriteLine($@"{Directory.GetCurrentDirectory()}\data\iris.data");
            FileInfo file = new FileInfo($@"{Directory.GetCurrentDirectory()}\data\iris.data");
            Console.WriteLine(file.FullName);
            
            IEnumerable<IrisRecord> records = null;

            try
            {
                records = IrisDataSourceHelper.GetIrisRecordsFromFlatFile(file.FullName);
            }catch (Exception exp)
            {
                Console.Error.WriteLine(exp.StackTrace);
            }
            return records;
        }
    }
}
