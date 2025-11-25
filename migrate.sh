#!/bin/bash

# Project paths
PROJ_NAME="NoteX"
INFRA_PROJ="src/$PROJ_NAME.Infrastructure"
API_PROJ="src/$PROJ_NAME.API"

read -p "Migration name: " MIGRATION_NAME

# Verify if migration name is empty
if [ -z "$MIGRATION_NAME" ]; then
  echo "[X] Migration name cannot be empty!"
  exit 1
fi

echo "[!] Creating migration '$MIGRATION_NAME'..."
dotnet ef migrations add "$MIGRATION_NAME" --project "$INFRA_PROJ" --startup-project "$API_PROJ"

if [ $? -eq 0 ]; then
  echo "[!] Migration '$MIGRATION_NAME' created!"
else
  echo "[X] Error creating migration!"
fi