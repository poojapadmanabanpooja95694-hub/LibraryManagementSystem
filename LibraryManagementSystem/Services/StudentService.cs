using System;
using System.Linq;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services
{
    public class StudentService
    {
        // 🔥 AUTO GENERATE STUDENT ID (S1001)
        private string GenerateStudentCode(LibraryContext context)
        {
            var last = context.Students
                .OrderByDescending(s => s.StudentCode)
                .FirstOrDefault();

            int num = 1000;

            if (last != null && last.StudentCode.Length > 1)
            {
                num = int.Parse(last.StudentCode.Substring(1));
            }

            return "S" + (num + 1);
        }

        // ✅ ADD STUDENT (AUTO CODE)
        public void AddStudent(string name, string dept, string phone, string email)
        {
            using var context = new LibraryContext();

            // 🔥 Validation
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name is required!");
                return;
            }

            if (!phone.All(char.IsDigit) || phone.Length != 10)
            {
                Console.WriteLine("Invalid phone number!");
                return;
            }

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@") || !email.Contains("."))
            {
                Console.WriteLine("Invalid email!");
                return;
            }

            string newCode = GenerateStudentCode(context);

            var student = new Student
            {
                StudentCode = newCode,
                FullName = name,
                Department = dept,
                Phone = phone,
                Email = email,
                IsActive = true,
                CreatedOn = DateTime.Now
            };

            context.Students.Add(student);
            context.SaveChanges();

            Console.WriteLine($"Student added successfully! Code: {newCode}");
        }

        // ✅ LIST STUDENTS
        public void ListStudents()
        {
            using var context = new LibraryContext();

            var students = context.Students
                .Where(s => s.IsActive)
                .ToList();

            if (!students.Any())
            {
                Console.WriteLine("No students found.");
                return;
            }

            foreach (var s in students)
            {
                Console.WriteLine("----------------------------------");
                Console.WriteLine($"Code       : {s.StudentCode}");
                Console.WriteLine($"Name       : {s.FullName}");
                Console.WriteLine($"Department : {s.Department}");
                Console.WriteLine($"Phone      : {s.Phone}");
                Console.WriteLine($"Email      : {s.Email}");
            }
        }

        // ✅ EDIT STUDENT
        public void EditStudentByCode(string code, string name, string dept, string phone, string email)
        {
            using var context = new LibraryContext();

            var student = context.Students
                .FirstOrDefault(s => s.StudentCode == code && s.IsActive);

            if (student == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            if (!phone.All(char.IsDigit) || phone.Length != 10)
            {
                Console.WriteLine("Invalid phone number!");
                return;
            }

            if (!email.Contains("@") || !email.Contains("."))
            {
                Console.WriteLine("Invalid email!");
                return;
            }

            student.FullName = name;
            student.Department = dept;
            student.Phone = phone;
            student.Email = email;

            context.SaveChanges();

            Console.WriteLine("Student updated successfully!");
        }

        // ✅ DELETE STUDENT (SOFT DELETE)
        public void DeleteStudentByCode(string code)
        {
            using var context = new LibraryContext();

            var student = context.Students
                .FirstOrDefault(s => s.StudentCode == code && s.IsActive);

            if (student == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            var hasActiveIssues = context.Issues
                .Any(i => i.StudentId == student.Id && i.Status == "Issued");

            if (hasActiveIssues)
            {
                Console.WriteLine("Cannot delete student. Books are still issued!");
                return;
            }

            student.IsActive = false;

            context.SaveChanges();

            Console.WriteLine("Student deleted successfully!");
        }
    }
}