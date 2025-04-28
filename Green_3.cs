using System;
using System.Linq;

namespace Lab_7
{
    public class Green_3
    {
        public class Student
        {
            private static int _nextID;
            private readonly int _studentID;
            private string _firstName;
            private string _lastName;
            private int[] _grades;
            private int _examsCount;
            private bool _expelled;

            static Student()
            {
                _nextID = 1;
            }

            public Student(string firstName, string lastName)
            {
                _firstName = firstName;
                _lastName = lastName;
                _grades = new int[3];
                _examsCount = 0;
                _expelled = false;
                _studentID = _nextID++;
            }

            public int ID => _studentID;
            public string Name => _firstName;
            public string Surname => _lastName;

            public int[] Marks
            {
                get
                {
                    if (_grades == null) return null;
                    int[] copy = new int[_grades.Length];
                    Array.Copy(_grades, copy, _grades.Length);
                    return copy;
                }
            }

            public double AvgMark
            {
                get
                {
                    if (_grades == null || _examsCount == 0) return 0;
                    double total = 0;
                    for (int i = 0; i < _examsCount; i++)
                    {
                        total += _grades[i];
                    }
                    return total / _examsCount;
                }
            }

            public bool IsExpelled => _expelled;

            public void Exam(int mark)
            {
                if (_expelled) return;
                if (mark < 2 || mark > 5) return;
                if (_examsCount >= _grades.Length) return;

                _grades[_examsCount++] = mark;
                if (mark == 2)
                {
                    _expelled = true;
                }
            }

            public void Restore()
            {
                if (_expelled)
                {
                    _expelled = false;
                }
            }

            public void Print()
            {
                Console.WriteLine($"Имя: {Name}, Фамилия: {Surname}, Средний балл: {AvgMark:F2}, Исключен: {IsExpelled}");
            }
        }

        public class Commission
        {
            public static void Sort(Student[] students)
            {
                if (students == null || students.Length == 0) return;

                for (int i = 1; i < students.Length; i++)
                {
                    var current = students[i];
                    int j = i - 1;
                    while (j >= 0 && students[j].ID > current.ID)
                    {
                        students[j + 1] = students[j];
                        j--;
                    }
                    students[j + 1] = current;
                }
            }

            public static Student[] Expel(ref Student[] students)
            {
                var expelled = students.Where(s => s.IsExpelled).ToArray();
                students = students.Where(s => !s.IsExpelled).ToArray();
                return expelled;
            }

            public static void Restore(ref Student[] students, Student restored)
            {
                if (!students.Any(s => s.ID == restored.ID)) return;

                restored.Restore();

                if (students.Any(s => s.ID == restored.ID && !s.IsExpelled)) return;

                var updatedList = students.Where(s => s.ID != restored.ID).ToList();
                updatedList.Add(restored);
                students = updatedList.OrderBy(s => s.ID).ToArray();
            }
        }
    }
}
