@echo off

REM Bước 1: Khởi động Redis Server cho node thứ nhất
SET var=%cd%
cd node1
start /B redis-server redis.conf

REM Bước 2: Khởi động Redis Server cho node thứ hai
rem cd E:\redis\node2
cd %var%
cd node2
start /B redis-server redis.conf

REM Bước 3: Khởi động Redis Server cho node thứ ba
rem cd E:\redis\node3
cd %var%
cd node3
start /B redis-server redis.conf

REM Bước 4: Chờ một khoảng thời gian để Redis Server khởi động
ping 127.0.0.1 -n 5 > nul

REM Bước 5: Tạo Redis Cluster
cd E:\redis\RedisCluster

REM Xóa dữ liệu trong node 127.0.0.1:7000
redis-cli -h 127.0.0.1 -p 7000 FLUSHALL

REM Xóa dữ liệu trong node 127.0.0.1:7001
redis-cli -h 127.0.0.1 -p 7001 FLUSHALL

REM Xóa dữ liệu trong node 127.0.0.1:7002
redis-cli -h 127.0.0.1 -p 7002 FLUSHALL


echo yes | redis-cli --cluster create 127.0.0.1:7000 127.0.0.1:7001 127.0.0.1:7002
 
 
 
pause
