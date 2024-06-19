using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class StudentManager
{
    private List<Student> students;
    private string filePath;

    public StudentManager()
    {

        students = new List<Student>
        {
            new Student(1, "John Doe", 20, true, "Computer Science", 3.5f),
            new Student(2, "Jane Smith", 22, false, "Mathematics", 3.8f),
            new Student(3, "Alice Johnson", 21, false, "Physics", 3.7f),
            new Student(4, "Bob Brown", 23, true, "Chemistry", 3.2f),
            new Student(5, "Charlie Davis", 24, true, "Biology", 3.6f)
        };
        //filePath = Path.Combine(Application.persistentDataPath, "students.json");
        //LoadStudents();
    }

    public void AddStudent(Student student)
    {
        students.Add(student);
        //SaveStudents();
    }

    public List<Student> GetAllStudents()
    {
        return students;
    }

    public Student GetStudentById(int id)
    {
        return students.Find(student => student.Id == id);
    }

    //public void RemoveStudent(int id)
    //{
    //    Student student = GetStudentById(id);
    //    if (student != null)
    //    {
    //        students.Remove(student);
    //        //SaveStudents();
    //    }
    //}
    public bool RemoveStudent(int id)
    {
        Student student = students.Find(s => s.Id == id);
        if (student != null)
        {
            students.Remove(student);
            return true;
        }
        return false;
    }

    //private void SaveStudents()
    //{
    //    string json = JsonUtility.ToJson(new StudentListWrapper { students = this.students });
    //    File.WriteAllText(filePath, json);
    //}

    //private void LoadStudents()
    //{
    //    if (File.Exists(filePath))
    //    {
    //        string json = File.ReadAllText(filePath);
    //        StudentListWrapper wrapper = JsonUtility.FromJson<StudentListWrapper>(json);
    //        students = wrapper.students;
    //    }
    //}

    public void UpdateStudent(Student updatedStudent)
    {
        Student existingStudent = students.Find(s => s.Id == updatedStudent.Id);
        if (existingStudent != null)
        {
            existingStudent.Name = updatedStudent.Name;
            existingStudent.Age = updatedStudent.Age;
            existingStudent.Sex = updatedStudent.Sex;
            existingStudent.Major = updatedStudent.Major;
            existingStudent.Grade = updatedStudent.Grade;
        }
        else
        {
            Debug.LogWarning($"Student with ID {updatedStudent.Id} not found.");
        }
    }

    //public void UpdateStudent(Student updatedStudent)
    //{
    //    for (int i = 0; i < students.Count; i++)
    //    {
    //        if (students[i].Id == updatedStudent.Id)
    //        {
    //            // C?p nh?t t?ng tr??ng c?a sinh viên
    //            students[i].Name = updatedStudent.Name;
    //            students[i].Age = updatedStudent.Age;
    //            students[i].Sex = updatedStudent.Sex;
    //            students[i].Major = updatedStudent.Major;
    //            students[i].Grade = updatedStudent.Grade;
    //            break;
    //        }
    //    }
    //}

    public List<Student> SearchStudentsByName(string name)
    {
        return students.Where(s => s.Name.ToLower().Contains(name.ToLower())).ToList();
    }

    [System.Serializable]
    private class StudentListWrapper
    {
        public List<Student> students;
    }
}
