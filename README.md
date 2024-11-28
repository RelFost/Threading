
# RThread: Extended Threading with Timeout and Callbacks

**RThread** is a lightweight library built on top of the standard .NET threading model. It provides additional features, such as timeouts, success, timeout, and error callbacks, while maintaining compatibility with the familiar `Thread` interface. With **RThread**, you can easily manage advanced thread control without compromising simplicity.

---

## **Features**
- Simple timeout management for threads.
- Callback support for:
  - **Timeout**: Triggered if the thread exceeds the set timeout.
  - **Success**: Triggered when the thread completes successfully.
  - **Error**: Triggered when an exception occurs in the thread.
- Fully compatible with the standard `Thread` interface.
- Easy migration by replacing `Thread` with `RThread`.

---

## **Usage**

Replace the standard `Thread` with `RThread` to enable extended functionality. Here's an example of how to use it:

### **Basic Example**
```csharp
using System;
using Relfost.Support.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Create a thread with advanced features
        RThread thread = new RThread(CountNumbers);

        // Make the thread a background process
        thread.IsBackground = true;

        // Set a timeout with a callback
        thread.Timedout(10, OnTimeout);

        // Set a success callback
        thread.OnSuccess(OnSuccess);

        // Set an error callback
        thread.OnError(OnError);

        // Start the thread
        thread.Start();

        Console.WriteLine("Main thread continues execution...");
    }

    // A simple method for counting numbers
    static void CountNumbers()
    {
        for (int i = 1; i <= 20; i++)
        {
            Console.WriteLine($"Counting: {i}");
            Thread.Sleep(1000); // Simulate work
        }
    }

    // Callback for timeout
    static void OnTimeout()
    {
        Console.WriteLine("Thread terminated due to timeout.");
    }

    // Callback for success
    static void OnSuccess()
    {
        Console.WriteLine("Thread completed successfully.");
    }

    // Callback for error
    static void OnError(Exception ex)
    {
        Console.WriteLine($"Thread encountered an error: {ex.Message}");
    }
}
```

### **Expected Output**
#### If the thread completes successfully:
```
Counting: 1
Counting: 2
...
Counting: 20
Thread completed successfully.
Main thread continues execution...
```

#### If the thread times out:
```
Counting: 1
Counting: 2
...
Counting: 10
Thread terminated due to timeout.
Main thread continues execution...
```

#### If an exception occurs in the thread:
```
Thread encountered an error: Exception details here
Main thread continues execution...
```

---

## **Why Use RThread?**
- **Simplicity:** Drop-in replacement for `Thread`.
- **Control:** Manage thread timeouts without additional logic.
- **Callbacks:** Handle thread lifecycle events (success, timeout, and error).
- **Compatibility:** Works seamlessly with existing .NET threading.

---

## **License**
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---

## **Contributing**
Contributions are welcome! Please feel free to submit issues, feature requests, or pull requests.

---

## **Contact**
For support or inquiries, please contact: **github@relfost.com**
