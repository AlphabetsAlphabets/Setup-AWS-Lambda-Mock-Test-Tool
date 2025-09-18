# Missing file issue (IMPORTANT!!!!!)
The repository will not include the AccessDetails file because it contains secret keys for my AWS account. Find a way to include your AWS keys

# Instructions
Instructions on how to run the lambda mock test tool for Visual Studio Code. If you use other IDEs such as  JetBrains rider, detailed instructions can be found [here](https://github.com/aws/aws-lambda-dotnet/tree/master/Tools/LambdaTestTool) which is the official repository by AWS, FYI the instructions here are the super simplified version in the repo as well. Basically
1. Create the project with the relavant template.

These templates are not available by default, which means you'll need to download them then generate a new dotnet project using those templates. Get templates with:

```
dotnet new install Amazon.Lambda.Templates::7.5.0
```

This is a nuget package, click on [this](https://www.nuget.org/packages/Amazon.Lambda.Templates) link to go there and download the latest version.

List all available templates with

```
dotnet new list
```

To add a filter for lambda specifically run `dotnet new list --tag lambda`. Then create the project. This specific sample is created with this command:

```
dotnet new lambda.EmptyFunction
```

2. Download the tool.

```
dotnet tool install -g Amazon.Lambda.TestTool-<dotnet-project-version>
```

3. Setup for launch.json

```
{
    "version": "0.2.0",
    "configurations": [
        {
            "name": "Mock Lambda Test Tool",
            "type": "coreclr",
            "request": "launch",
            "program": "~/.dotnet/tools/dotnet-lambda-test-tool-8.0",
            "args": [],
            "cwd": "${workspaceFolder}/LambdaTest/src/LambdaTest",
            "console": "internalConsole",
            "stopAtEntry": false,
            "internalConsoleOptions": "openOnSessionStart"
        }
    ]
}
```

The [launch.json in the repo](https://github.com/aws/aws-lambda-dotnet/tree/master/Tools/LambdaTestTool#configure-for-visual-studio-code) has`preLaunchTask: "build"` which just doesn't work. So I removed it. In the `cwd` path, it should point to the project. In this case it's `"cwd": "${workspaceFolder}/LambdaTest/src/LambdaTest"`. I didn't know it would be this, I just kept testing out file paths.

Once you have the stuff installed and `launch.json` configured, you can enter debug mode and you'll be good to go.

# README generated when running `dotnet new lambda.EmptyFunction`
# AWS Lambda Empty Function Project

This starter project consists of:
* Function.cs - class file containing a class with a single function handler method
* aws-lambda-tools-defaults.json - default argument settings for use with Visual Studio and command line deployment tools for AWS

You may also have a test project depending on the options selected.

The generated function handler is a simple method accepting a string argument that returns the uppercase equivalent of the input string. Replace the body of this method, and parameters, to suit your needs. 

## Here are some steps to follow from Visual Studio:

To deploy your function to AWS Lambda, right click the project in Solution Explorer and select *Publish to AWS Lambda*.

To view your deployed function open its Function View window by double-clicking the function name shown beneath the AWS Lambda node in the AWS Explorer tree.

To perform testing against your deployed function use the Test Invoke tab in the opened Function View window.

To configure event sources for your deployed function, for example to have your function invoked when an object is created in an Amazon S3 bucket, use the Event Sources tab in the opened Function View window.

To update the runtime configuration of your deployed function use the Configuration tab in the opened Function View window.

To view execution logs of invocations of your function use the Logs tab in the opened Function View window.

## Here are some steps to follow to get started from the command line:

Once you have edited your template and code you can deploy your application using the [Amazon.Lambda.Tools Global Tool](https://github.com/aws/aws-extensions-for-dotnet-cli#aws-lambda-amazonlambdatools) from the command line.

Install Amazon.Lambda.Tools Global Tools if not already installed.
```
    dotnet tool install -g Amazon.Lambda.Tools
```

If already installed check if new version is available.
```
    dotnet tool update -g Amazon.Lambda.Tools
```

Execute unit tests
```
    cd "LambdaTest/test/LambdaTest.Tests"
    dotnet test
```

Deploy function to AWS Lambda
```
    cd "LambdaTest/src/LambdaTest"
    dotnet lambda deploy-function
```
