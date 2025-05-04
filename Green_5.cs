using System;
using System.Linq;

namespace Lab_7
{
    public class Green_5
    {
        public struct Student
        {
            private readonly string _name;
            private readonly string _surname;
            private readonly int[] _marks;

            public Student(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[5];
            }

            public string Name => _name ?? string.Empty;
            public string Surname => _surname ?? string.Empty;
            public int[] Marks => _marks?.ToArray() ?? Array.Empty<int>();

            public double AvgMark
            {
                get
                {
                    if (_marks == null || _marks.Length == 0) return 0;
                    double total = 0;
                    int count = 0;
                    foreach (var mark in _marks)
                    {
                        if (mark != 0)
                        {
                            total += mark;
                            count++;
                        }
                    }
                    return count == 0 ? 0 : total / count;
                }
            }

            public void Exam(int mark)
            {
                if (mark < 2 || mark > 5 || _marks == null) return;
                for (int i = 0; i < _marks.Length; i++)
                {
                    if (_marks[i] == 0)
                    {
                        _marks[i] = mark;
                        break;
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"Имя: {Name}, Фамилия: {Surname}, Средний балл: {AvgMark:F2}");
            }
        }

        public class Group
        {
            private readonly string _name;
            protected readonly Student[] _students;
            protected int _count; // Изменено с private на protected

            public Group(string name, int capacity = 10)
            {
                _name = name;
                _students = new Student[capacity];
                _count = 0;
            }

            public string Name => _name ?? string.Empty;
            public Student[] Students => _students ?? Array.Empty<Student>();

            public virtual double AvgMark
            {
                get
                {
                    if (_count == 0) return 0;
                    double total = 0;
                    int validStudents = 0;
                    for (int i = 0; i < _count; i++)
                    {
                        var avg = _students[i].AvgMark;
                        if (avg > 0)
                        {
                            total += avg;
                            validStudents++;
                        }
                    }
                    return validStudents == 0 ? 0 : total / validStudents;
                }
            }

            public void Add(Student student)
            {
                if (_count < _students.Length)
                {
                    _students[_count++] = student;
                }
            }

            public void Add(Student[] students)
            {
                foreach (var student in students)
                {
                    Add(student);
                }
            }

            public static void SortByAvgMark(Group[] groups)
            {
                if (groups == null || groups.Length == 0) return;

                for (int i = 1; i < groups.Length; i++)
                {
                    var key = groups[i];
                    int j = i - 1;

                    while (j >= 0 && groups[j].AvgMark < key.AvgMark)
                    {
                        groups[j + 1] = groups[j];
                        j--;
                    }
                    groups[j + 1] = key;
                }
            }

            public void Print()
            {
                Console.WriteLine($"Группа: {Name}, Средний балл: {AvgMark:F2}");
                for (int i = 0; i < _count; i++)
                {
                    _students[i].Print();
                }
            }
        }

        public class EliteGroup : Group
        {
            public EliteGroup(string name, int capacity) : base(name, capacity) { }

            public override double AvgMark
            {
                get
                {
                    if (_count == 0) return 0;
                    double totalWeightedAvg = 0;
                    int studentsWithMarks = 0;

                    for (int i = 0; i < _count; i++)
                    {
                        var marks = _students[i].Marks.Where(m => m != 0).ToArray();
                        if (marks.Length == 0) continue;

                        double weightedSum = 0;
                        foreach (var mark in marks)
                        {
                            double weight = mark switch
                            {
                                5 => 1.0,
                                4 => 1.5,
                                3 => 2.0,
                                2 => 2.5,
                                _ => 0
                            };
                            weightedSum += mark * weight;
                        }
                        totalWeightedAvg += weightedSum / marks.Length;
                        studentsWithMarks++;
                    }

                    return studentsWithMarks == 0 ? 0 : totalWeightedAvg / studentsWithMarks;
                }
            }
        }

        public class SpecialGroup : Group
        {
            public SpecialGroup(string name, int capacity) : base(name, capacity) { }

            public override double AvgMark
            {
                get
                {
                    if (_count == 0) return 0;
                    double totalWeightedAvg = 0;
                    int studentsWithMarks = 0;

                    for (int i = 0; i < _count; i++)
                    {
                        var marks = _students[i].Marks.Where(m => m != 0).ToArray();
                        if (marks.Length == 0) continue;

                        double weightedSum = 0;
                        foreach (var mark in marks)
                        {
                            double weight = mark switch
                            {
                                5 => 1.0,
                                4 => 0.75,
                                3 => 0.5,
                                2 => 0.25,
                                _ => 0
                            };
                            weightedSum += mark * weight;
                        }
                        totalWeightedAvg += weightedSum / marks.Length;
                        studentsWithMarks++;
                    }

                    return studentsWithMarks == 0 ? 0 : totalWeightedAvg / studentsWithMarks;
                }
            }
        }
    }
}