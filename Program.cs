using RunTime = RuntimeDemystification.Compiled.RunTime;

namespace RuntimeDemystification;

internal class Program
{
    static void Main(string[] args)
    {
        ReportSyntaxSugar();

        Console.WriteLine();

        ReportCompiled();
    }

    static void ReportCompiled()
    {
        var baseLogic = RunTime.New<Compiled.BaseLogic>();
        var logic = RunTime.New<Compiled.Logic>();

        var work = RunTime.Call<string>(baseLogic, nameof(Compiled.BaseLogic.Work));
        var workReason = RunTime.Call<string>(baseLogic, nameof(Compiled.BaseLogic.WorkReason));

        var derivedWork = RunTime.Call<string>(logic, nameof(Compiled.Logic.Work));

        Console.WriteLine($"Under the hood: Work: {work}");
        Console.WriteLine($"Under the hood: WorkReason: {workReason}");

        Console.WriteLine($"Under the hood: DerivedWork: {derivedWork}");
    }

    static void ReportSyntaxSugar()
    {
        var baseLogic = new SyntaxSugar.BaseLogic();
        var logic = new SyntaxSugar.Logic();

        var work = baseLogic.Work();
        var workReason = baseLogic.WorkReason();

        var derivedWork = new SyntaxSugar.Logic().Work();

        Console.WriteLine($"Syntax Work: {work}");
        Console.WriteLine($"Syntax WorkReason: {workReason}");

        Console.WriteLine($"Syntax DerivedWork: {derivedWork}");
    }
}
