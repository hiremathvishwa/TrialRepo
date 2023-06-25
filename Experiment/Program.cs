using System;
using System.Collections.Generic;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;

namespace Assignment1
{
    class EntityData
    {
        public string Name { get; set; }
        public bool Attendance { get; set; }

        public EntityData() { 
            
            Console.WriteLine("Enter the name:");
            Name = Console.ReadLine();
        }
    }


    class Section
    {

        public string SectionName { get; set; }

        public List<Student> Students = new List<Student>();

        public Section(string sectionName)
        {
            SectionName = sectionName;
        }
       
        public void Register(Student student)
        {
            Students.Add(student);
            student.Section = this;
        }

        public void Transfer(Student student, Section section)
        {
            int index=Indexof(Students, student);
            if(index!=-1)
            {
                Students.RemoveAt(index);
                section.Register(student);
            }
            else
            {
                Console.WriteLine("Enter valid section");
            }
        }
        private int Indexof(List<Student> students,Student student)
        {
            for(int i=0;i<students.Count;++i)
            {
                if (students[i] == student)
                    return i;
            }
            return -1;
        }

        public List<Student> GetAllStudent()
        {
            return Students;
        }
    }
    class Teacher:EntityData
    {

        public Section Section { get; set; }
        public EntityData entity_Data { get; set; }


    }

        class Student: EntityData
        {
            public string ClassName { get; set; }

            public int StudentID { get; set; }
            public string EmailID { get; set; }

            public Section Section { get; set; }
            public EntityData Entitydata { get; set; }
   

            public Student()
            {
                Console.WriteLine();
                Console.WriteLine("Enter Student Details:");

                Console.WriteLine("Enter class name:");
                ClassName=Console.ReadLine();
                Console.Write("Enter Student ID: ");
                StudentID = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter Email ID: ");
                EmailID = Console.ReadLine();
            }
        }






    class Program
    {

        static List<Section> section;
        static List<Student> students;
        static List<Teacher> teacher;

        static void PrintBySection()
        {
            foreach(Section sec in section)
            {
                Console.WriteLine("------------------------");
                Console.WriteLine($"Section is:{sec.SectionName}");
                ViewStudents(sec.Students);
            }
        }
        static void Main(string[] args)
        {
        
            Console.WriteLine("Enter the number of students:");
            int n = Convert.ToInt32(Console.ReadLine());

             students= new List<Student>();
           teacher=new List<Teacher>();  
             section = new List<Section>();
            //Considering only 3 sections for now
            Section sec1=new Section("A");
            Section sec2 = new Section("B");
            Section sec3 = new Section("C");
            section.Add(sec1);
            section.Add(sec2);  
            section.Add(sec3);

            for (int i = 0; i < n; i++)
            {
                        Console.WriteLine();
                        Console.WriteLine($"Enter details for student #{i + 1}");
                        Student student = new Student();
                        Console.WriteLine("Enter section");
                        string sectionName= Console.ReadLine();
                Section NewSection = section.Find(sec => sec.SectionName == sectionName);
                if (NewSection == null)
                {
                    NewSection = new Section(sectionName);
                    section.Add(NewSection);
                }

               
                NewSection.Register(student);

                students.Add(student);

                  
            }

            Console.WriteLine("Enter number of teachers:");
            int t=Convert.ToInt32(Console.ReadLine());  

            for(int i=0;i< t;++i)
            {
                Console.WriteLine();
                Console.WriteLine($"Enter details for teacher #{i + 1}");
                Teacher teacher1 = new Teacher();
                Console.WriteLine("Enter section");
                string sectionName = Console.ReadLine();
                Section NewSection = section.FirstOrDefault(sec => sec.SectionName == sectionName);
                teacher1.Section = NewSection;
               
                Console.WriteLine("Enter attendance of the teacher:Y/N");
               
                string InputAttend = Console.ReadLine();
               if(InputAttend=="Y")
                    teacher1.Attendance = true;
               else
                    teacher1.Attendance= false;
                
                teacher.Add(teacher1);

            }
                
              
            

            while (true)
            {
                Console.WriteLine("Enter your choice:\n 1. View Students details\n 2. Mark Attendance\n 3. Sort students by attendance \n 4. Sort students by class and section \n 5.Sort students by class \n 6. Transfer student by section\n 7. Exit");
                string ch = Console.ReadLine();

                switch (ch)
                {
                    case "1":
                        ViewStudents(students);
                        break;

                    case "2":
                        MarkAttendance();
                        break;

                    case "3":
                        SortByAttendance();
                        break;

                    case "4":
                        SortByClassAndSection(students);
                        break;

                    case "5":
                        SortByClass(students);
                        break;

                    case "6":
                        TransferBySection(students);
                        break;

                    case "7":
                        CheckAndAssignTeacher();
                        break;

                    case "8":
                        Environment.Exit(0);
                        break;

                    case "9":
                        Console.WriteLine("Enter current sec:");
                        string cur_sec=Console.ReadLine();
                        int StudId=Convert.ToInt32(Console.ReadLine());
                        foreach(Student s in students)
                        {
                            if(s.StudentID==StudId && s.Section.SectionName==cur_sec)
                            {
                                Console.WriteLine("Enter new section");
                               string target_sec= Console.ReadLine();

                                Section sec = section.FirstOrDefault(x => x.SectionName == target_sec);//lambda function yet to be learnt
                                
                                s.Section.Transfer(s, sec);

                            }

                        }
                        PrintBySection();
                        break;
                       

                    default:
                        Console.WriteLine("Sorry! Enter a valid choice......");
                        break;
                }
            }
        }

