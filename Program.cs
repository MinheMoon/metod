using System;

public abstract class Operation
{
    public virtual float Calculate(float a, float b)
    {
        ValidateInputs(a, b);
        float result = PerformCalculation(a, b);
        LogOperation(a, b, result);
        return result;
    }

    protected abstract float PerformCalculation(float a, float b);

    protected virtual void ValidateInputs(float a, float b) { }

    protected virtual void LogOperation(float a, float b, float result)
    {
        Console.WriteLine($"{GetType().Name}: {a} {GetOperationSymbol()} {b} = {result}");
    }

    protected abstract string GetOperationSymbol();
}

public class Addition : Operation
{
    protected override float PerformCalculation(float a, float b) => a + b;
    protected override string GetOperationSymbol() => "+";
}

public class Subtraction : Operation
{
    protected override float PerformCalculation(float a, float b) => a - b;
    protected override string GetOperationSymbol() => "-";
}

public class Multiplication : Operation
{
    protected override float PerformCalculation(float a, float b) => a * b;
    protected override string GetOperationSymbol() => "*";
}

public class Division : Operation
{
    protected override void ValidateInputs(float a, float b)
    {
        if (b == 0)
        {
            throw new DivideByZeroException("Ділення на нуль неможливе.");
        }
    }

    protected override float PerformCalculation(float a, float b) => a / b;
    protected override string GetOperationSymbol() => "/";
}

public class Calculator
{
    public static float PerformOperation(Operation operation, float a, float b)
    {
        return operation.Calculate(a, b);
    }
}
class Program
{
    static void Main()
    {
        try
        {
            Console.WriteLine(Calculator.PerformOperation(new Addition(), 5, 3));
            Console.WriteLine(Calculator.PerformOperation(new Subtraction(), 10, 4));
            Console.WriteLine(Calculator.PerformOperation(new Multiplication(), 6, 7));
            Console.WriteLine(Calculator.PerformOperation(new Division(), 20, 5));
        }
        catch (DivideByZeroException e)
        {
            Console.WriteLine($"Помилка: {e.Message}");
        }
    }
}