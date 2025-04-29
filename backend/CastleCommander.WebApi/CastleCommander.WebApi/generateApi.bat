@echo off

:: Download the Swagger JSON file
curl -k https://localhost:7226/swagger/v1/swagger.json -o swagger.json
echo Swagger JSON file has been saved as swagger.json

:: Generate the TypeScript client
nswag openapi2tsclient /input:swagger.json /output:../../../frontend/src/app/api-client.ts