        static void ViewStudents(List<Student> students)//added in student class
        {
            Console.WriteLine("Student List");
            Console.WriteLine("------------");

            foreach (Student stud in students)
            {
                string AttendanceStatus = stud.Attendance ? "Present" : "Absent";
                Console.WriteLine($"Student ID: {stud.StudentID}");
                Console.WriteLine($"Name: {stud.Name}");
                Console.WriteLine($"Attendance: {AttendanceStatus}");
                Console.WriteLine($"Section: {stud.Section.SectionName}");
                Console.WriteLine($"Class: {stud.ClassName}");
                Console.WriteLine();
            }
        }

        static void MarkAttendance()
        {
            Console.WriteLine();
            Console.WriteLine("Mark Attendance");
            Console.WriteLine("---------------");

            foreach (Student stud in students)
            {
                Console.Write($"Is {stud.Name} present? (Y/N): ");
                string Input = Console.ReadLine().ToUpper();

                if (Input == "Y")
                {
                    stud.Attendance = true;
                }
                else if (Input == "N")
                {
                    stud.Attendance = false;
                }
                else
                {
                    Console.WriteLine("Invalid input! Marking as absent.");
                    stud.Attendance = false;
                }
            }

            Console.WriteLine("Attendance marked successfully.");
        }



        static void CheckAndAssignTeacher()//add in teacher class as an abstract class
        {
            Teacher AbsentTeacher = null;
            Teacher UnassignedTeacher = null;

            foreach (Teacher t in teacher)
            {
                if (!t.Attendance && AbsentTeacher == null)
                {
                    AbsentTeacher = t;
                }
                else if (t.Attendance && t.Section == null)
                {
                    UnassignedTeacher = t;
                }

                if (AbsentTeacher != null && UnassignedTeacher != null)
                {
                    break;
                }
            }

            if (AbsentTeacher != null && UnassignedTeacher != null)
            {
                UnassignedTeacher.Section = AbsentTeacher.Section;
                AbsentTeacher.Section = null;
            }

            foreach (Teacher t in teacher)//move to another function
            {
                string AttendanceStatus = t.Attendance ? "Present" : "Absent";
                Console.WriteLine($"Name: {t.Name}");
                Console.WriteLine($"Attendance: {AttendanceStatus}");

                if (t.Section == null)
                {
                    Console.WriteLine("I am free to take a class");
                }
                else
                {
                    Console.WriteLine($"Section: {t.Section.SectionName}");
                }

                Console.WriteLine();
            }
        }





        static void SortByAttendance()
        {
            List<Student> Present = new List<Student>();
            List<Student> Absent = new List<Student>();
            foreach (Student stud in students)
            {
                if (stud.Attendance)
                {
                    Present.Add(stud);
                }
                else
                {
                    Absent.Add(stud);
                }
            }
            Console.WriteLine("List of students who are present:");
            ViewStudents(Present);
            Console.WriteLine("List of students who are absent:");
            ViewStudents(Absent);
        }

