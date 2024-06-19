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
    public InputField deleteIdInputField;
    public InputField searchNameInputField;

    //public InputField searchInputField;
    //public Button searchButton;

    public Transform studentListPanel; //Panel chua danh sach SV 
    public GameObject studentTextPrefab; //Prefab cho moi SV
    public Text errorText; // Add an errorText UI element to display error messages

    private StudentManager studentManager;

    void Start()
    {
        studentManager = new StudentManager();
        //DisplayAllStudents();

    }

    public void AddStudent()
    {
        if (ValidateInputs(out int id, out int age, out float grade))
        {
            if (studentManager.GetStudentById(id) != null)
            {
                DisplayError("Student with this ID already exists.");
                return;
            }

            string name = nameInputField.text;
            bool sex = toggleMale.isOn;
            string major = majorInputField.text;

            //Student newStudent = new Student(id, name, age, sex, major, grade);
            Student newStudent = new(id, name, age, sex, major, grade);
            studentManager.AddStudent(newStudent);

            DisplayAllStudents();
            ClearInputs();
        }
    }

    public void DisplayAllStudents()
    {
        DisplayStudents(studentManager.GetAllStudents());
    }

    //public void RemoveStudent()
    //{
    //    if (int.TryParse(deleteIdInputField.text, out int id))
    //    {
    //        studentManager.RemoveStudent(id);
    //        DisplayAllStudents();
    //        ClearInputs();
    //    }
    //    else
    //    {
    //        DisplayError("Invalid ID format.");
    //    }
    //}
    public void RemoveStudent()
    {
        if (int.TryParse(deleteIdInputField.text, out int id))
        {
            bool isRemoved = studentManager.RemoveStudent(id);

            if (isRemoved)
            {
                DisplayAllStudents();
                ClearInputs();
            }
            else
            {
                DisplayError("Student not found.");
            }
        }
        else
        {
            DisplayError("Invalid ID format.");
        }
    }

    public void UpdateStudent()
    {
        if(!int.TryParse(idInputField.text, out int id))
        {
            DisplayError("Invalid ID format.");
            return;
        }
        //int id = int.Parse(idInputField.text);
        Student existingStudent = studentManager.GetStudentById(id);

        if (existingStudent == null)
        {
            Debug.LogWarning("Student not found with ID: " + id);
            return;
        }

        // Update only the fields that were provided in the input fields
        if (!string.IsNullOrEmpty(nameInputField.text))
        {
            existingStudent.Name = nameInputField.text;
        }

        if (!string.IsNullOrEmpty(ageInputField.text))
        {
            existingStudent.Age = int.Parse(ageInputField.text);
        }

        existingStudent.Sex = toggleMale.isOn; // Assuming you have a toggle for Male/Female
        existingStudent.Major = majorInputField.text;

        if (!string.IsNullOrEmpty(gradeInputField.text))
        {
            existingStudent.Grade = float.Parse(gradeInputField.text);
        }

        // Call the UpdateStudent method of StudentManager
        studentManager.UpdateStudent(existingStudent);

        // Display updated student list
        DisplayAllStudents();
        //ClearInputs();
    }

    public void SearchStudent()
    {
        string searchName = searchNameInputField.text;
        List<Student> students = studentManager.SearchStudentsByName(searchName);

        if (students.Count == 0)
        {
            DisplayError("No students found with the given name.");
            Debug.Log("Khong tim thay SV can xoa");
        }
        else
        {
            DisplayStudents(students);
        }
    }

    private bool ValidateInputs(out int id, out int age, out float grade)
    {
        id = 0;
        age = 0;
        grade = 0f;
        StringBuilder errorMessage = new StringBuilder();

        if (!int.TryParse(idInputField.text, out id))
        {
            errorMessage.AppendLine("Invalid ID format.");
        }

        if (!int.TryParse(ageInputField.text, out age))
        {
            errorMessage.AppendLine("Invalid age format.");
        }

        if (!float.TryParse(gradeInputField.text, out grade))
        {
            errorMessage.AppendLine("Invalid grade format.");
        }

        if (errorMessage.Length > 0)
        {
            DisplayError(errorMessage.ToString());
            return false;
        }

        return true;
    }

    private void DisplayError(string message)
    {
        if (errorText != null)
        {
            errorText.text = message;
        }
    }

    private void ClearInputs()
    {
        deleteIdInputField.text = string.Empty;
        idInputField.text = "";
        nameInputField.text = "";
        ageInputField.text = "";
        toggleMale.isOn = true;
        majorInputField.text = "";
        gradeInputField.text = "";
        if (errorText != null)
        {
            errorText.text = "";
        }
    }

    public void DisplayStudents(List<Student> students)
    {
        //clear previous list
        foreach (Transform child in studentListPanel)
        {
            Destroy(child.gameObject);
        }

        //List<Student> students = studentManager.GetAllStudents();
        foreach (Student student in students)
        {
            GameObject studentText = Instantiate(studentTextPrefab, studentListPanel);
            //StudentInfo studentInfo = studentText.GetComponent<StudentInfo>();

            studentText.GetComponent<StudentInfo>().txt_1.text = student.Id.ToString();
            studentText.GetComponent<StudentInfo>().txt_2.text = student.Name;
            studentText.GetComponent<StudentInfo>().txt_3.text = student.Age.ToString();
            studentText.GetComponent<StudentInfo>().txt_4.text = student.Sex ? "Male" : "Female"; // Display gender
            studentText.GetComponent<StudentInfo>().txt_5.text = student.Major;
            studentText.GetComponent<StudentInfo>().txt_6.text = student.Grade.ToString();
        }
        
    }
}
