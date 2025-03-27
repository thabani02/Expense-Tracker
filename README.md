# Expense Tracker WPF Application

## Overview

The Expense Tracker is a Windows Presentation Foundation (WPF) application that helps users manage their expenses efficiently. The application allows users to add, view, and generate monthly spending reports. Data is stored in a SQL Server database, ensuring persistence and reliability.

## Features

- Add Expenses: Users can input expense details such as category and amount.

- View Expenses: Displays a list of recorded expenses in a DataGrid.

- Monthly Report: Generates a summary of expenses grouped by category for the current month.

- SQL Server Integration: Uses SQL Server to store and retrieve expense data securely.

## Technologies Used

- C# (.NET) – Backend logic and database interactions

- WPF (XAML) – User Interface design

- SQL Server – Database for storing expense records

## Prerequisites

Before running the application, ensure you have the following installed:

- Visual Studio (with .NET desktop development workload)

- SQL Server (Express or full version)

- SQL Server Management Studio (SSMS) (optional but recommended)

## Database Setup

- Open SQL Server Management Studio (SSMS).

- Run the following SQL script to create the database and stored procedures:
```
  CREATE DATABASE ExpenseTracker;
GO
USE ExpenseTracker;

CREATE TABLE Expenses (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Category NVARCHAR(255) NOT NULL,
    Amount DECIMAL(10,2) NOT NULL,
    Date DATE NOT NULL
);

GO
CREATE PROCEDURE AddExpense
    @Category NVARCHAR(255),
    @Amount DECIMAL(10,2)
AS
BEGIN
    INSERT INTO Expenses (Category, Amount, Date)
    VALUES (@Category, @Amount, GETDATE());
END;
GO
```
## Running the Application

1. Open the project in Visual Studio.
2. clone the repo
  ```
  https://github.com/thabani02/Expense-Tracker
  ```
-Ensure your connection string in MainWindow.xaml.cs is correctly set to your SQL Server instance:
```
private string connectionString = "Data Source=YOUR_SERVER_NAME;Initial Catalog=ExpenseTracker;Integrated Security=True;";
```
3. Press F5 or click Start to run the application.

4. Add expenses and generate a monthly report using the UI buttons.
