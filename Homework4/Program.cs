/*
 
Написать сериализацию свойств или полей класса в строку
Проверить на классе: class F { int i1, i2, i3, i4, i5; Get() => new F(){ i1 = 1, i2 = 2, i3 = 3, i4 = 4, i5 = 5 }; }
Замерить время до и после вызова функции (для большей точности можно сериализацию сделать в цикле 100-100000 раз)
Вывести в консоль полученную строку и разницу времен
Отправить в чат полученное время с указанием среды разработки и количества итераций
Замерить время еще раз и вывести в консоль сколько потребовалось времени на вывод текста в консоль
Провести сериализацию с помощью каких-нибудь стандартных механизмов (например в JSON)
И тоже посчитать время и прислать результат сравнения
Написать десериализацию/загрузку данных из строки (ini/csv-файла) в экземпляр любого класса
Замерить время на десериализацию
Общий результат прислать в чат с преподавателем в системе в таком виде:
 
 */



using System.Xml.Serialization;
using System;
using Homework4;
using System.Reflection;
using System.Diagnostics;
using System.Xml.Linq;
using System.Data;
using System.Reflection.Emit;
using System.IO;
using System.Threading.Tasks;

internal class Program
{
    private static void Main(string[] args)
    {
        // Serialize();
        Task3();
        Task6();
        Task8();
        Task9();
        Task10();
        // var res = Deserialize();
        Console.ReadLine();

    }
    private static void Task3()
    {
        var sw = new Stopwatch();
        var f = F.Get();
        var serialiser = new StringSerializer(f.GetType());

        sw.Start();
        for (var i = 0; i < 10000; i++)
        {
            using (var oStream = new FileStream(@"strFile.csv", FileMode.Create))
            {
                serialiser.Serialize(oStream, f);
                oStream.Close();
            }
        }
        sw.Stop();
        Console.WriteLine(sw.Elapsed);
    }
    private static void Task6()
    {
        var sw = new Stopwatch();
        sw.Start();
        for (int i = 0; i < 10000; i++)
        {
            Console.WriteLine(sw.Elapsed);
        }
        sw.Stop();
        Console.WriteLine($"Итого {sw.Elapsed}");
    }
    private static void Task8()
    {
        var sw = new Stopwatch();
        var f = F.Get();
        var serialiser = new XmlSerializer(f.GetType());

        sw.Start();
        for (var i = 0; i < 10000; i++)
        {
            using (var oStream = new FileStream(@"strFile.xml", FileMode.Create))
            {
                serialiser.Serialize(oStream, f);
                oStream.Close();
            }
        }
        sw.Stop();
        Console.WriteLine(sw.Elapsed);
    }
    private static void Task9()
    {
        var sw = new Stopwatch(); 
        var serialiser = new StringSerializer(typeof(F));

        sw.Start();
        for (var i = 0; i < 10000; i++)
        {
            using (var oStream = new FileStream(@"strFile.csv", FileMode.Open))
            {
                var res = (F)serialiser.Deserialize(oStream);
                oStream.Close();
            }
        }
        sw.Stop();
        Console.WriteLine(sw.Elapsed);
    }
    private static void Task10()
    {
        var sw = new Stopwatch();
        var serialiser = new XmlSerializer(typeof(F));

        sw.Start();
        for (var i = 0; i < 10000; i++)
        {
            using (var oStream = new FileStream(@"strFile.xml", FileMode.Open))
            {
                var res = (F)serialiser.Deserialize(oStream);
                oStream.Close();
            }
        }
        sw.Stop();
        Console.WriteLine(sw.Elapsed);
    }
    private static void Serialize()
    {
        var f = F.Get();
        var serialiser = new StringSerializer(f.GetType());
        using (var oStream = new FileStream(@"strFile.csv", FileMode.Create))
        {
            serialiser.Serialize(oStream, f);
            oStream.Close();
        }
    }
    private static F Deserialize()
    {
        var serialiser = new StringSerializer(typeof(F));
        var oStream = new FileStream(@"strFile.csv", FileMode.Open);
        return (F)serialiser.Deserialize(oStream); 
    }
}