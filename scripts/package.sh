set -ex

cd $(dirname $0)/../

artifactsFolder="./artifacts"

if [ -d $artifactsFolder ]; then
  rm -R $artifactsFolder
fi

mkdir -p $artifactsFolder

dotnet restore ./KafkaMessageBus.sln
dotnet build ./KafkaMessageBus.sln -c Release


dotnet pack ./src/KafkaMessageBus/Aix.KafkaMessageBus.csproj -c Release -o $artifactsFolder
