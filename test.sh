#!/bin/bash

# Run tests
dotnet test --no-build --logger "console;verbosity=detailed" | sed -e "s/Passed/$(printf '\033[0;32mPASS\033[0m')/" -e "s/Failed/$(printf '\033[0;31mFAIL\033[0m')/"