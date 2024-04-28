set -ex

cd $(dirname $0)/../

artifactsFolder="./artifacts"

if [ -d $artifactsFolder ]; then
  rm -R $artifactsFolder
fi

mkdir -p $artifactsFolder

dotnet restore ./Aix.StateMachines.sln
dotnet build ./Aix.StateMachines.sln -c Release


dotnet pack ./src/Aix.StateMachines/Aix.StateMachines.csproj -c Release -o $artifactsFolder
