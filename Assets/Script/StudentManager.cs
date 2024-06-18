using System;
using System.Collections.Generic;
using System.IO;
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

    public void RemoveStudent(int id)
    {
        Student student = GetStudentById(id);
        if (student != null)
        {
            students.Remove(student);
            SaveStudents();
        }
    }

    private void SaveStudents()
    {
        string json = JsonUtility.ToJson(new StudentListWrapper { students = this.students });
        File.WriteAllText(filePath, json);
    }

    private void LoadStudents()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            StudentListWrapper wrapper = JsonUtility.FromJson<StudentListWrapper>(json);
            students = wrapper.students;
        }
    }

    public void UpdateStudent(Student updatedStudent)
    {
        for (int i = 0; i < students.Count; i++)
        {
            if (students[i].Id == updatedStudent.Id)
            {
                students[i] = updatedStudent;
                break;
            }
        }
    }

    [System.Serializable]
    private class StudentListWrapper
    {
        public List<Student> students;
    }
}
