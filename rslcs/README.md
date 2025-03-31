# rslcs

A lightweight C# library that brings Go-like error handling to .NET applications.

## Installation

```bash
dotnet add package rslcs
```

## Features

- **Explicit Error Handling**: No more hidden exceptions or try/catch blocks for expected errors
- **Type Safety**: Compiler-enforced error checking prevents forgetting to handle error cases
- **Deconstruction Support**: C# tuple deconstruction for clean, concise code
- **Operation Chaining**: API for composing operations that might fail
- **Zero Dependencies**: Pure C# implementation with no external dependencies

## Basic Usage

```csharp
using rslcs;

public Result<double> Divide(double a, double b)
{
    if (b == 0)
    {
        return Result<double>.Err(new DivideByZeroException("Cannot divide by zero"));
    }
    return Result<double>.Ok(a / b);
}

Result<double> result = Divide(10, 2);

if (result.IsSuccess)
{
    Console.WriteLine($"Result: {result.Value}");
}
else
{
    Console.WriteLine($"Error: {result.Error.Message}");
}
```

## Tuple Deconstruction

```csharp
var (isSuccess, value, error) = Divide(10, 0);

if (isSuccess)
{
    Console.WriteLine($"Result: {value}");
}
else
{
    Console.WriteLine($"Error: {error.Message}");
}
```

## Operation Chaining

```csharp
Result<double> finalResult = Divide(10, 2)
    .Then(result => Divide(result, 5))
    .Then(result => Divide(result, 2));

// Equivalent to:
var result1 = Divide(10, 2);
if (!result1.IsSuccess) return Result<double>.Err(result1.Error);
var result2 = Divide(result1.Value, 5);
if (!result2.IsSuccess) return Result<double>.Err(result2.Error);
var result3 = Divide(result2.Value, 2);
return result3;
```

## Async Support

```csharp
public async Task<Result<string>> FetchDataAsync(string url)
{
    try
    {
        using HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            return Result<string>.Err(new HttpRequestException(
                $"HTTP error! Status: {response.StatusCode}"));
        }

        string data = await response.Content.ReadAsStringAsync();
        return Result<string>.Ok(data);
    }
    catch (Exception ex)
    {
        return Result<string>.Err(ex);
    }
}

// Usage
public async Task ProcessDataAsync()
{
    Result<string> fetchResult = await FetchDataAsync("https://api.example.com/data");

    if (fetchResult.IsSuccess)
    {
        Console.WriteLine($"Data: {fetchResult.Value}");
    }
    else
    {
        Console.WriteLine($"Error: {fetchResult.Error.Message}");
    }
}
```

## License

MIT License