set tag=%1
IF "%~1"=="" SET tag=latest

docker build -t cityinfoapi:%tag% .

pause