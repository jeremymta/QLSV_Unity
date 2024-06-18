using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Collections.Generic;
using Unity.VisualScripting;

public class StudentUIManager : MonoBehaviour
{
    public InputField idInputField;
    public InputField nameInputField;
    public InputField ageInputField;
    public Toggle toggleMale;
    public Toggle toggleFemale;
    public InputField majorInputField;
    public InputField gradeInputField;

    //public InputField searchInputField;
    //public Button searchButton;

    public Transform studentListPanel;
    public GameObject studentTextPrefab;

    private StudentManager studentManager;


    void Start()
    {
        studentManager = new StudentManager();
        //DisplayAllStudents();
        //searchButton.onClick.AddListener(SearchNameStudent);
    }

    public void AddStudent()
    {
        int id = int.Parse(idInputField.text);
        string name = nameInputField.text;
        int age = int.Parse(ageInputField.text);
        
        bool sex = toggleMale.isOn; // True if Male, False if Female
        string major = "majorInputField.text";
        float grade = float.Parse(gradeInputField.text);


        Student newStudent = new Student(id, name, age, sex, major, grade);
        studentManager.AddStudent(newStudent);

        DisplayAllStudents();
    }

    public void RemoveStudent()
    {
        int id = int.Parse(idInputField.text);
        studentManager.RemoveStudent(id);

        DisplayAllStudents();
    }

    public void UpdateStudent()
    {
        int id = int.Parse(idInputField.text);
        string name = nameInputField.text;
        int age = int.Parse(ageInputField.text);
        bool sex = toggleMale.isOn; 
        string major = majorInputField.text;
        float grade = float.Parse(gradeInputField.text);

        Student updatedStudent = new Student(id, name, age, sex, major, grade);
        studentManager.UpdateStudent(updatedStudent);

        DisplayAllStudents();
    }

    public void DisplayAllStudents()
    {
        //clear previous list
        foreach (GameObject child in studentListPanel.transform)
        {
            Destroy(child);
        }

        List<Student> students = studentManager.GetAllStudents();
        foreach (Student student in students)
        {
            GameObject studentText = Instantiate(studentTextPrefab, studentListPanel);
            studentText.GetComponent<StudentInfo>().txt_1.text = student.Id.ToString();
            studentText.GetComponent<StudentInfo>().txt_2.text = student.Name;
            studentText.GetComponent<StudentInfo>().txt_3.text = student.Age.ToString();
            studentText.GetComponent<StudentInfo>().txt_4.text = student.Sex ? "Male" : "Female"; // Display gender
            studentText.GetComponent<StudentInfo>().txt_5.text = student.Major;
            studentText.GetComponent<StudentInfo>().txt_6.text = student.Grade.ToString();
        }
        //for (int i=0; i<5; i++)
        //{
        //    GameObject studentText = Instantiate(studentTextPrefab, studentListPanel);
        //    studentText.GetComponent<StudentInfo>().txt_1.text = "abc";
        //    studentText.GetComponent<StudentInfo>().txt_2.text = "123";
        //    studentText.GetComponent<StudentInfo>().txt_3.text = "1234";
        //    studentText.GetComponent<StudentInfo>().txt_4.text = "21345";
        //    studentText.GetComponent<StudentInfo>().txt_5.text = "123456";
        //}
    }
}
