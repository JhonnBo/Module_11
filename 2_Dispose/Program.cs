namespace _2_Dispose
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
            writer.Write("IDisposable");
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
                obj[i] = new FinalizeObject(i);
            for (int i = 0; i < 3; i++)
                obj[i].Dispose();
            Console.Read();
        }
    }

}// Реализация IDisposable: правильное использование
 // https://habr.com/ru/companies/clrium/articles/341864/