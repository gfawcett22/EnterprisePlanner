#!/bin/bash

declare -a projects=()

cd ./API_Gateway

dotnet run &

projects+=$!

cd ../Customers

dotnet run &

projects+=$!

cd ../Orders

dotnet run &

projects+=$!

export projects
