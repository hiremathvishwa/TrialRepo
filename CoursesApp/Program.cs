// See https://aka.ms/new-console-template for more information
class Course
{
    string course_name { get; set; }
   
    string course_type { get; set; }// frontend,backend

    public int course_id { get; set; }
    TimeOnly Course_duration { get; set; }
    DateOnly start_date { get; set; }
    DateOnly end_date { get; set; }



    List<Student> enrolled_students { get; set; }
}

class chapter : Course
{
    string chapter_name { get; set; }
    int chapter_num { get; set; }
    Course course { get; set; }

    string chapter_description { get; set; }
}

class Student
{
    public string student_name { get; set; }
    public int student_id { get; set; }
    TimeOnly Course_duration { get; set; }
    List<Course> enrolled_courses { get; set; }
    public string email { get; set; }
}

class Admin
{
    public string admin_name { get; set; }
    public int admin_id { get; set;}
}

class Instructor
{
    public string instructor_name { get; set; }
    public int instructor_id { get; set; }

    public string email { get; set; }
    List<Course> courses { get; set;}
}

class Assignment
{
    public int assignment_id { get; set; }
    public string assignment_name { get; set; }
    public string assignment_description { get; set; }
    Course course { get; set; }//to access the course details 

    DateOnly due_date { get; set; }

    List<string> responses_to_assignment { get; set; }
}

class User
{
    public string username { get; set; }
    public string password { get; set; }
    public string role { get; set; }//Admin,student,Ibstructor,
}

class Score
{
    public int student_id { get; set; }
    public int course_id { get; set; }
    public int assignment_id { get; set; }
    public int score { get; set; }
}

