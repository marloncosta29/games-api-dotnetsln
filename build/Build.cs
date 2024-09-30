using Nuke.Common;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

class Build : NukeBuild
{
    [Solution] readonly Solution Solution;

    public static int Main () => Execute<Build>(x => x.RunApi);

    Target Clean => _ => _
        .Executes(() =>
        {
            DotNetClean(s => s
                .SetProject(Solution));
        });

    Target Restore => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution));
        });

    Target RunApi => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetRun(s => s
                .SetProjectFile(Solution.GetProject("games-api")));
        });

    Target TestApi => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetTest(s => s
                .SetProjectFile(Solution.GetProject("games-api.Tests")));
        });
}
