pause
start ..\SimpleZooKeeper\bin\Debug\net6.0\SimpleZooKeeper.exec --urls http://localhost:5020
pause 
start .\bin\Debug\net6.0\SimpleService.exec --urls http://localhost:5003
start .\bin\Debug\net6.0\SimpleService.exec --urls http://localhost:5004
start .\bin\Debug\net6.0\SimpleService.exec --urls http://localhost:5005
start .\bin\Debug\net6.0\SimpleService.exec --urls http://localhost:5007
