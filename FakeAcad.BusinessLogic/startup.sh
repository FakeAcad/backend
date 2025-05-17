#!/bin/bash

until curl -fsS http://io-service:80/api/ping 2> /dev/null ; do
    sleep 1
done

dotnet FakeAcad.BusinessLogic.dll
