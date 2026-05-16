using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class TaskItem
{
    public string Title { get; set; }
    public bool IsCompleted { get; set; }
}

class Program
{
    static List<TaskItem> tasks = new List<TaskItem>();

    static string fileName = "tasks.json";

    static void Main()
    {
        LoadTasks();

        while (true)
        {
            Console.WriteLine("\n--- TO DO LIST ---");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. View Tasks");
            Console.WriteLine("3. Mark Complete");
            Console.WriteLine("4. Delete Task");
            Console.WriteLine("5. Exit");

            Console.Write("Choose option: ");

            int choice;

            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Enter valid number");
                continue;
            }

            switch (choice)
            {
                case 1:
                    AddTask();
                    break;

                case 2:
                    ViewTasks();
                    break;

                case 3:
                    MarkComplete();
                    break;

                case 4:
                    DeleteTask();
                    break;

                case 5:
                    return;

                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }
    }

    static void AddTask()
    {
        Console.Write("Enter task: ");

        string title = Console.ReadLine();

        tasks.Add(new TaskItem
        {
            Title = title,
            IsCompleted = false
        });

        SaveTasks();

        Console.WriteLine("Task Added!");
    }

    static void ViewTasks()
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks available.");
            return;
        }

        for (int i = 0; i < tasks.Count; i++)
        {
            string status = tasks[i].IsCompleted ? "[Done]" : "[Pending]";

            Console.WriteLine($"{i + 1}. {status} {tasks[i].Title}");
        }
    }

    static void MarkComplete()
    {
        ViewTasks();

        Console.Write("Enter task number: ");

        int index = Convert.ToInt32(Console.ReadLine()) - 1;

        if (index >= 0 && index < tasks.Count)
        {
            tasks[index].IsCompleted = true;

            SaveTasks();

            Console.WriteLine("Task completed!");
        }
        else
        {
            Console.WriteLine("Invalid task number.");
        }
    }

    static void DeleteTask()
    {
        ViewTasks();

        Console.Write("Enter task number to delete: ");

        int index = Convert.ToInt32(Console.ReadLine()) - 1;

        if (index >= 0 && index < tasks.Count)
        {
            tasks.RemoveAt(index);

            SaveTasks();

            Console.WriteLine("Task deleted!");
        }
        else
        {
            Console.WriteLine("Invalid task number.");
        }
    }

    static void SaveTasks()
    {
        string json = JsonSerializer.Serialize(tasks);

        File.WriteAllText(fileName, json);
    }

    static void LoadTasks()
    {
        if (File.Exists(fileName))
        {
            string json = File.ReadAllText(fileName);

            tasks = JsonSerializer.Deserialize<List<TaskItem>>(json);
        }
    }
}