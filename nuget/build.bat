@echo Off
set config=%1
if "%config%" == "" (
   set config=Release
)
 
set version=1.0.0
if not "%PackageVersion%" == "" (
   set version=%PackageVersion%
)

set nuget=
if "%nuget%" == "" (
	set nuget=.\tools\nuget.exe
)

%nuget% restore .\AbiokaApi.sln 

"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild" .\AbiokaApi.sln /t:Build /p:Configuration="%config%" /flp:LogFile=msbuild.log;Verbosity=diag

%nuget% pack ".\nuget\Domain.nuspec" -NoPackageAnalysis -verbosity detailed -o Build -Version %version% -p Configuration="%config%"
%nuget% pack ".\nuget\Infrastructure.Common.nuspec" -NoPackageAnalysis -verbosity detailed -o Build -Version %version% -p Configuration="%config%"
%nuget% pack ".\nuget\Repository.nuspec" -NoPackageAnalysis -verbosity detailed -o Build -Version %version% -p Configuration="%config%"
%nuget% pack ".\nuget\ApplicationService.nuspec" -NoPackageAnalysis -verbosity detailed -o Build -Version %version% -p Configuration="%config%"
%nuget% pack ".\nuget\Infrastructure.Framework.nuspec" -NoPackageAnalysis -verbosity detailed -o Build -Version %version% -p Configuration="%config%"
%nuget% pack ".\nuget\Host.nuspec" -NoPackageAnalysis -verbosity detailed -o Build -Version %version% -p Configuration="%config%"
%nuget% pack ".\nuget\All.nuspec" -NoPackageAnalysis -verbosity detailed -o Build -Version %version% -p Configuration="%config%"