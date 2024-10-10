using System;


namespace Lab1_Constructors
{
    public abstract class Instrument : IDisposable
    {
        private string material;
        private bool isTuned;
        static private string owner;
        private bool disposed;

        static Instrument()
        {
            owner = "Maryna Polishchuk";
        }
        public Instrument() // дефолтний конструктор
        {
            this.material = "Unknown";
            this.isTuned = false;
            Console.WriteLine("Default Instrument constructor is working");
        }
        public Instrument(string material, bool isTuned) // параметризований конструктор
        {
            this.material = material;
            this.isTuned = isTuned;
            Console.WriteLine("Parameters Instrument constructor is working");
        }
        public virtual void Play() // віртуальна функція
        {
            Console.WriteLine("Can be overridden");
        }
        public bool IsTurned
        {
            get { return this.isTuned; }
            set
            {
                if (value)
                {

                    this.isTuned = true;
                    Console.WriteLine("instrument needn't to tuning");
                }
                else
                {
                    this.isTuned = false;
                    Console.WriteLine("instrument need to tuning");

                }
            }
        }
        public string Material
        {
            get { return material; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    material = value;
                    Console.WriteLine($"Material set to: {material}");
                }
                else
                {
                    Console.WriteLine("Invalid material value.");
                }
            }
        }

        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed) // Перевірка, чи вже був об'єкт звільнений
            {
                if (disposing)
                {
                    Console.WriteLine("Disposing managed resourсes"); // Звільнення керованих ресурсів
                }
                Console.WriteLine("Disposing unmanaged resourсes"); // Звільнення некерованих ресурсів
                disposed = true; // Позначаємо, що ресурси вже звільнені
            }
        }

        ~Instrument()
        {
            Dispose(false);
            Console.WriteLine("Instrument destructor is used");
        }
     


    }
    public class PercussionInstrument : Instrument // ударні інстурменти 
    {
        int percussionInstrumentNumber = 0;
        public PercussionInstrument() // дефолтний конструктор
        {
            Console.WriteLine("Default constructor PercussionInstrument is working");
        }
        public PercussionInstrument(string material, bool isTurned) : base(material, isTurned) // параметризований конструктор
        {
            Console.WriteLine("Parameters constructor PercussionInstrument is working");
        }
        ~PercussionInstrument()
        {
            Dispose(false);
            Console.WriteLine("PercussionInstrument destructor is used");
        }
    }

    public class Drum:PercussionInstrument // барабан наслідується від ударних
    {
        int drumInstrumentNumber = 0;
        public Drum() // дефолтний конструктор
        {
            Console.WriteLine("Default constructor Drum is working");
        }
        public Drum(string material, bool isTurned) : base(material, isTurned) //  параметризований
        {
            Console.WriteLine("Parameters constructor Drum is working");
        }
        public override void Play()  
        {
            Console.WriteLine("Playing the Drum");
        }
        ~Drum()
        {
            Dispose(false);
            Console.WriteLine("Drum destructor is used");
        }
    }
    public class SpecificDrum : Drum // специфічний барабан наслідується від барабанів
    {
        private static SpecificDrum instance; 
        private static readonly long validId = 561935830;
        private long id;

        private SpecificDrum(long id) // приватний конструктор
        {
            this.id = id;
            Console.WriteLine("SpecificDrum constructor is working");
        }


        public static SpecificDrum GetInstance(long id) // публічний статичний конструктор
        {
            if (instance == null && id == validId)
            {
                instance = new SpecificDrum(id); 
            }
            else if (instance != null)
            {
                Console.WriteLine("Instance already exists.");
            }
            else
            {
                Console.WriteLine("Invalid ID. Cannot create instance.");
            }

            return instance;
        }

        public override void Play()
        {
            Console.WriteLine("Playing the Specific Drum");
        }
        ~SpecificDrum()
        {
            Dispose(false);
            Console.WriteLine("SpecificDruml destructor is used");
        }
    }


    public class StringInstrument : Instrument // струнні
    {
        public int numberOfStrings;
        public StringInstrument(int numberOfStrings) 
        {
            this.numberOfStrings = numberOfStrings;
            Console.WriteLine("Constructor with 1 parameter constructor StringInstrument is working");
        }
        public StringInstrument(string material, bool isTurned, int numberOfStrings) : base(material, isTurned)
        {
            Console.WriteLine("Parameters constructor StringInstrument is working");
            this.numberOfStrings = numberOfStrings;
        }
        ~StringInstrument()
        {
            Dispose(false);
            Console.WriteLine("StringInstrument destructor is used");
        }

    }
    public class Piano : StringInstrument // фортепіано наслідується від струнних
    {
        int numberOfPiano;
        public Piano() :base(88) 
        {
            Console.WriteLine("Default constructor Piano is working");
        }
        public Piano(string material, bool isTerned, int numberOfStrings) : base(material, isTerned, numberOfStrings)
        {
            Console.WriteLine("Parameters constructor Piano is working");
        }

        public override void Play()
        {
            Console.WriteLine("Playing the Piano");
        }
        ~Piano()
        {
            Dispose(false);
            Console.WriteLine("Piano destructor is used");
        }
    }
    public class Guitar : StringInstrument
    {
        private bool isElectric;
        public bool IsElectric
        {
            get { return isElectric; }
            set
            {
                isElectric = value;
                Console.WriteLine(isElectric ? "This is an electric guitar." : "This is an acoustic guitar.");
            }
        }
        public Guitar() : base(6) 
        {
            Console.WriteLine("Default constructor Guitar is working");
        }
        public Guitar(string material, bool isTerned, int numberOfStrings, bool isElectric) : base(material, isTerned, numberOfStrings)
        {
            Console.WriteLine("Parameters constructor Guitar is working");
            this.isElectric = isElectric;

        }
        public override void Play()
        {
            Console.WriteLine("Playing the Guitar");
        }
        ~Guitar()
        {
            Dispose(false);
            Console.WriteLine("Guitar destructor is used");
        }
    }
    public class InstrumentsCounter
    {
        //Приватний конструктор, щоб не можна було створити об'єкт класу
        private InstrumentsCounter() { }

        public static int currentCount;
        static InstrumentsCounter()//Для ініціалізації статичних даних
        {
            currentCount = 0;
        }

        public static int IncrementCount()
        {
            return ++currentCount;
        }
        public static void Amount()
        {
            Console.WriteLine($"There are {currentCount} instruments.");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("creating new guitar");
            //Guitar guitar = new Guitar("wood", true, 6);
            for (int i = 0; i < 15; i++)
            {
                new Piano($"{i}",true, 90);
            }
            StringInstrument seal = new StringInstrument(88);
            InstrumentsCounter.Amount();

            //Виклик GC
            Console.WriteLine($"Total memory: {GC.GetTotalMemory(false)} bytes");//Загальна кількість використаної пам'яті
            Console.WriteLine($"Generation of {seal.Material}: {GC.GetGeneration(seal)}.");
            GC.ReRegisterForFinalize(seal);
            GC.Collect();//Виклик збирання сміття
            GC.WaitForPendingFinalizers();//Очікує повне звільнення ресурсів
            Console.WriteLine($"Total memory after GC: {GC.GetTotalMemory(true)} bytes.");
            Console.WriteLine($"Generation of {seal.Material}: {GC.GetGeneration(seal)}.");


            //Примусовий виклик GC

            Console.ReadKey();

        }
    }
}
