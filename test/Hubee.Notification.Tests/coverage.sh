#!/bin/bash

rm -rf TestResults

dotnet test Hubee.NotificationApp.Tests.csproj --collect:"XPlat Code Coverage"

cd TestResults

for dir in '*/'
do
reportgenerator "-reports:${dir}/coverage.cobertura.xml" "-targetdir:coveragereport" -reporttypes:Html
done