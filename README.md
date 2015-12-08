# TestExtractor
A toolchain to extract Tests from Test Assemblies.

## Introduction
This toolchain was born out of the necessity to exctract Tests from NUnit Test Assemblies and spread the Tests across a Load Balancing system for parallel and remote execution.

## Current Test Framework Support
As of right now, the toolchain can only extract NUnit Tests from Assemblies.
It uses two DLLs from "NUnit 2.6.3".
-> nunit.core.interfaces.dll
-> nunit.util.dll
