#!/bin/bash
cd "$(dirname "$0")"
env ASPNETCORE_ENVIRONMENT=Release dotnet Personaltool.dll