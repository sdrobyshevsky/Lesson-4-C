// Урок 4. GOF: паттерны проектирования в .Net разработке
// Структурируйте код клиента и сервера чата, используя знания о шаблонах.

// Пример структурированного кода клиента и сервера чата с использованием паттерна наблюдатель (Observer) в .NET

// Интерфейс для наблюдателей (подписчиков)
public interface IObserver
{
    void Update(string message);
}

// Класс конкретного наблюдателя (подписчика), который представляет клиента
public class ChatClient : IObserver
{
    private string name;

    public ChatClient(string name)
    {
        this.name = name;
    }

    public void Update(string message)
    {
        Console.WriteLine($"{name} received message: {message}");
    }

    public void SendMessage(string message, ChatServer server)
    {
        server.SendMessage(message, this);
    }
}

// Интерфейс для наблюдаемого объекта, который является сервером чата
public interface IObservable
{
    void AddObserver(IObserver observer);
    void RemoveObserver(IObserver observer);
    void NotifyObservers(string message);
}

// Класс сервера чата, который реализует интерфейс наблюдаемого объекта
public class ChatServer : IObservable
{
    private List<IObserver> observers = new List<IObserver>();

    public void AddObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void NotifyObservers(string message)
    {
        foreach (var observer in observers)
        {
            observer.Update(message);
        }
    }

    public void SendMessage(string message, IObserver sender)
    {
        Console.WriteLine($"{sender} sent message: {message}");
        NotifyObservers(message);
    }
}

// Пример использования
class Program
{
    static void Main(string[] args)
    {
        ChatServer server = new ChatServer();

        ChatClient client1 = new ChatClient("Alice");
        ChatClient client2 = new ChatClient("Bob");

        server.AddObserver(client1);
        server.AddObserver(client2);

        client1.SendMessage("Hello, Bob!", server);
    }
}

