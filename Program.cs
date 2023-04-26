using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Serialisation
{
    internal class Program
    {
        [Serializable]
        public class Treadmill {
            public Treadmill() { }
            public int number { get; }
            public List <Schedule> schedule = new List<Schedule>();
            public Treadmill(int number, List<Schedule> schedule)
            {
                this.number = number;
                this.schedule = schedule;
            }
        }
        [Serializable]
        public class Trainer
        {
            public Trainer() { }
            string surname;
            string number;
            double zp_p_hour;
            public Schedule schedule;
            public Trainer(string surname, string number, double zp_p_hour, Schedule schedule)
            {
                this.surname = surname;
                this.number = number;
                this.zp_p_hour = zp_p_hour;
                this.schedule = schedule;
            }
        }
        [Serializable]
        public class Student
        {
            public void print()
            {
                Console.WriteLine($"Name: {name}");
                Console.WriteLine($"Surname: {surname}");
                Console.WriteLine($"Parent phone number: {P_T_number}");
                Console.WriteLine($"Phone number: {T_number}");
                Console.WriteLine($"Lessons payed: {payed_lessons}");
            }
            string name;
            string surname;
            string P_T_number;
            string T_number;
            int payed_lessons;
            public Groop groop;
            public Student() { }
            public Student(string name, string surname, string p_T_number, string number, int payed_lessons, Groop groop)
            {
                this.name = name;
                this.surname = surname;
                P_T_number = p_T_number;
                T_number = number;
                this.payed_lessons = payed_lessons;
                this.groop = groop;
            }
        }
        [Serializable]
        public class Groop{
            public Groop() { }
            public int number;
            public List<Student> students;
            public Schedule schedule;
            public Groop(int number, List<Student> students, Schedule schedule)
            {
                this.number = number;
                this.students = students;
                this.schedule = schedule;
            }
        }
        [Serializable]
        public class Schedule
        {
            public Schedule() { }
            public string time = "";
            public Treadmill Treadmill_num;
            public Trainer trainer;
            public Groop groop;
            public Schedule(string time, Treadmill treadmill_num, Trainer trainer, Groop groop)
            {
                this.time = time;
                Treadmill_num = treadmill_num;
                this.trainer = trainer;
                this.groop = groop;
            }
        }
        [Serializable]
        public class General
        {
            List<Student> students;
            Treadmill treadmill;
            Groop groop;
            Schedule schedule;
            public General(List<Student> students, Treadmill treadmill, Groop groop, Schedule schedule)
            {
                this.students = students;
                this.treadmill = treadmill;
                this.groop = groop;
                this.schedule = schedule;
            }
            public void print()
            {
                Console.WriteLine("STUDENTS:");
                foreach (Student student in students)
                {
                    student.print();
                }
                Console.WriteLine($"TREADMILL NUMBER: {this.treadmill.number}");
                Console.WriteLine($"GROOP: {groop.number}");
                Console.WriteLine($"SCHEDULE TIME: {schedule.time}");
            }
        }

        static void Main(string[] args)
        {
            Treadmill tr1 = new Treadmill(1,new List<Schedule>());
            Trainer trainer1 = new Trainer("Pylipko","1",900,new Schedule());
            Student student1 = new Student("Vododimyr","Petrenko","0504496366","0663567236",3,new Groop());
            Student student2 = new Student("Andryi", "Zaharchenko", "0504985366", "0661237236", 5, new Groop());
            Student student3 = new Student("Sergiy", "Lakusta", "0501578366", "0663575436", 8, new Groop());
            Student student4 = new Student("Artem", "Gubka", "0504489466", "0663567123", 2, new Groop());
            Groop gr1 = new Groop(222,new List<Student>() { student1,student2,student3,student4},new Schedule());
            Schedule schedule1 = new Schedule("18:00-19:30",tr1,trainer1,gr1);
            gr1.schedule=schedule1;
            student1.groop = gr1;
            student2.groop = gr1;
            student3.groop = gr1;
            student4.groop = gr1;
            trainer1.schedule=schedule1;
            General gn1 = new General(new List<Student> { student1,student2,student3,student4},tr1,gr1, schedule1);
            BinaryFormatter binFormat = new BinaryFormatter();
            try
            {
                using (Stream fStream = File.Create("serialize.bin"))
                {
                    binFormat.Serialize(fStream, gn1);
                }
                Console.WriteLine("OK! \n");
                gn1 = null;
                using (Stream fStream = File.OpenRead("serialize.bin"))
                {
                    gn1 = (General)binFormat.Deserialize(fStream);
                }
                gn1.print();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.ReadKey();
        }
    }
}
