#!/bin/sh

dotnet lambda deploy-function -pl ./ModifyEc2
dotnet lambda deploy-function -pl ./StartEC2
dotnet lambda deploy-function -pl ./StopEC2