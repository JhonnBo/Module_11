namespace _3_Dispose_By_using
{
    // В .NET существует два типа ресурсов: управляемые и неуправляемые. 
    // К неуправляемым ресурсам относятся только «сырые» ресурсы, типа IntPtr, дескрипторы сокетов или файлов.
    // Если же этот ресурс упаковали в объект, захватывающий его в конструкторе и освобождающий в методе Dispose, 
    // то такой ресурс уже является управляемым. 
    // По сути, управляемые ресурсы – это «умные оболочки» для неуправляемых ресурсов, для освобождения которых 
    // не нужно вызывать какие-то функции, а достаточно вызвать метод Dispose интерфейса IDisposable


    class FinalizeObject : IDisposable
    {
        // Данный класс реализует интерфейс IDisposable
        // Интерфейс IDisposable объявляет один единственный метод Dispose,
        // в котором при реализации интерфейса в классе должно
        // происходить освобождение неуправляемых ресурсов.
        FileStream file;
        StreamWriter writer;
        public int id { get; set; }

        public FinalizeObject(int id)
        {
            this.id = id;
            file = new FileStream("files\\" + id + ".txt", FileMode.Create, FileAccess.Write);
            writer = new StreamWriter(file);
            writer.Write("IDisposable using");
        }

        // Реализуем метод Dispose()
        public void Dispose()
        {
            Console.WriteLine("Освобождение ресурсов объекта!");
            writer.Close();
            file.Close();
            // Освобождаем управляемые и неуправляемые ресурсы
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            FinalizeObject[] obj = new FinalizeObject[30];
            for (int i = 0; i < 3; i++)
                //Конструкция using оформляет блок кода и создает объект некоторого типа,
                //который реализует интерфейс IDisposable, в частности, его метод Dispose.
                //При завершении блока кода у объекта вызывается метод Dispose.
                //Важно, что данная конструкция применяется только для типов,
                //которые реализуют интерфейс IDisposable.
                using (obj[i] = new FinalizeObject(i))
                {
                    // Выполнение действий на объектами
                }

        }
    }
}