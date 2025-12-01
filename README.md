# 🛒 Amazon QA Automation Framework

![.NET](https://img.shields.io/badge/.NET-8.0-purple)
![Selenium](https://img.shields.io/badge/Selenium-WebDriver-green)
![SpecFlow](https://img.shields.io/badge/SpecFlow-BDD-orange)

## 🚀 Tech Stack

* **Language:** C# (.NET 8.0)
* **Core Framework:** Selenium WebDriver
* **BDD Framework:** SpecFlow (Gherkin syntax)
* **Test Runner:** NUnit
* **Reporting:** Allure Report
* **Browser Management:** WebDriverManager
* **CI/CD:** GitHub Actions (Headless Chrome execution)

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