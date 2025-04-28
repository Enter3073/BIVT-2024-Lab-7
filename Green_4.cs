using System;
using System.Linq;

namespace Lab_7
{
    public class Green_4
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private double[] _results;

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _results = new double[3];
            }

            public string Name => _name;
            public string Surname => _surname;
            public double[] Jumps
            {
                get
                {
                    if (_results == null) return null;
                    double[] copy = new double[_results.Length];
                    Array.Copy(_results, copy, _results.Length);
                    return copy;
                }
            }

            public double BestJump => _results?.Max() ?? 0;

            public void Jump(double distance)
            {
                if (_results == null) return;
                for (int i = 0; i < _results.Length; i++)
                {
                    if (_results[i] == 0)
                    {
                        _results[i] = distance;
                        return;
                    }
                }
            }

            public static void Sort(Participant[] participants)
            {
                if (participants == null || participants.Length == 0) return;

                for (int i = 1; i < participants.Length; i++)
                {
                    Participant key = participants[i];
                    int j = i - 1;
                    while (j >= 0 && participants[j].BestJump < key.BestJump)
                    {
                        participants[j + 1] = participants[j];
                        j--;
                    }
                    participants[j + 1] = key;
                }
            }

            public void Print()
            {
                Console.WriteLine($"Имя: {Name}, Фамилия: {Surname}, Лучший результат: {BestJump}");
            }
        }

        public abstract class Discipline
        {
            private string _name;
            private Participant[] _participants;

            public string Name => _name;
            public Participant[] Participants => _participants;

            protected Discipline(string name)
            {
                _name = name;
                _participants = Array.Empty<Participant>();
            }

            public void Add(Participant participant)
            {
                Participant[] temp = new Participant[_participants.Length + 1];
                for (int i = 0; i < _participants.Length; i++)
                {
                    temp[i] = _participants[i];
                }
                temp[temp.Length - 1] = participant;
                _participants = temp;
            }

            public void Add(Participant[] newParticipants)
            {
                if (newParticipants == null) return;
                int oldLength = _participants.Length;
                Array.Resize(ref _participants, oldLength + newParticipants.Length);
                for (int i = 0; i < newParticipants.Length; i++)
                {
                    _participants[oldLength + i] = newParticipants[i];
                }
            }

            public void Sort()
            {
                Participant.Sort(_participants);
            }

            public abstract void Retry(int index);

            public void Print()
            {
                Console.WriteLine($"Дисциплина: {Name}");
                Console.WriteLine("Участники:");
                foreach (var participant in _participants)
                {
                    participant.Print();
                }
            }
        }

        public class LongJump : Discipline
        {
            public LongJump() : base("Long jump") { }

            public override void Retry(int index)
            {
                if (index < 0 || index >= Participants.Length) return;

                Participant current = Participants[index];
                double best = current.BestJump;

                Participant updated = new Participant(current.Name, current.Surname);
                updated.Jump(best);
                updated.Jump(0);
                updated.Jump(0);

                Participants[index] = updated;
            }
        }

        public class HighJump : Discipline
        {
            public HighJump() : base("High jump") { }

            public override void Retry(int index)
            {
                if (index < 0 || index >= Participants.Length) return;

                Participant current = Participants[index];
                double[] jumps = current.Jumps;
                if (jumps == null || jumps.Length == 0) return;

                for (int i = jumps.Length - 1; i >= 0; i--)
                {
                    if (jumps[i] != 0)
                    {
                        jumps[i] = 0;
                        break;
                    }
                }

                Participant updated = new Participant(current.Name, current.Surname);
                foreach (var jump in jumps)
                {
                    if (jump != 0)
                        updated.Jump(jump);
                }

                Participants[index] = updated;
            }
        }
    }
}
