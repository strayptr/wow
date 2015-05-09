@echo off
setlocal

:: get the scriptfile's path.
SET scriptdir=%~dp0

:: execute the visualizer, passing in any commandline args.
start "" "%scriptdir%/x86/Visualizer/Visualizer.exe" %*

