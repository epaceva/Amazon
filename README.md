# 🛒 Amazon QA Automation Framework

![.NET](https://img.shields.io/badge/.NET-8.0-purple)
![Selenium](https://img.shields.io/badge/Selenium-WebDriver-green)
![SpecFlow](https://img.shields.io/badge/SpecFlow-BDD-orange)
![Build Status](https://img.shields.io/badge/build-passing-brightgreen)

A robust automated testing framework for **Amazon.co.uk**, built with **C#**, **Selenium WebDriver**, and **SpecFlow (BDD)**. 

This project demonstrates modern QA automation practices including the **Page Object Model (POM)** design pattern, dynamic element handling, and comprehensive reporting via **Allure**.

## 🚀 Tech Stack

* **Language:** C# (.NET 8.0)
* **Core Framework:** Selenium WebDriver
* **BDD Framework:** SpecFlow (Gherkin syntax)
* **Test Runner:** NUnit
* **Reporting:** Allure Report
* **Browser Management:** WebDriverManager
* **CI/CD:** GitHub Actions (Headless Chrome execution)

## ✨ Key Features
Robust Page Objects: Handles dynamic Amazon elements (e.g., Book format swatches, Buy Box variations).

Smart Waiting: Uses explicit waits (WaitHelpers) to eliminate flakiness.

Resilience:

Auto-handles "Accept Cookies" banners.

Dismisses "Deliver to..." location popups.

Handles "Soft Bot Checks" (Continue Shopping screens).

Cross-Currency Support: Logic to handle price verification in both GBP (£) and local currencies (e.g., BGN) based on IP location.

CI/CD Ready: Automatically detects environment variables to switch between Headless (for GitHub Actions) and UI modes.

## 🛠️ Prerequisites
To run this project locally, ensure you have:

Visual Studio 2022 (or newer).

.NET 8.0 SDK.

Allure Commandline (Required for viewing reports).

```Bash
dotnet tool install -g allure-commandline
```

## ⚙️ Installation & Setup
Clone the repository:

```Bash
git clone https://github.com/epaceva/Amazon.git
```

Restore NuGet Packages:

```Bash
dotnet restore
```

Configuration Check: Ensure the following files have "Copy to Output Directory" set to "Copy always" in their File Properties within Visual Studio:

appsettings.json

allureConfig.json

specflow.json

## ▶️ Running Tests
Option 1: Visual Studio
Go to Test > Test Explorer.

Right-click the scenario SearchForHarryPotterBookAndVerifyExtendedDetails.

Select Run.

Option 2: Command Line (CLI)
Navigate to the project root and run:

```Bash
dotnet test
```

## 📊 Generating Reports
After execution, test results are generated in bin/Debug/net8.0/allure-results. To view the interactive HTML report:

Open your terminal in the project directory.

Run the following command:

``` Bash
allure serve Amazon/bin/Debug/net8.0/allure-results
```

## 🤖 CI/CD (GitHub Actions)
This project includes a configured workflow (.github/workflows/dotnet.yml) that:

Sets up the .NET environment.

Runs tests in Headless Chrome mode.

Generates Allure reports and uploads them as artifacts.