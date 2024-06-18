using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Student
{
    public int Id;
    public string Name;
    public int Age;
    public bool Sex;
    public string Major;
    public float Grade;
    

    public Student(int id, string name, int age, bool gt, string major, float grade)
    {
        Id = id;
        Name = name;
        Age = age;
        Sex = gt;
        Major = major;
        Grade = grade;
    }
}

