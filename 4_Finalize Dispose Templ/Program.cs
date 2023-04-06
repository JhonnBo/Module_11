namespace _4_Finalize_Dispose_Templ

{
    // В классе-упаковщике ресурсов следует обеспечить каждый метод дополнительной логикой, 
    // которая бы гласила: "если объект освобожден, ничего не делать, а просто вернуть управление".
    public class MyResourceWrapper : IDisposable
    {
        // Используется для выяснения того, вызывался ли уже метод Dispose()
        private bool disposed = false;

        public void Dispose()
        {
            // Вызов вспомогательного метода.
            // Значение true указывает на то, что очистка
            // была инициирована пользователем объекта.
            Cleanup(true);
            // Подавление финализации.
            // GC.SuppressFinalize не позволяет системе выполнить вызвать метод Finalize для данного объекта.
            GC.SuppressFinalize(this);
            Console.WriteLine("Освобождение ресурсов объекта! Dispose\n");
            Console.Beep();
        }

        protected virtual void Cleanup(bool disposing)
        {
            // Проверка, выполнялась ли очистка
            if (!this.disposed)
            {
                // Если disposing равно true, должно осуществляться
                // освобождение всех управляемых ресурсов
                if (disposing)
                {
                    // Здесь осуществляется освобождение управляемых ресурсов,
                    // т.е. вызов Dispose() для управляемых объектов.
                    // При вызове деструктора в качестве параметра disposing передается значение false, 
                    // чтобы избежать очистки управляемых ресурсов, так как мы не можем быть уверенными 
                    // в их состоянии, что они до сих пор находятся в памяти. 
                    // И в этом случае остается полагаться на деструкторы этих ресурсов.
                }
                // Очистка неуправляемых ресурсов.
            }
            disposed = true;
        }

        // Деструктор следует реализовывать только у тех объектов, которым он действительно необходим, 
        // так как метод Finalize оказывает сильное влияние на производительность
        ~MyResourceWrapper()
        {
            // Вызов вспомогательного метода.
            // Значение false указывает на то, что
            // очистка была инициирована сборщиком мусора.
            Cleanup(false);
            Console.WriteLine("Освобождение ресурсов объекта! Финализатор\n");
            Console.Beep();
        }
    }

    // При создании производных классов от базовых, которые реализуют интерфейс IDisposable, 
    // следует также вызывать метод Dispose базового класса.
    public class Derived : MyResourceWrapper
    {
        private bool IsDisposed = false;

        protected override void Cleanup(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    // Освобождение управляемых ресурсов
                }
                IsDisposed = true;
            }
            // Обращение к методу Dispose базового класса
            base.Cleanup(disposing);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            CreateAndDisposeObjs();
            GC.Collect();
            Console.Read();
        }

        static void CreateAndDisposeObjs()
        {
            MyResourceWrapper[] obj = new MyResourceWrapper[30];
            for (int i = 0; i < 30; i++)
                obj[i] = new MyResourceWrapper();
            for (int i = 0; i < 15; i++)
                obj[i].Dispose();
        }
    }
}