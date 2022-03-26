using System;
using System.Linq;
using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.Docker;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[CheckBuildProjectConfigurations]
[UnsetVisualStudioEnvironmentVariables]
class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode
    public static int Main() => Execute<Build>(x => x.LogoutFromGithubDockerRegistry);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] readonly Solution Solution;
    [GitRepository] readonly GitRepository GitRepository;

    AbsolutePath SourceDirectory => RootDirectory / "src";
    AbsolutePath OutputDirectory => RootDirectory / "output";
    AbsolutePath DockerFilePath => RootDirectory / "Dockerfile";

    string tag = "";
    string buildNumber = "";
    string branch = "";
    [Parameter("Github access token for packages")] readonly string GitHubAccessToken;
    string repo = "ghcr.io/upinepal/upinepal";
    string user = "iambipinpaul";

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            SourceDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
            EnsureCleanDirectory(OutputDirectory);
        });

    Target Restore => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            DotNetRestore(b => b
                .SetProjectFile(Solution));
        });

    Target CheckDockerVersion => _ => _
        .DependsOn(CheckBranch)
        .Executes(() =>
        {
            DockerTasks.DockerVersion();
        });

    Target BuildDockerImage => _ => _
        .DependsOn(LoginIntoGithubDockerRegistry)
        .DependsOn(DetermineTag)
        .Executes(() =>
        {
            var buildContext = ".";
            if (IsLocalBuild)
            {
                buildContext = "../";
            }

            DockerTasks.DockerBuild(b =>
                b.SetFile(DockerFilePath.ToString())
                    .SetPath(buildContext)
                    .SetTag($"{repo}:{tag}")
            );
        });

    Target LoginIntoGithubDockerRegistry => _ => _
        .DependsOn(CheckDockerVersion)
        .Executes(() =>
        {
            DockerTasks.DockerLogin(b => b
                .SetServer("ghcr.io")
                .SetUsername(user)
                .SetPassword(GitHubAccessToken)
            );
        });

    Target DetermineTag => _ => _
        .Executes(() =>
        {
            if (IsServerBuild && GitHubActions.Instance != null)
            {
                Console.WriteLine(GitHubActions.Instance.Ref);
                var gitHubRef = GitHubActions.Instance.Ref.Split('/').Last();
                tag = gitHubRef == "main"
                    ? "github-latest"
                    : $"github-{ GitHubActions.Instance.Ref.Split('/').Last()}-{GitHubActions.Instance.JobId}";
            }
            else
            {
                Console.WriteLine("Not on server");
                tag = $"not-server-build";
            }
        });

    Target PushDockerImage => _ => _
        .DependsOn(BuildDockerImage)
        .Executes(() =>
        {
            DockerTasks.DockerPush(b =>
                b.SetName($"{repo}:{tag}"));
        });

    Target LogoutFromGithubDockerRegistry => _ => _
        .DependsOn(PushDockerImage)
        .Executes(() =>
        {
            DockerTasks.DockerLogout(b => b
                .SetServer("ghcr.io")
            );
        });

    Target CheckBranch => _ => _
        .Executes(() =>
        {
            Console.WriteLine(GitRepository.Branch);
        });
}