        static void SortBySection(List<Student> students)
        {
            Dictionary<string, List<Student>> StudentGroups = new Dictionary<string, List<Student>>();

            foreach (Student stud in students)
            {
                string section = stud.Section.SectionName;

                if (!StudentGroups.ContainsKey(section))
                {
                    StudentGroups[section] = new List<Student>();
                }

                StudentGroups[section].Add(stud);
            }

            Console.WriteLine("Students Grouped by Section");
            Console.WriteLine("---------------------------");

            foreach (var group in StudentGroups)
            {
                Console.WriteLine($"Section: {group.Key}");

                foreach (Student stud in group.Value)
                {
                    string AttendanceStatus = stud.Attendance ? "Present" : "Absent";
                    Console.WriteLine($"Student ID: {stud.StudentID}");
                    Console.WriteLine($"Name: {stud.Name}");
                    Console.WriteLine($"Attendance: {AttendanceStatus}");
                    Console.WriteLine();
                }
            }
        }

        static void SortByClass(List<Student> students)
        {
            Dictionary<string, List<Student>> ClassGroups = new Dictionary<string, List<Student>>();

            foreach (Student stud in students)
            {
                string Class = stud.ClassName;

                if (!ClassGroups.ContainsKey(Class))
                {
                    ClassGroups[Class] = new List<Student>();
                }

                ClassGroups[Class].Add(stud);
            }

            Console.WriteLine("Students Grouped by Class");
            Console.WriteLine("---------------------------");

            foreach (var group in ClassGroups)
            {
                Console.WriteLine($"Class: {group.Key}");

                foreach (Student stud in group.Value)
                {
                    string AttendanceStatus = stud.Attendance ? "Present" : "Absent";
                    Console.WriteLine($"Student ID: {stud.StudentID}");
                    Console.WriteLine($"Name: {stud.Name}");
                    Console.WriteLine($"Attendance: {AttendanceStatus}");
                    Console.WriteLine();
                }
            }
        }

        static void SortByClassAndSection(List<Student> students)
        {
            Dictionary<string, Dictionary<string, List<Student>>> ClassSection = new Dictionary<string, Dictionary<string, List<Student>>>();
            foreach(Student stud in students)
            {
                string CName=stud.ClassName;
                string SName = stud.Section.SectionName;

                if(!ClassSection.ContainsKey(CName))
                {
                    ClassSection[CName] = new Dictionary<string, List<Student>>();
                }
                if (!ClassSection[CName].ContainsKey(SName))
                {
                    ClassSection[CName][SName] = new List<Student>();
                }
                ClassSection[CName][SName].Add(stud);

            }
            Console.WriteLine("Sort students by class and section");
            Console.WriteLine("-----------------------------------");
            foreach(var group in ClassSection)
            {
                string CName=group.Key;
                Console.WriteLine($"Class :{CName}");

                foreach(var section in group.Value)
                {
                    string Sec = section.Key;
                    Console.WriteLine($"Section:{Sec}");
                    foreach(Student stud in section.Value)
                    {
                        string AttendanceStatus = stud.Attendance ? "Present" : "Absent";
                        Console.WriteLine($"Student ID: {stud.StudentID}");
                        Console.WriteLine($"Name: {stud.Name}");
                        Console.WriteLine($"Attendance: {AttendanceStatus}");
                        Console.WriteLine();
                    }
                }
            }

        }

        static void TransferBySection(List<Student> students)
        {
            Console.WriteLine("Enter the ID of the student:");
            int StudId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the current section of the student:");
            string PresentSec = Console.ReadLine();
            bool Success = false;
            foreach (Student stud in students)
            {
                if (stud.StudentID == StudId && stud.Section.SectionName == PresentSec)
                {
                    Console.WriteLine("Enter the desired section to be transferred to:");
                    string DesiredSection = Console.ReadLine();
                    stud.Section.SectionName = DesiredSection;
                    Console.WriteLine($"Student transferred to {stud.Section.SectionName} section successfully.");
                    Success = true;
                    break;
                }
            }

            if (!Success)
            {
                Console.WriteLine("Transfer of student was unsuccessful.....Try again with valid details.");
            }
        }
    }
}
