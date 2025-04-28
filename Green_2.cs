using System;

namespace Lab_7
{
    public class Green_2
    {
        public class Human
        {
            private string _name;
            private string _surname;

            public Human(string name, string surname)
            {
                _name = name;
                _surname = surname;
            }

            public string Name => _name;
            public string Surname => _surname;

            public void Print()
            {
                Console.WriteLine($"Имя: {Name}, Фамилия: {Surname}");
            }
        }

        public class Student : Human
        {
            private int[] _grades;
            private int _currentCount;
            private static int _totalExcellentStudents;
            private bool _countedAsExcellent;

            public Student(string name, string surname) : base(name, surname)
            {
                _grades = new int[4];
                _currentCount = 0;
                _countedAsExcellent = false;
            }

            public static int ExcellentAmount => _totalExcellentStudents;

            public int[] Marks
            {
                get
                {
                    int[] result = new int[_currentCount];
                    Array.Copy(_grades, result, _currentCount);
                    return result;
                }
            }

            public double AvgMark
            {
                get
                {
                    if (_currentCount < 4) return 0;
                    double sum = 0;
                    foreach (var grade in _grades)
                    {
                        sum += grade;
                    }
                    return sum / _grades.Length;
                }
            }

            public bool IsExcellent
            {
                get
                {
                    if (_currentCount < 4) return false;
                    foreach (var grade in _grades)
                    {
                        if (grade < 4) return false;
                    }
                    if (!_countedAsExcellent)
                    {
                        _totalExcellentStudents++;
                        _countedAsExcellent = true;
                    }
                    return true;
                }
            }

            public void Exam(int mark)
            {
                if (_currentCount < _grades.Length && mark >= 2 && mark <= 5)
                {
                    _grades[_currentCount] = mark;
                    _currentCount++;
                }
            }

            public static void SortByAvgMark(Student[] students)
            {
                if (students == null || students.Length == 0) return;
                int length = students.Length;
                for (int i = 0; i < length - 1; i++)
                {
                    for (int j = 0; j < length - i - 1; j++)
                    {
                        if (students[j].AvgMark < students[j + 1].AvgMark)
                        {
                            Student temp = students[j];
                            students[j] = students[j + 1];
                            students[j + 1] = temp;
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"Имя: {Name}, Фамилия: {Surname}, Средний балл: {AvgMark}, Отличники: {IsExcellent}");
            }
        }
    }
}