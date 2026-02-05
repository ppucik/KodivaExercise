# Kodiva Exercise 19

Write a program that prints the numbers from **1 to 100** to the console, each on a new line, with the following conditions:

* If the number is divisible by **2**, it prints `"foo"` instead.
* If the number is divisible by **4**, it prints `"fuu"` instead.

> **Example output:** > `1`, `foo`, `3`, `foofuu`, `5`, ...

## 🎯 Focus Areas

The quality of the code is just as important as solving the problem itself. Focus on these key principles:

* **Maintainability:** Write readable code, use comments with real value, and follow **DRY** (Don't Repeat Yourself) and **SOLID** principles.
* **Testability:** Ensure the code is easily testable with a code coverage of at least **35%**.
* **Flexibility:** Design the system to handle potential specification changes, such as:
    * Variable size of the input set.
    * Additional conditions/rules.
    * Different output devices (Console, File, etc.).

### Instructions:
Include clear instructions on how to run your program. You can use any external libraries needed to complete the task.

---

## 🚀 Getting Started

### Prerequisites
* .NET 10 SDK

### How to Run

1.  **Via CLI:**
    ```bash
    dotnet run --project KodivaFooFuuApp.csproj
    dotnet run --project KodivaFooFuuApp.csproj -s 10 -e 20 -o File
    ```

1.  **Running Tests**
    ```bash
    dotnet test
    ```











### CPM

> dotnet tool install CentralisedPackageConverter --global

> central-pkg-converter ./