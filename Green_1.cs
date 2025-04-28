using System;
using System.Linq;

namespace Lab_7
{
    public class Green_1
    {
        public abstract class Participant
        {
            private string _surname;
            private string _group;
            private string _trainer;
            private double _result;

            protected double standart;
            private static int passedCount;

            static Participant()
            {
                passedCount = 0;
            }

            protected Participant(string surname, string group, string trainer)
            {
                _surname = surname;
                _group = group;
                _trainer = trainer;
                _result = 0;
            }

            public string Surname { get { return _surname; } }
            public string Group { get { return _group; } }
            public string Trainer { get { return _trainer; } }
            public double Result { get { return _result; } }
            public static int PassedTheStandard { get { return passedCount; } }
            public bool HasPassed { get { return _result > 0 && _result <= standart; } }

            public void Run(double time)
            {
                if (_result == 0)
                {
                    _result = time;
                    if (HasPassed)
                    {
                        passedCount++;
                    }
                }
            }

            public static Participant[] GetTrainerParticipants(Participant[] participants, Type participantType, string trainer)
            {
                return participants.Where(p => p.GetType() == participantType && p.Trainer == trainer).ToArray();
            }

            public void Print()
            {
                Console.WriteLine($"Фамилия: {Surname}");
                Console.WriteLine($"Группа: {Group}");
                Console.WriteLine($"Тренер: {Trainer}");
                Console.WriteLine($"Результат: {Result}");
                Console.WriteLine($"Прошел: {HasPassed}");
            }
        }

        public class Participant100M : Participant
        {
            public Participant100M(string surname, string group, string trainer) : base(surname, group, trainer)
            {
                standart = 12;
            }
        }

        public class Participant500M : Participant
        {
            public Participant500M(string surname, string group, string trainer) : base(surname, group, trainer)
            {
                standart = 90;
            }
        }
    }
}